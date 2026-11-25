# ERD: Heizungskonfigurator
Version: 0.1.0  
Datum: 25.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
Grundlage: `Solution_Design.md` v0.4.0, `Anforderungen.md` v0.6.0, `Fachliches_Konzept.md` v0.6.0

Jede Anwendung hat ihre eigene Datenbank. Beziehungen zwischen den beiden Datenbanken gibt es nicht; der Heizungskonfigurator übergibt Leads über die API an das Backend der Verwaltungsapp.

## 1. Datenbank des Heizungskonfigurators

```mermaid
erDiagram
    CatalogVersion ||--o{ Question : "enthält"
    CatalogVersion ||--o{ Rule : "enthält"
    Question ||--o{ AnswerOption : "hat"
    Rule ||--|{ ConditionGroup : "hat"
    ConditionGroup ||--|{ Condition : "hat"
    Component |o..o{ Rule : "ArticleNumber"

    CatalogVersion {
        int Id PK
        int VersionNumber "leer beim Entwurf"
        string Status "Draft, Published"
        datetime PublishedAt
        string PublishedBy
        string Comment
        string ChangeLog
    }

    Question {
        int Id PK
        int CatalogVersionId FK
        guid QuestionId "stabil, je Version eindeutig"
        string Key "je Version eindeutig"
        string Text
        string AnswerType "Enum, Number, Boolean"
        string HelpText
        bool AllowUnknown
        string DefaultValue
        string Unit
        decimal Min
        decimal Max
        int Position
        bool IsActive
    }

    AnswerOption {
        int Id PK
        int QuestionRowId FK
        string Text
        int Position
    }

    Rule {
        int Id PK
        int CatalogVersionId FK
        guid RuleId "stabil"
        string Name
        bool IsActive
        string ActionType
        string ArticleNumber
        int FixedQuantity
        guid TargetQuestionId
        string HintText
    }

    ConditionGroup {
        int Id PK
        int RuleRowId FK
        int Position
    }

    Condition {
        int Id PK
        int ConditionGroupId FK
        string Key
        string Operator
        string Value
    }

    Component {
        int Id PK
        string ArticleNumber UK "id in der Firmendatenbank"
        string Name "aus der Firmendatenbank"
        string Text "aus der Firmendatenbank"
        decimal Price "aus der Firmendatenbank"
        bool IsActive
    }
```

- `ArticleNumber`, `Name`, `Text` und `Price` werden über die API aus der Firmendatenbank übernommen; `IsActive` wird in der Verwaltungsapp gepflegt.
- `Rule.ArticleNumber` verweist auf `Component.ArticleNumber`. Die automatische Prüfung vor dem Veröffentlichen stellt sicher, dass der Baustein vorhanden und aktiv ist.
- `Condition.Key` verweist auf den `Key` einer Frage derselben Version, `Rule.TargetQuestionId` auf deren stabile `QuestionId`. Diese Verweise werden beim Prüfen kontrolliert, nicht über Fremdschlüssel.
- `PublishedBy` speichert den Benutzer als Text, weil die Benutzer in der LeadDB liegen.

## 2. LeadDB (Verwaltungsapp)

```mermaid
erDiagram
    Lead ||--o{ LeadAnswer : "hat"
    Lead ||--o{ LeadRuleHit : "hat"
    Lead ||--o{ LeadComponent : "hat"
    Lead ||--o{ LeadStatusChange : "hat"
    Lead ||--o{ NotificationAttempt : "hat"
    User |o--o{ Lead : "ist zuständig für"
    User ||--o{ LeadStatusChange : "ändert"
    User ||--o{ NotificationAttempt : "empfängt"

    Lead {
        int Id PK
        datetime CreatedAt
        string FirstName
        string LastName
        string Email
        string Phone
        string Street
        string PostalCode
        string City
        datetime PrivacyConsentAt
        string Remarks
        int CatalogVersionNumber
        string HeatingSystem "Antwort auf Key heizsystem"
        bool NeedsConsultation
        decimal CostMin
        decimal CostMax
        string Status
        string Priority "hoch, mittel, niedrig"
        string LostReason
        int AssignedUserId FK "zuständiger Benutzer, leer bis zur Zuweisung"
        string ExistingCustomerId "Treffer in der Firmendatenbank"
    }

    LeadAnswer {
        int Id PK
        int LeadId FK
        guid QuestionId
        string Key
        string QuestionText
        string AnswerType
        int Position
        bool WasShown
        string Value
        bool IsUnknown
        string AssumedValue
    }

    LeadRuleHit {
        int Id PK
        int LeadId FK
        guid RuleId
        string RuleName
        string ConditionText
        string MatchedValues
        string ActionType
        string Result
    }

    LeadComponent {
        int Id PK
        int LeadId FK
        string ArticleNumber
        string Name
        int Quantity
        decimal Price
    }

    LeadStatusChange {
        int Id PK
        int LeadId FK
        string NewStatus
        datetime ChangedAt
        int ChangedByUserId FK
    }

    NotificationAttempt {
        int Id PK
        int LeadId FK
        int RecipientUserId FK
        string Channel
        datetime AttemptedAt
        bool Succeeded
        string Error
    }

    User {
        int Id PK
        string Name
        string Email UK
        string PasswordHash
        string Role "Administrator, Geschäftsführer, Mitarbeiter"
        bool NotificationsEnabled
    }
```

- Die Lead-Tabellen speichern Fragen, Antworten, Regeln und Bausteine als Kopie zum Anfragezeitpunkt. `QuestionId`, `RuleId` und `ArticleNumber` sind deshalb keine Fremdschlüssel.
- Das PDF wird nicht gespeichert, sondern bei Bedarf aus den Daten des Leads erzeugt.
- Die Status-Werte sind vorläufig (siehe `Fachliches_Konzept.md`, „Leadverwaltung“).

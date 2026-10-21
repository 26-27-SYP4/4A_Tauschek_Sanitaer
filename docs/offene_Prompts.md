# Offene Promts

## Wiesner: Gemini 14.10-11:40
Wir haben jetzt beschlossen das der AG über eine Software die eingegangenen Kostenschätzungen mit jeglichen Details angezeigt bekommt. Der Kunde erfährt lediglich die Preisspanne und das die kostenschätzung bei unseren AG angekommen ist. 


Das ist eine hervorragende architektonische und fachliche Entscheidung! Den Prozess von E-Mails und statischen PDFs in eine zentrale Verwaltungssoftware (ein Admin-Dashboard) zu verlagern, macht das Projekt viel professioneller. Es verhindert E-Mail-Chaos beim Auftraggeber und verwandelt euer Projekt in ein echtes Lead-Management-System (CRM).

Diese Änderung hat allerdings direkte Auswirkungen auf euer aktuelles Design. Hier ist die Übersicht, was sich dadurch ändert und was ihr anpassen müsst:
1. Konsequenzen für die Architektur & Sicherheit

    Das Admin-Dashboard wird Pflicht: Ihr müsst neben dem Konfigurator (für WordPress) nun auch ein geschütztes Backend-Frontend (z.B. unter admin.tauschek-heizung.at) für den Installateur bauen.

    Authentifizierung (JWT) ist jetzt Akut: Im Solution_Design.md stand bisher: "Installateur Login läuft später über JWT." Das "später" ist jetzt hinfällig. Ihr müsst sofort ein Login-System (ASP.NET Core Identity mit JWT) für das C#-Backend einplanen.

    Datenbank-Erweiterung: Eure Angebots-Datenbank braucht jetzt zwingend Status-Felder, damit der AG in seiner Software den Überblick behält (z. B. Spalte Status: Neu, Kontaktiert, Abgesagt, Auftrag erteilt).

    Verschlankung der E-Mail/PDF-Logik:

        Der Kunde braucht vermutlich gar kein aufwendiges PDF mehr, sondern sieht am Ende des Formulars eine saubere Erfolgsmeldung mit der Preisspanne und bekommt eine kurze Bestätigungs-E-Mail ("Ihre Anfrage wird bearbeitet").

        Der AG bekommt keine E-Mail mit einem riesigen PDF und Kundendaten mehr (was auch DSGVO-technisch viel sauberer ist!), sondern nur einen simplen Ping: "Neuer Lead eingegangen. Hier einloggen: [Link zur Software]".

2. Was ihr jetzt in den Dokumenten anpassen müsst

Im Fachlichen Konzept (Fachliches_Konzept.md):

    Den Soll-Zustand umschreiben: Die Erwähnung des "automatischen PDFs" für den Kunden entfernen oder abschwächen. Hinzufügen, dass der AG die Leads über eine eigene Verwaltungssoftware ansieht und bearbeitet.

In den Anforderungen (Anforderungen.md):

    Anforderung 4 & 5 ("Firma erhält E-Mail" / "Leads in Software managen") anpassen. Die Software rückt jetzt in den Fokus.

    Neue Anforderung hinzufügen: Der Auftraggeber muss sich sicher im System einloggen können, um Kundendaten einzusehen.

    Neue Anforderung hinzufügen: Der Auftraggeber muss den Status eines Leads in der Software ändern können.

Im Solution Design (Solution_Design.md):

    Unter "Applikations-Typ" ergänzen, dass es aus zwei Teilen besteht: 1. Public Web-App (Konfigurator) und 2. Secured Web-App (Admin-Dashboard).

    Unter "Sicherheit" das "später" beim Installateur-Login streichen.

Wofür braucht ihr als Nächstes Input? Sollen wir die genauen Funktionen für dieses neue AG-Dashboard definieren oder wollt ihr zuerst die Datenbank-Tabelle für das Lead-Tracking entwerfen?

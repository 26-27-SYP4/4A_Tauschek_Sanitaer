# Fachliches Konzept: Heizungskonfigurator
Version: 0.6.0  
Datum: 25.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
AG bedeutet im Folgenden: Auftraggeber  
Endkunde bedeutet im Folgenden: Kunde des AG (weitere Begriffe siehe `Glossar.md`)
## Ziel des Projekts
Ziel des Projekts ist die Entwicklung eines Heizungskonfigurators mit integrierter Kostenschätzung. Dieser soll potenziellen Endkunden ermöglichen, eine passende Heizung zu konfigurieren und eine erste Einschätzung der zu erwartenden Kosten zu erhalten. Dadurch kann der AG bereits im Vorfeld das Interesse der potenziellen Käufer besser einschätzen und gezielt auf interessierte Endkunden zugehen.
## Wer wird das System verwenden?
Der Heizungskonfigurator wird von den Endkunden der Firma verwendet. Die Leadverwaltungssoftware wird von den Geschäftsführern, Mitarbeitern und Administratoren der Firma genutzt.
## Ist-Zustand
Der Endkunde geht in die Firma, wenn er eine Heizung kaufen will. Der AG stellt Fragen zur Heizung, wie es aktuell ist und was sich der Endkunde vorstellt. Der AG schreibt selbst ein Angebot, sie gehen das Angebot dann gemeinsam nochmal durch und wenn alles passt, wird der Auftrag bestätigt und die Heizung montiert.
## Soll-Zustand
Wenn der Endkunde eine Heizung will, geht er auf die Website der Firma, wo es einen neuen Abschnitt “Heizungskonfigurator” gibt. Der Endkunde kann beim Heizungskonfigurator bequem eingeben, was er will. Es wird automatisch ein PDF generiert, in dem die Kostenschätzung enthalten ist. Gleichzeitig wird der Lead in der Verwaltungsapp gespeichert und der AG bekommt eine E-Mail, dass ein neuer Lead eingegangen ist. Die Details stehen in der Verwaltungsapp.
## Fragenkatalog
Für die Entwicklung des Heizungskonfigurators wurde vom Geschäftsführer ein Fragenkatalog zur Verfügung gestellt. Dieser dient als Grundlage für die Gestaltung des Konfigurators und legt fest, welche Informationen von den Endkunden abgefragt werden sollen.

Der Fragenkatalog umfasst mehrere Schritte, in denen Informationen zum bestehenden Heizsystem, zum Gebäude, zum Warmwasserbedarf und zum gewünschten neuen Heizsystem erhoben werden. Je nach Auswahl werden zusätzliche Fragen eingeblendet, um die Anforderungen und Wünsche der Endkunden möglichst genau zu erfassen.

Am Ende des Konfigurationsprozesses geben die Endkunden ihre Kontaktdaten bekannt, können optional sonstige Bemerkungen eintragen und stimmen den Datenschutzbestimmungen zu. Die gesammelten Informationen dienen als Grundlage für eine erste Kostenschätzung und die anschließende Bearbeitung der Kundenanfragen.

Der Konfigurator muss flexibel in der Erweiterbarkeit der Fragen sein, damit bestehende Fragen angepasst, entfernt oder neue Fragen hinzugefügt werden können. Für Fragen, die Endkunden möglicherweise nicht beantworten können, gibt es die Antwort „Ich weiß nicht“ mit einem hinterlegten Standardwert. Wie Fragen und Regeln gepflegt und versioniert werden, beschreibt das Kapitel „Pflege von Fragen und Regeln“. Der vom Geschäftsführer bereitgestellte Fragenkatalog bildet die erste Version.

## Bausteine
Die Kostenschätzung wird aus standardisierten Bausteinen zusammengesetzt. Ein Baustein ist ein konkretes, vordefiniertes Produkt oder Leistungspaket aus dem Sortiment des AG, zum Beispiel ein Pelletkessel, ein Pufferspeicher oder die Montage. Regeln wählen anhand der Antworten des Endkunden die Bausteine aus; das System zählt ihre Preise zu einem groben Angebot zusammen.

Die Bausteine werden über eine API-Schnittstelle aus der Firmendatenbank des AG in die Datenbank des Heizungskonfigurators übernommen und automatisch zusammengeführt.

### Merkmale eines Bausteins
| Merkmal | Beschreibung | Beispiel |
| ------- | ------------ | -------- |
| Artikelnummer | Verweis auf den Artikel in der Firmendatenbank | laut Firmendatenbank |
| Bezeichnung | Name, wie er im Angebot erscheint | laut Firmendatenbank |
| Text | Beschreibung des Bausteins | laut Firmendatenbank |
| Preis (€) | Richtpreis für das grobe Angebot | laut Firmendatenbank |
| Aktiv | ob der Baustein im Konfigurator verwendet wird | ja / nein |

### Von den Antworten zum Angebot
1. **Bausteine wählen:** über Regeln der Form „Bedingung → Baustein, Menge“. Alle zutreffenden Regeln werden angewendet; liefern mehrere Regeln denselben Baustein, werden die Mengen addiert.
2. **Preis berechnen:** Summe aus Preis × Menge aller Bausteine; daraus ergibt sich die Preisspanne durch einen Ab- bzw. Aufschlag in Prozent, den der AG festlegt.
3. **Annahmen und Hinweise** ins PDF übernehmen.

Wie Regeln aufgebaut sind und gepflegt werden, beschreibt das Kapitel „Pflege von Fragen und Regeln“. Beispiele für Regeln:

| Bedingung | Baustein |
| --------- | -------- |
| Heizsystem Pellets | Pelletkessel |
| Heizsystem Biomasse | Pufferspeicher |
| Warmwasser soll über die neue Heizung laufen | Warmwasserspeicher passend zur Personenzahl |
| Sanierung mit bestehender Heizung | Demontage der Altanlage |
| Mehrere Heizkreise | Heizkreisgruppe |

### Sonderfälle
- **Unbekannte Angaben** („Ich weiß nicht“): Das System verwendet einen Standardwert und weist ihn im PDF als Annahme aus.
- **Persönliche Beratung:** Trifft eine Regel „persönliche Beratung“ zu, etwa bei einem Sonderwunsch, oder wurde kein Baustein ausgewählt, wird kein automatisches Angebot erstellt; der Lead wird für eine persönliche Beratung markiert.
- **Unlogische oder technisch ungünstige Kombinationen**, etwa Kühlen mit Heizkörpern: Welche Kombinationen das sind, wird über Regeln mit der Aktion „Hinweis an den Endkunden“ festgelegt. Der Hinweis erscheint bereits bei der Eingabe.

### Pflege der Bausteine
Artikel und Preise werden über eine API-Schnittstelle aus der Firmendatenbank übernommen; der Zugriff auf die Firmendatenbank erfolgt nur lesend. Die Regeln werden wie im Kapitel „Pflege von Fragen und Regeln“ beschrieben gepflegt. Der Abgleich läuft regelmäßig, damit die Preise aktuell bleiben.

## Pflege von Fragen und Regeln
Fragen und Regeln sind nicht fest vorgegeben, sondern werden in der Verwaltungskonsole gepflegt, ohne Programmierung. Der Administrator und der Geschäftsführer dürfen sie bearbeiten und veröffentlichen; Mitarbeiter können sie nur ansehen.

### Fragen
Die Antwort des Endkunden auf eine Frage wird in einer Variable gespeichert; deren Name ist der Key (z. B. heizsystem = Biomasse, wohnflaeche = 140). Regeln beziehen sich auf Keys, nicht auf einzelne Fragen. In einer Version befüllt jeder Key genau eine Frage. Zu jeder Frage werden festgelegt:

| Merkmal | Beschreibung | Beispiel |
| ------- | ------------ | -------- |
| Key | Variable, in der die Antwort gespeichert wird und auf die sich Regeln beziehen | heizsystem |
| Fragetext | Text, wie ihn der Endkunde sieht | Welches Heizsystem wünschen Sie sich? |
| Antworttyp | Auswahl, Zahl oder Ja/Nein (siehe unten) | Auswahl |
| Hilfetext | optionaler Erklärungstext zur Frage | Wo finde ich meinen Verbrauch? |
| „Ich weiß nicht“ | ob diese Antwort angeboten wird; falls ja, mit Standardwert | ja, Standardwert laut Freigabe des AG |
| Position | Stelle der Frage in der Reihenfolge | 3 |
| Aktiv | ob die Frage im Heizungskonfigurator verwendet wird | ja / nein |

| Antworttyp | Zusätzliche Angaben | Eingabe im Konfigurator |
| ---------- | ------------------- | ----------------------- |
| Auswahl (Enum) | Liste der Antwortmöglichkeiten | Auswahl genau einer Möglichkeit |
| Zahl (Number) | Einheit, Minimum, Maximum; Kommazahlen sind erlaubt | Zahleneingabe innerhalb der Grenzen |
| Ja/Nein (Boolean) | – | Ja oder Nein |

Eine Mehrfachauswahl ist nicht vorgesehen. Freitextfragen gibt es ebenfalls nicht, weil sich freie Texte nicht verlässlich auswerten lassen. Einzige Ausnahme ist das optionale Feld „Sonstige Bemerkungen“ am Ende des Konfigurators: Es wird nicht in Regeln verwendet, sondern nur im Lead angezeigt.

**Reihenfolge:** Der Administrator bzw. der Geschäftsführer ordnet die Fragen in der Verwaltungskonsole durch Verschieben an. Der Heizungskonfigurator zeigt sie in dieser Reihenfolge, eine Frage pro Seite. Folgefragen erscheinen an ihrer Position nur dann, wenn ihre Bedingungen erfüllt sind.

**Änderungen an Fragen:** Fragen dürfen geändert, deaktiviert und gelöscht werden; Fragetext, Hilfetext, Antwortmöglichkeiten, Antworttyp und Position sind frei änderbar. Eine neue Frage darf einen bereits verwendeten Key übernehmen, etwa wenn eine Frage neu formuliert wird. Dann gelten bestehende Regeln für diesen Key weiter, und Leads verschiedener Versionen lassen sich in der Verwaltungsapp gemeinsam nach diesem Key filtern. Der Antworttyp gehört zur Frage: Ändert er sich, behält der Key seinen Namen, Regeln mit unpassenden Vergleichen meldet die Prüfung, und alte Leads behalten ihren damaligen Antworttyp. Auswertungen über Versionen hinweg sind für diesen Key dann nur eingeschränkt möglich.

Kontaktdaten und Datenschutz-Zustimmung sind keine Fragen des Fragenkatalogs, sondern fester Bestandteil am Ende des Konfigurators.

**Heizsystem im Lead:** Die Antwort auf die Frage mit dem Key `heizsystem` wird beim Lead zusätzlich als gewünschtes Heizsystem gespeichert, damit Leadliste und Filter es anzeigen können. Gibt es keine Frage mit diesem Key oder wurde „Ich weiß nicht“ gewählt, bleibt das Heizsystem leer.

### Bedingungen
Eine Bedingung vergleicht den Wert eines Keys mit einem Wert, zum Beispiel „heizsystem ist gleich Biomasse“. Welche Vergleiche möglich sind, hängt vom Antworttyp der Frage ab, die den Key befüllt:

| Antworttyp | Vergleiche |
| ---------- | ---------- |
| Zahl | gleich, ungleich, kleiner, kleiner gleich, größer, größer gleich |
| Auswahl | gleich, ungleich (Wert aus der Liste der Antwortmöglichkeiten) |
| Ja/Nein | ist Ja, ist Nein |
| alle Antworttypen | wurde mit „Ich weiß nicht“ beantwortet |

Mehrere Bedingungen werden mit UND und ODER verknüpft. Damit die Darstellung übersichtlich bleibt, werden Bedingungen in Gruppen zusammengefasst: Innerhalb einer Gruppe gilt UND, zwischen den Gruppen gilt ODER. Beispiel: (heizsystem ist gleich Wärmepumpe UND wohnfläche größer 150) ODER (anzahl_heizkreise größer 1).

### Regeln
Eine Regel besteht aus einem Namen, den Bedingungen, einer Aktion und dem Merkmal Aktiv. Inaktive Regeln werden nicht angewendet, bleiben aber erhalten und können jederzeit wieder aktiviert werden.

| Aktion | Beschreibung | Beispiel |
| ------ | ------------ | -------- |
| Baustein mit fester Menge | fügt eine feste Anzahl eines Bausteins hinzu | heizsystem = Biomasse → 1 Stück Pufferspeicher |
| Folgefrage einblenden | eine Frage wird nur angezeigt, wenn die Bedingungen erfüllt sind | Wärmequelle nur bei heizsystem = Wärmepumpe |
| Hinweis an den Endkunden | Hinweis bei unlogischen oder ungünstigen Kombinationen schon während der Eingabe | Kühlen mit Heizkörpern |
| Persönliche Beratung | kein automatisches Angebot, Lead für persönliche Beratung markieren | Sonderwunsch |

Bausteine werden in Regeln mit ihrer Artikelnummer angegeben. Die konkreten Regeln und Standardwerte legt der AG fest.

### Entwurf, Prüfung und Veröffentlichung
Änderungen an Fragen und Regeln wirken nicht sofort. Sie werden zunächst in einem Entwurf gesammelt:

1. **Bearbeiten:** Fragen und Regeln im Entwurf anlegen, ändern, verschieben, aktivieren, deaktivieren oder löschen.
2. **Automatische Prüfung:** Das System meldet unter anderem Regeln, die einen Key verwenden, den keine aktive Frage befüllt, Regeln, die sich auf eine entfernte Antwortmöglichkeit oder einen inaktiven bzw. nicht mehr vorhandenen Baustein beziehen, Keys, die von mehr als einer Frage befüllt werden, Vergleiche, die nicht zum Antworttyp passen, Folgefragen, die sich gegenseitig bedingen, und Folgefragen, die vor der Frage stehen, von der sie abhängen.
3. **Testmodus:** Der Benutzer gibt Beispielantworten ein und sieht, welche Fragen eingeblendet werden, welche Regeln zutreffen, welche Bausteine gewählt werden und welche Kostenspanne entsteht.
4. **Bestätigen:** Erst danach wird der Entwurf veröffentlicht. Ab diesem Zeitpunkt verwendet der Heizungskonfigurator die neuen Fragen und Regeln.

### Versionierung
Jede Veröffentlichung erzeugt eine neue Version des gesamten Fragenkatalogs mit allen Regeln. Zu jeder Version werden festgehalten:
- fortlaufende Versionsnummer
- Zeitpunkt und Benutzer der Veröffentlichung
- optionaler Kommentar
- Änderungsliste: neue, geänderte, verschobene, deaktivierte und gelöschte Fragen sowie neue, geänderte, aktivierte, deaktivierte und gelöschte Regeln

Veröffentlichte Versionen werden nicht mehr verändert. Frühere Versionen können eingesehen werden; die Rückkehr zu einer früheren Version erzeugt eine neue Version mit deren Inhalt. Leads werden durch neue Versionen nicht neu berechnet (siehe „Entstehung eines Leads“).

### Offene Punkte
- Umgang mit Konfigurationen, die während einer Veröffentlichung laufen (Vorschlag: Die Konfiguration bleibt bis zum Absenden bei der Version, mit der sie begonnen wurde)
- konkrete Fragen, Standardwerte und Regeln (stellt der AG bereit)

## Leadverwaltung

### Entstehung eines Leads
Ein Lead entsteht automatisch, wenn der Kunde den Konfigurator absendet, und wird im Backend der Verwaltungsapp gespeichert. Er enthält:
- Kontaktdaten des Kunden und die Zustimmung zum Datenschutz
- sonstige Bemerkungen des Kunden, falls angegeben
- die Versionsnummer des Fragenkatalogs, mit der die Konfiguration erstellt wurde
- das gewünschte Heizsystem (siehe „Heizsystem im Lead“)
- alle Antworten aus dem Fragenkatalog mit Key und Fragetext zum Anfragezeitpunkt; bei „Ich weiß nicht“ den verwendeten Standardwert als Annahme
- die ermittelten Bausteine und die Kostenschätzung
- die Begründung: welche Regeln zugetroffen haben, mit ihren Bedingungen, den dafür maßgeblichen Antworten und dem jeweiligen Ergebnis
- Zeitpunkt der Anfrage
- Status, zuständigen Mitarbeiter und Notizen des AG

Antworten und Begründung werden beim Lead selbst gespeichert. Spätere Änderungen an Fragen oder Regeln verändern einen bestehenden Lead daher nicht, und Regeln neuer Versionen werden nicht auf alte Leads angewendet.

### Status eines Leads
Jeder Lead hat genau einen Status. Der AG ändert ihn manuell; jede Änderung wird mit Zeitpunkt und Benutzer protokolliert.

| Status | Bedeutung |
| ------ | --------- |
| Neu | Anfrage ist eingegangen, noch niemand hat sie geöffnet |
| In Bearbeitung | ein Mitarbeiter kümmert sich um den Lead |
| Kontaktiert | der Kunde wurde angerufen oder angeschrieben |
| Angebot erstellt | ein genaues Angebot wurde dem Kunden übergeben |
| Gewonnen | der Auftrag wurde bestätigt |
| Verloren | kein Auftrag, optional mit Grund |

Die Status-Werte sind vorläufig und werden mit dem AG abgestimmt.

### Verwaltungskonsole
Die Verwaltungskonsole ist eine eigene Oberfläche, getrennt vom öffentlichen Konfigurator, und nur nach Anmeldung erreichbar.

**Leadliste**
- Übersicht aller Leads mit Name, Datum, gewünschtem Heizsystem, Kostenschätzung, Status und Zuständigem
- Sortierung und Filter nach Status, Zeitraum, Zuständigem, Heizsystem und Priorität
- Suche nach Name, E-Mail oder Ort
- neue Leads sind hervorgehoben

**Lead-Detailansicht**
- Versionsnummer des Fragenkatalogs zum Anfragezeitpunkt
- alle Fragen dieser Version, jeweils gekennzeichnet als beantwortet oder nicht eingeblendet
- alle Antworten des Kunden; Standardwerte bei „Ich weiß nicht“ sind als Annahme gekennzeichnet
- die gewählten Bausteine mit Begründung: zutreffende Regeln mit Bedingung im Klartext (z. B. „heizsystem ist gleich Biomasse“), den maßgeblichen Antworten und dem Ergebnis (Baustein und Menge, Hinweis, persönliche Beratung)
- das grobe Angebot als PDF
- Kontaktdaten mit Schnellaktionen (anrufen, E-Mail schreiben)
- Statusverlauf und Notizen
- Hinweis, ob der Kunde bereits Bestandskunde ist
- Benachrichtigungsstatus (wann und über welchen Kanal wurden die Benutzer informiert)

### Vorsortierung nach Interesse
Der AG möchte erkennen, welche Kunden wirklich Interesse haben. Das System kennzeichnet jeden Lead mit einer Priorität (hoch, mittel, niedrig) anhand nachvollziehbarer Merkmale, zum Beispiel:
- gewünschter Umsetzungszeitraum (kurzfristig = höher)
- Vollständigkeit der Angaben und Kontaktdaten
- ob ein automatisches Angebot erstellt werden konnte
- Sonderwunsch oder Beratungsbedarf

Die Priorität ist nur ein Vorschlag; der AG kann sie jederzeit ändern. Die Merkmale und ihre Gewichtung werden mit dem AG festgelegt und sollen später anpassbar sein.

### Bestandskunden
Das System prüft beim Eingang eines Leads, ob der Kunde bereits in der Firmendatenbank des AG vorkommt (Abgleich z. B. über E-Mail, Name und Adresse). Ein Treffer wird in Liste und Detailansicht angezeigt. Der Zugriff auf die Firmendatenbank erfolgt nur lesend. Wie sicher ein Treffer ist, wird dem Benutzer angezeigt, da ein Abgleich nicht immer eindeutig ist.

## Benachrichtigungssystem

### Empfänger
Bei jedem neuen Lead bekommt der AG eine E-Mail. Zusätzlich werden alle weiteren Benutzer mit aktivierter Benachrichtigung informiert.

### Inhalt einer Benachrichtigung
Die Benachrichtigung teilt nur mit, dass ein neuer Lead eingegangen ist, und enthält einen Link zum Lead, zum Beispiel: „Neue Anfrage eingegangen. Details: <Link zum Lead>“. Alle Details (Kontaktdaten, Angaben, Kostenschätzung, Bestandskunden-Hinweis) stehen in der Verwaltungsapp. Dadurch werden auch über Kanäle, die Dritte mitlesen könnten (SMS, Push auf dem Sperrbildschirm), keine personenbezogenen Daten übertragen.

### Kanäle
Die technische Umsetzung ist noch offen. Zur Auswahl stehen:

| Kanal | Vorteile | Nachteile / Zu klären |
| ----- | -------- | --------------------- |
| E-Mail | einfach, kostenlos, kann viele Details enthalten | wird teils spät gelesen; Spam-Ordner möglich |
| SMS | wird sofort wahrgenommen | Kosten pro Nachricht, nur wenig Text, Anbieter nötig |
| WhatsApp | sehr verbreitet, wird schnell gelesen | Business-Schnittstelle mit Freigabe von Nachrichtenvorlagen nötig, Kosten, Datenschutz prüfen |
| Push-Benachrichtigung (Progressive Web App) | kostenlos, sofort, passt zur Web-Anwendung | Benutzer muss die App installieren und Push erlauben; auf iPhones nur bei installierter App |

**Empfehlung für die erste Version:** E-Mail als verpflichtender Kanal. Die Benachrichtigung wird so aufgebaut, dass weitere Kanäle später ergänzt werden können, ohne den Rest des Systems zu ändern. Welcher zweite Kanal sich lohnt, entscheidet der AG.

### Einstellungen
- Der Administrator legt fest, welche Kanäle im System verfügbar sind.
- Jeder Benutzer wählt für sich, über welche Kanäle er welche Ereignisse bekommt.
- Kontaktdaten für Benachrichtigungen (E-Mail-Adresse, Handynummer) werden pro Benutzer hinterlegt.

### Fehlerverhalten
- Ein Lead wird immer gespeichert, auch wenn die Benachrichtigung scheitert.
- Scheitert der Versand, versucht das System es erneut und protokolliert den Fehler.
- Der Benachrichtigungsstatus (gesendet, fehlgeschlagen) ist im Lead sichtbar.

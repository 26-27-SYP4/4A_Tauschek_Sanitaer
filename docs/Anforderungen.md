# Anforderungen: Heizungskonfigurator
Version: 0.3.0  
Datum: 11.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
Grundlage: `Fachliches_Konzept.md` v0.4.0, `Solution_Design.md` v0.2.0, Entwurf „Heizungs-Konfigurator“ des AG (Oktober 2026). Beide genannten Markdown-Dateien liegen im selben Ordner wie dieses Dokument.
 
AG = Auftraggeber, Endkunde = Kunde des AG
 
## Legende
- **Priorität nach MoSCoW:** M = Must (muss in der ersten Version sein), S = Should (soll, wenn möglich), C = Could (kann, wenn Zeit bleibt)
- **Bezug:** Kapitel im Fachlichen Konzept, aus dem die Anforderung stammt
# Funktionale Anforderungen
 
## 1. Konfigurator (Endkunde)
Bezug: Fachkonzept „Soll-Zustand“, „Fragenkatalog“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| KON-01 | Der Konfigurator ist ohne Anmeldung als eigener Abschnitt auf der Website des AG erreichbar. | Die Seite ist ohne Login über die Website des AG aufrufbar. | M |
| KON-02 | Der Endkunde gibt die Angaben zu bestehender Heizung, Gebäude, Warmwasser und gewünschtem Heizsystem laut Fragenkatalog ein. | Alle im Fragenkatalog vorgesehenen Angaben können eingegeben und beim Absenden übernommen werden. | M |
| KON-03 | Der Konfigurator führt Schritt für Schritt durch die Fragen (eine Frage pro Seite, Fortschrittsanzeige, „Zurück“ und „Weiter“). | Fragen erscheinen einzeln; Fortschritt sowie „Zurück“ und „Weiter“ funktionieren in einem vollständigen Testdurchlauf. | M |
| KON-04 | Folgefragen werden abhängig von vorherigen Antworten ein- oder ausgeblendet (z. B. Wärmequelle nur bei Wärmepumpe). | Für definierte Testantworten werden ausschließlich die jeweils passenden Folgefragen angezeigt. | M |
| KON-05 | Bei Fragen, die Endkunden oft nicht beantworten können, gibt es die Antwort „Ich weiß nicht“. | Bei allen dafür vorgesehenen Fragen ist „Ich weiß nicht“ auswählbar und die Konfiguration kann fortgesetzt werden. | M |
| KON-06 | Eingaben werden geprüft: Pflichtfelder, gültige Zahlenbereiche (z. B. Fläche, Personen), gültige E-Mail-Adresse. | Leere Pflichtfelder, Werte außerhalb der festgelegten Zahlenbereiche und ungültige E-Mail-Adressen werden erkannt und verständlich markiert. | M |
| KON-07 | Das System erkennt unlogische oder technisch ungünstige Kombinationen und weist den Endkunden darauf hin (z. B. Kühlen mit Heizkörpern, Wärmepumpe bei hoher Vorlauftemperatur). | Für die festgelegten unlogischen bzw. ungünstigen Kombinationen erscheint jeweils ein passender Hinweis. | M |
| KON-08 | Am Ende gibt der Endkunde seine Kontaktdaten ein; Absenden ist nur mit Zustimmung zu den Datenschutzbestimmungen möglich. | Ohne Kontaktdaten oder Datenschutz-Zustimmung lässt sich die Konfiguration nicht absenden; mit Zustimmung ist das Absenden möglich. | M |
| KON-09 | Der Konfigurator begrenzt die Anzahl der Anfragen pro Zeit (Rate-Limiting), um automatisierte Anfragen einzuschränken. | Nach Überschreiten des konfigurierten Anfrage-Limits innerhalb des festgelegten Zeitraums werden weitere Anfragen blockiert. | M |
| KON-10 | Der Konfigurator verwendet reCaptcha als zusätzlichen Schutz gegen automatisierte Anfragen. | Eine Anfrage kann erst nach erfolgreicher reCaptcha-Prüfung abgesendet werden; eine fehlgeschlagene Prüfung wird abgewiesen. | S |
| KON-11 | Auswahlfragen werden als Kacheln mit kurzem Erklärungstext und Hinweisen dargestellt. | Jede Auswahlfrage wird als Kachel mit Erklärungstext und gegebenenfalls Hinweis dargestellt. | S |
| KON-12 | Zu einer Frage kann ein Hilfetext angezeigt werden (z. B. wo man den Verbrauch findet). | Bei einer Frage lässt sich der Hilfetext öffnen und wieder schließen. | S |
| KON-13 | Bereits eingegebene Antworten gehen beim Neuladen der Seite nicht verloren. | Nach Neuladen der Seite sind zuvor eingegebene Antworten weiterhin vorhanden. | C |
| KON-14 | Der Endkunde kann Fotos vom Heizraum und einen Hausplan hochladen. | Fotos und Hausplan in den festgelegten Dateiformaten und Größen können hochgeladen und der Konfiguration zugeordnet werden. | C |
 
## 2. Pflege der Fragen
Bezug: Fachkonzept „Fragenkatalog“ (Erweiterbarkeit der Fragen)
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| FRG-01 | Fragen, Antwortoptionen und Einblendbedingungen werden in der Datenbank gespeichert, nicht im Programmcode. | Fragen, Antwortoptionen und Einblendbedingungen können in der Datenbank gefunden und ohne Codeänderung angepasst werden. | M |
| FRG-02 | Ein Lead speichert Fragen und Antworten so, wie sie zum Zeitpunkt der Anfrage waren; spätere Änderungen an Fragen verändern alte Leads nicht. | Ein bereits gespeicherter Lead zeigt nach Änderung einer Frage weiterhin den ursprünglichen Fragetext und die damalige Antwort. | M |
| FRG-03 | Der Administrator kann Fragen in der Verwaltungskonsole anlegen, ändern, sortieren und deaktivieren, ohne Programmierung. | Ein Administrator kann eine Frage anlegen, bearbeiten, sortieren und deaktivieren; die Änderungen werden im Konfigurator übernommen. | S |
| FRG-04 | Der Administrator kann Einblendbedingungen, Hilfetexte und Standardwerte für „Ich weiß nicht“ festlegen. | Ein Administrator kann Einblendbedingungen, Hilfetexte und Standardwerte speichern; diese werden im nächsten Konfigurator-Durchlauf angewendet. | S |
 
## 3. Bausteine und Kostenschätzung
Bezug: Fachkonzept „Bausteine“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BAU-01 | Das System schätzt den Wärmebedarf aus dem bekannten Verbrauch oder, wenn dieser fehlt, aus Fläche und Gebäudezustand. | Bei vorhandenem Verbrauch wird dieser für die Bedarfsschätzung verwendet; fehlt er, erfolgt die Schätzung anhand von Fläche und Gebäudezustand. | M |
| BAU-02 | Das System wählt genau einen Wärmeerzeuger der gewünschten Technologie, dessen Eignungsbereich zum Wärmebedarf passt. | Für einen Testfall mit passendem Baustein wird genau ein Wärmeerzeuger der gewünschten Technologie und mit passendem Eignungsbereich ausgewählt. | M |
| BAU-03 | Das System ergänzt Zusatzbausteine (Speicher, Zubehör, Dienstleistungen) über Regeln der Form „Bedingung → Baustein, Menge“. | Für jede definierte Regel werden der zugehörige Zusatzbaustein und die festgelegte Menge in die Auswahl aufgenommen. | M |
| BAU-04 | Das System berechnet auf Basis der Bausteine automatisch eine Kostenspanne (von–bis). | Die angezeigte Unter- und Obergrenze entspricht der Summe der Preis-Unter- bzw. Obergrenzen der ausgewählten Bausteine. | M |
| BAU-05 | Bei „Ich weiß nicht“ verwendet das System einen Standardwert und speichert ihn als Annahme. | Bei Auswahl von „Ich weiß nicht“ wird der konfigurierte Standardwert verwendet und als Annahme gespeichert. | M |
| BAU-06 | Gibt es keinen passenden Baustein (z. B. sehr hoher Wärmebedarf, Sonderwunsch), wird kein automatisches Angebot erstellt und der Lead für persönliche Beratung markiert. | Bei einem Testfall ohne passenden Baustein wird keine automatische Kostenschätzung erstellt und der Lead als beratungsbedürftig markiert. | M |
| BAU-07 | Bei technisch nur bedingt passenden Kombinationen wird ein Hinweis „Prüfung vor Ort nötig“ gespeichert. | Bei einer als bedingt passend definierten Kombination wird der Hinweis „Prüfung vor Ort nötig“ gespeichert und ausgegeben. | M |
| BAU-08 | Ein Baustein hat die Merkmale Bezeichnung, Kategorie, Technologie, Heizleistung, Eignung, Hersteller/Modell, Preis, Artikelnummer und Aktiv. | Für jeden Baustein können alle neun genannten Merkmale gespeichert und wieder ausgelesen werden. | M |
| BAU-09 | Artikel und Preise werden regelmäßig über eine API-Schnittstelle aus der Firmendatenbank des AG übernommen; der Zugriff auf die Firmendatenbank ist nur lesend. | Die API-Schnittstelle übernimmt Artikel und Preise aus der Firmendatenbank; Schreiboperationen auf dieser Datenbank finden nicht statt. | S |
| BAU-10 | Der Administrator pflegt Leistung, Eignung und Regeln der Bausteine in der Verwaltungskonsole, ohne Programmierung. | Ein Administrator kann Leistung, Eignung und Regeln ändern; die nächste Berechnung berücksichtigt die Änderungen ohne Codeänderung. | S |
| BAU-11 | Bausteine können aktiviert und deaktiviert werden; deaktivierte Bausteine werden nicht ausgewählt. | Ein deaktivierter Baustein wird bei einem Testlauf auch dann nicht ausgewählt, wenn seine übrigen Kriterien passen. | S |
 
## 4. PDF mit Kostenschätzung
Bezug: Fachkonzept „Soll-Zustand“, „Sonderfälle“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| PDF-01 | Nach dem Absenden wird automatisch ein PDF mit der Kostenspanne und den gewählten Bausteinen erstellt. | Nach erfolgreichem Absenden wird ein PDF erzeugt, das Kostenspanne und ausgewählte Bausteine enthält. | M |
| PDF-02 | Das PDF weist die Kostenschätzung deutlich als unverbindlichen Richtwert aus, nicht als verbindliche Feinplanung. | Im PDF ist die Kostenschätzung eindeutig als unverbindlicher Richtwert und nicht als verbindliche Feinplanung bezeichnet. | M |
| PDF-03 | Das PDF enthält alle Annahmen (Standardwerte bei „Ich weiß nicht“) und Hinweise (z. B. „Prüfung vor Ort nötig“). | Alle verwendeten Standardannahmen und alle zutreffenden Hinweise sind im PDF angeführt. | M |
| PDF-04 | Der Endkunde kann das PDF nach dem Absenden sofort herunterladen. | Der Download des erzeugten PDFs ist unmittelbar nach dem Absenden möglich und die Datei lässt sich öffnen. | M |
| PDF-05 | Das PDF enthält Logo und Kontaktdaten des AG. | Das PDF zeigt das Logo sowie die festgelegten Kontaktdaten des AG. | S |
| PDF-06 | Der Endkunde bekommt das PDF zusätzlich per E-Mail. | Nach dem Absenden wird das PDF an die angegebene E-Mail-Adresse versendet. | S |
| PDF-07 | Kann kein automatisches Angebot erstellt werden, bekommt der Endkunde statt des PDFs eine Bestätigung, dass sich der AG persönlich meldet. | Bei einem Testfall ohne automatisches Angebot wird eine Bestätigung angezeigt, die eine persönliche Rückmeldung des AG ankündigt. | S |
 
## 5. Energieberatung
Bezug: Meeting 19.10.2026 („automatische Energieberatung mit aktuellen Tagespreisen“). **Noch nicht im Fachkonzept beschrieben**, Kapitel muss ergänzt werden.
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| ENB-01 | Das PDF enthält die geschätzten jährlichen Energiekosten der neuen Heizung. | Das PDF weist die geschätzten jährlichen Energiekosten der neuen Heizung aus. | S |
| ENB-02 | Bei bekanntem bisherigen Verbrauch werden die Energiekosten der alten und der neuen Heizung verglichen. | Bei vorhandenem Altverbrauch enthält die Ausgabe die jährlichen Kosten der alten und der neuen Heizung in vergleichbarer Form. | S |
| ENB-03 | Der Administrator kann die Energiepreise (Heizöl, Gas, Strom, Pellets, Holz) manuell pflegen. | Ein Administrator kann Preise für Heizöl, Gas, Strom, Pellets und Holz ändern und speichern; die Berechnung verwendet die gespeicherten Werte. | S |
| ENB-04 | Energiepreise werden täglich automatisch aus einer öffentlichen Quelle übernommen; bei Ausfall wird der letzte bekannte Preis verwendet. | Bei erfolgreichem Abruf werden Preise aktualisiert; bei simuliertem Quellenausfall bleiben die zuletzt gespeicherten Preise in Verwendung. | C |
 
## 6. Leadverwaltung
Bezug: Fachkonzept „Leadverwaltung“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| LEA-01 | Jede abgeschlossene Konfiguration wird automatisch als Lead mit allen Antworten, Bausteinen und Kostenschätzung gespeichert. | Nach jeder erfolgreich abgeschlossenen Konfiguration existiert genau ein Lead mit Antworten, Bausteinen und Kostenschätzung. | M |
| LEA-02 | Die Verwaltungskonsole ist nur nach Anmeldung erreichbar. | Nicht angemeldete Benutzer können keine Seite und keine Daten der Verwaltungskonsole aufrufen. | M |
| LEA-03 | Der Zugriff richtet sich nach der Rolle des Benutzers (Geschäftsführer, Mitarbeiter, Administrator). | Benutzer jeder Rolle können nur die gemäß ihrer Rolle freigegebenen Funktionen und Daten verwenden. | M |
| LEA-04 | Benutzer sehen alle für sie freigegebenen Leads in einer Liste mit Sortierung, Filter und Suche. | Die Leadliste zeigt freigegebene Leads; Sortierung, Filter und Suche liefern für vorbereitete Testdaten die erwarteten Ergebnisse. | M |
| LEA-05 | Jeder Lead hat einen Status, der manuell geändert werden kann; Änderungen werden mit Zeitpunkt und Benutzer protokolliert. | Ein Benutzer kann den Status ändern; jede Änderung enthält den neuen Status, Zeitpunkt und Benutzer in der Historie. | M |
| LEA-06 | Die Detailansicht zeigt alle Angaben des Kunden, die Bausteine und das PDF. | Die Detailansicht enthält sämtliche gespeicherten Kundenangaben, Bausteine und das zugehörige PDF. | M |
| LEA-07 | Das System schlägt für jeden Lead eine Priorität (hoch, mittel, niedrig) vor; der AG kann sie ändern. | Für jeden Lead wird eine Priorität hoch, mittel oder niedrig vorgeschlagen; ein berechtigter Benutzer kann sie ändern. | S |
| LEA-08 | Das System zeigt an, ob der Kunde bereits Bestandskunde ist, und wie sicher der Treffer ist. | Bei einem passenden Datenbankeintrag wird ein Bestandskunden-Hinweis samt Treffer-Sicherheit angezeigt; ohne Treffer wird dies ebenfalls erkennbar. | S |
| LEA-09 | Beim Status „Verloren“ kann ein Grund erfasst werden. | Beim Setzen des Status „Verloren“ kann ein Verlustgrund eingegeben und anschließend angezeigt werden. | C |
| LEA-10 | Einfache Auswertung (Anzahl Leads, Anteil gewonnen/verloren) für den Geschäftsführer. | Für den Geschäftsführer werden Gesamtzahl der Leads sowie Anzahl bzw. Anteil gewonnener und verlorener Leads angezeigt. | C |
| LEA-11 | Leads lassen sich als Datei exportieren (z. B. CSV). | Ein berechtigter Benutzer kann Leads exportieren; die exportierte Datei enthält die ausgewählten Lead-Daten in einem lesbaren Format. | C |
| LEA-12 | Merkmale und Gewichtung der Priorität sind in der Verwaltungskonsole anpassbar. | Ein Administrator kann Prioritätsmerkmale und deren Gewichtung ändern; die neue Gewichtung wirkt sich auf die nächste Prioritätsberechnung aus. | C |

<!--| Leads können einem Mitarbeiter zugewiesen werden. | S |
| Mitarbeiter können Notizen zu einem Lead erfassen. | S |-->
 
## 7. Benachrichtigungssystem
Bezug: Fachkonzept „Benachrichtigungssystem“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BEN-01 | Bei jedem neuen Lead werden alle Benutzer mit aktivierter Benachrichtigung per E-Mail benachrichtigt (Kunde, Kontakt, wichtigste Angaben, Kostenschätzung, Link zum Lead). | Bei einem neuen Lead erhalten alle Benutzer mit aktivierter Benachrichtigung eine E-Mail mit Kunde, Kontaktdaten, wichtigsten Angaben, Kostenschätzung und Link zum Lead. | M |
| BEN-02 | Die Benachrichtigung wird automatisch und ohne Verzögerung nach Abschluss der Konfiguration ausgelöst. | Die Benachrichtigung wird nach Abschluss der Konfiguration automatisch ausgelöst; der Versand startet ohne manuelle Aktion. | M |
| BEN-03 | Ein Lead wird gespeichert, auch wenn der Versand fehlschlägt; fehlgeschlagene Versuche werden wiederholt und protokolliert. | Bei simuliertem E-Mail-Ausfall bleibt der Lead gespeichert; Versandversuche werden erneut ausgeführt und der Fehler wird protokolliert. | M |
| BEN-04 | Der Benachrichtigungsstatus ist in der Verwaltungskonsole beim Lead sichtbar. | In der Leadansicht ist erkennbar, ob die Benachrichtigung gesendet wurde oder fehlgeschlagen ist. | S |
| BEN-05 | Dringende Leads werden in der Benachrichtigung hervorgehoben. | Bei einem Lead mit hoher Priorität enthält die Benachrichtigung eine eindeutige Dringlichkeitskennzeichnung. | S |
| BEN-06 | Der Administrator wird informiert, wenn Angebot oder PDF nicht erstellt werden konnten. | Wenn die PDF- oder Angebotserstellung fehlschlägt, erhält der Administrator eine entsprechende Meldung. | S |
| BEN-07 | Das System ist so aufgebaut, dass weitere Kanäle (Push, SMS, WhatsApp) ohne Umbau ergänzt werden können. | Ein zusätzlicher Benachrichtigungskanal kann über eine klar abgegrenzte Schnittstelle ergänzt werden, ohne bestehende Leadabläufe umzubauen. | S |
| BEN-08 | Benutzer wählen selbst, über welche Kanäle sie benachrichtigt werden. | Ein Benutzer kann seine gewünschten Ereignisse und Benachrichtigungskanäle speichern; nur diese Einstellungen werden berücksichtigt. | C |
| BEN-09 | Erinnerung bei Leads, die zu lange unbearbeitet bleiben. | Für einen länger unbearbeiteten Lead wird nach Ablauf der konfigurierten Zeit eine Erinnerung an den festgelegten Empfänger ausgelöst. | C |
| BEN-10 | Mindestens ein weiterer Kanal neben E-Mail (Push, SMS oder WhatsApp). | Ein konfigurierter zusätzlicher Kanal neben E-Mail kann eine Testbenachrichtigung erfolgreich zustellen. | C |
 
# Nicht-funktionale Anforderungen
Jede Anforderung hat Akzeptanzkriterien, anhand derer überprüft wird, ob sie erfüllt ist.
 
## 8. Benutzbarkeit
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BENUTZ-01 | Die Web-App funktioniert auf Smartphone, Tablet und Laptop. | Alle Seiten sind bei 360 px, 768 px und 1366 px Breite ohne waagrechtes Scrollen bedienbar; getestet in aktuellem Chrome, Firefox, Safari und Edge. | M |
| BENUTZ-02 | Der Konfigurator ist ein intuitiver Schritt-für-Schritt-Assistent. | 3 Testpersonen ohne Vorwissen schließen den Konfigurator ohne Hilfe ab; Ziel unter 10 Minuten. | M |
| BENUTZ-03 | Alle Texte für Endkunden sind auf Deutsch und ohne Fachbegriffe bzw. mit Erklärung. | Review der Texte durch den AG. | S |
 
## 9. Leistung
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| LEIST-01 | Seitenwechsel innerhalb des Konfigurators laden schnell. | Wechsel zur nächsten Frage in unter 1 Sekunde (gemessen im Browser, normale Breitbandverbindung). | M |
| LEIST-02 | Die Kostenschätzung wird ohne spürbare Verzögerung berechnet. | Berechnung inkl. Antwort des Backends in unter 2 Sekunden; PDF-Erstellung in unter 5 Sekunden. | M |
| LEIST-03 | Die Benachrichtigung erreicht die Empfänger zeitnah. | E-Mail wird innerhalb von 1 Minute nach dem Absenden an die vorgesehenen Empfänger verschickt. | S |
 
## 10. Sicherheit und Datenschutz
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| SICH-01 | Personenbezogene Daten werden verschlüsselt in der Datenbank gespeichert. | Datenbank bzw. betroffene Spalten sind verschlüsselt; Klartext ist ohne Schlüssel nicht lesbar. | M |
| SICH-02 | Die gesamte Kommunikation läuft verschlüsselt. | Nur HTTPS; Aufrufe über HTTP werden auf HTTPS umgeleitet. | M |
| SICH-03 | Passwörter der Benutzer werden nur gehasht gespeichert; die Anmeldung erfolgt über JWT. | Code-Review; kein Passwort im Klartext in der Datenbank. | M |
| SICH-04 | Das System ist gegen SQL-Injection und XSS geschützt. | Datenbankzugriff nur über Entity Framework Core; Texteingaben werden im Backend bereinigt; Test mit typischen Angriffseingaben. | M |
| SICH-05 | Die Verarbeitung entspricht der DSGVO. | Zustimmung zum Datenschutz wird mit Zeitpunkt gespeichert; Leads können auf Wunsch gelöscht werden; Testdaten sind anonymisiert. | M |
| SICH-06 | Logdateien enthalten keine echten Kundendaten. | Review der Logs nach einem Testdurchlauf. | M |
 
## 11. Zuverlässigkeit und Wartbarkeit
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| WART-01 | Die Datenbank wird täglich automatisch gesichert. | Täglicher verschlüsselter SQL-Dump auf einem separaten Speichermedium des AG; Wiederherstellung einmal erfolgreich getestet. | M |
| WART-02 | Fragen, Bausteine und Regeln sind ohne Änderung am Programmcode erweiterbar. | Eine neue Frage und ein neuer Baustein werden über die Verwaltungskonsole angelegt und funktionieren ohne neues Deployment. | S |
| WART-03 | Der Code ist automatisch getestet. | Unit-, Integrations- und End-to-End-Tests laufen in der CI/CD-Pipeline bei jedem Merge auf main. | S |

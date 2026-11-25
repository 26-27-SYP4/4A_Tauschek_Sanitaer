# Anforderungen: Heizungskonfigurator
Version: 0.5.0  
Datum: 25.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
Grundlage: `Fachliches_Konzept.md` v0.5.0, `Solution_Design.md` v0.3.0, Entwurf „Heizungs-Konfigurator“ des AG (Oktober 2026). Beide genannten Markdown-Dateien liegen im selben Ordner wie dieses Dokument.
 
AG = Auftraggeber, Endkunde = Kunde des AG
 
## Legende
- **Priorität (Zahlen):** 1 = zwingende Grundbasis ab Projektstart, 2 = notwendige Kernfunktion, 3 = wichtig für den vollständigen Arbeitsablauf, 4 = sinnvolle Erweiterung, 5 = optional bzw. später umsetzbar
- **Bezug:** Kapitel im Fachlichen Konzept, aus dem die Anforderung stammt
# Funktionale Anforderungen
 
## 1. Konfigurator (Endkunde)
Bezug: Fachkonzept „Soll-Zustand“, „Fragenkatalog“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| KON-01 | Der Konfigurator ist ohne Anmeldung als eigener Abschnitt auf der Website des AG erreichbar. | Die Seite ist ohne Login über die Website des AG aufrufbar. | 2 |
| KON-02 | Der Endkunde gibt die Angaben zu bestehender Heizung, Gebäude, Warmwasser und gewünschtem Heizsystem laut Fragenkatalog ein. | Alle im Fragenkatalog vorgesehenen Angaben können eingegeben und beim Absenden übernommen werden. | 2 |
| KON-03 | Der Konfigurator führt Schritt für Schritt durch die Fragen (eine Frage pro Seite, Fortschrittsanzeige, „Zurück“ und „Weiter“). | Fragen erscheinen einzeln; Fortschritt sowie „Zurück“ und „Weiter“ funktionieren in einem vollständigen Testdurchlauf. | 2 |
| KON-04 | Folgefragen werden abhängig von vorherigen Antworten ein- oder ausgeblendet (z. B. Wärmequelle nur bei Wärmepumpe). | Für definierte Testantworten werden ausschließlich die jeweils passenden Folgefragen angezeigt. | 2 |
| KON-05 | Bei Fragen, die Endkunden oft nicht beantworten können, gibt es die Antwort „Ich weiß nicht“. | Bei allen dafür vorgesehenen Fragen ist „Ich weiß nicht“ auswählbar und die Konfiguration kann fortgesetzt werden. | 2 |
| KON-06 | Eingaben werden geprüft: Pflichtfelder, gültige Zahlenbereiche (z. B. Fläche, Personen), gültige E-Mail-Adresse. | Leere Pflichtfelder, Werte außerhalb der festgelegten Zahlenbereiche und ungültige E-Mail-Adressen werden erkannt und verständlich markiert. | 2 |
| KON-07 | Das System erkennt unlogische oder technisch ungünstige Kombinationen und weist den Endkunden darauf hin (z. B. Kühlen mit Heizkörpern, Wärmepumpe bei hoher Vorlauftemperatur). | Für die festgelegten unlogischen bzw. ungünstigen Kombinationen erscheint jeweils ein passender Hinweis. | 2 |
| KON-08 | Am Ende gibt der Endkunde seine Kontaktdaten ein; Absenden ist nur mit Zustimmung zu den Datenschutzbestimmungen möglich. | Ohne Kontaktdaten oder Datenschutz-Zustimmung lässt sich die Konfiguration nicht absenden; mit Zustimmung ist das Absenden möglich. | 2 |
| KON-09 | Der Konfigurator begrenzt die Anzahl der Anfragen pro Zeit (Rate-Limiting), um automatisierte Anfragen einzuschränken. | Nach Überschreiten des konfigurierten Anfrage-Limits innerhalb des festgelegten Zeitraums werden weitere Anfragen blockiert. | 2 |
| KON-10 | Der Konfigurator verwendet reCaptcha als zusätzlichen Schutz gegen automatisierte Anfragen. | Eine Anfrage kann erst nach erfolgreicher reCaptcha-Prüfung abgesendet werden; eine fehlgeschlagene Prüfung wird abgewiesen. | 3 |
| KON-15 | Vor dem Absenden kann der Endkunde optional sonstige Bemerkungen als Freitext eingeben; sie werden im Lead angezeigt, aber nicht in Regeln ausgewertet. | Ein eingegebener Bemerkungstext erscheint unverändert in der Lead-Detailansicht; die Konfiguration lässt sich auch ohne Bemerkung absenden. | 4 |
| KON-11 | Auswahlfragen werden als Kacheln mit kurzem Erklärungstext und Hinweisen dargestellt. | Jede Auswahlfrage wird als Kachel mit Erklärungstext und gegebenenfalls Hinweis dargestellt. | 4 |
| KON-12 | Zu einer Frage kann ein Hilfetext angezeigt werden (z. B. wo man den Verbrauch findet). | Bei einer Frage lässt sich der Hilfetext öffnen und wieder schließen. | 4 |
| KON-13 | Bereits eingegebene Antworten gehen beim Neuladen der Seite nicht verloren. | Nach Neuladen der Seite sind zuvor eingegebene Antworten weiterhin vorhanden. | 4 |
| KON-14 | Der Endkunde kann Fotos vom Heizraum und einen Hausplan hochladen. | Fotos und Hausplan in den festgelegten Dateiformaten und Größen können hochgeladen und der Konfiguration zugeordnet werden. | 4 |
 
## 2. Pflege von Fragen und Regeln
Bezug: Fachkonzept „Fragenkatalog“, „Pflege von Fragen und Regeln“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| FRG-01 | Fragen, Antwortoptionen, Einblendbedingungen und Regeln werden in der Datenbank gespeichert, nicht im Programmcode. | Fragen, Antwortoptionen, Einblendbedingungen und Regeln können in der Datenbank gefunden und ohne Codeänderung angepasst werden. | 1 |
| FRG-02 | Ein Lead speichert Fragen, Antworten und die angewendeten Regeln so, wie sie zum Zeitpunkt der Anfrage waren; spätere Änderungen an Fragen oder Regeln verändern alte Leads nicht. | Ein bereits gespeicherter Lead zeigt nach Änderung einer Frage oder Regel weiterhin den ursprünglichen Fragetext, die damalige Antwort und die damals zutreffenden Regeln. | 1 |
| FRG-03 | Administrator und Geschäftsführer können Fragen in der Verwaltungskonsole anlegen, ändern, durch Verschieben neu anordnen, deaktivieren und löschen, ohne Programmierung; Mitarbeiter können Fragen und Regeln nur ansehen. | Ein Administrator bzw. Geschäftsführer kann eine Frage anlegen, bearbeiten, verschieben, deaktivieren und löschen; nach dem Veröffentlichen zeigt der Konfigurator die Fragen in der neuen Reihenfolge. Ein Mitarbeiter kann nichts ändern. | 3 |
| FRG-04 | Administrator und Geschäftsführer können Einblendbedingungen, Hilfetexte und Standardwerte für „Ich weiß nicht“ festlegen. | Ein Administrator bzw. Geschäftsführer kann Einblendbedingungen, Hilfetexte und Standardwerte speichern; diese werden nach dem Veröffentlichen im nächsten Konfigurator-Durchlauf angewendet. | 3 |
| FRG-05 | Zu jeder Frage werden Key, Fragetext, Antworttyp (Auswahl, Zahl, Ja/Nein; keine Freitextfragen), optionaler Hilfetext, optional „Ich weiß nicht“ mit Standardwert, Position und Aktiv festgelegt; bei Auswahl die Antwortmöglichkeiten, bei Zahl Einheit, Minimum und Maximum. | Für jeden Antworttyp kann eine Frage mit allen Merkmalen angelegt werden; der Konfigurator zeigt das passende Eingabeelement und prüft Minimum und Maximum. | 3 |
| FRG-06 | Der Key ist die Variable, in der die Antwort gespeichert wird. In einer Version befüllt jeder Key genau eine Frage; eine neue Frage kann einen bestehenden Key übernehmen. Für jeden festen Key (gewünschtes Heizsystem, Angaben zum Wärmebedarf) muss genau eine aktive Frage mit dem erwarteten Antworttyp existieren. | Ein Entwurf, in dem zwei Fragen denselben Key befüllen oder ein fester Key fehlt bzw. den falschen Antworttyp hat, kann nicht veröffentlicht werden; eine neue Frage mit bestehendem Key wird von den Regeln für diesen Key erfasst. | 3 |
| FRG-07 | Regeln bestehen aus Bedingungen (Key, Vergleich passend zum Antworttyp, Wert), die in Gruppen mit UND und zwischen den Gruppen mit ODER verknüpft sind, und einer Aktion: Baustein mit fester Menge, Baustein mit Menge aus Antwort, Wärmeerzeuger wählen, Folgefrage einblenden, Hinweis an den Endkunden, „Prüfung vor Ort nötig“ oder persönliche Beratung. | Für jede Aktion kann eine Regel mit mehreren Bedingungsgruppen angelegt werden; ein Testfall pro Aktion liefert das erwartete Ergebnis; Vergleiche, die nicht zum Antworttyp passen, sind nicht auswählbar. | 3 |
| FRG-08 | Regeln können einzeln aktiviert und deaktiviert werden; inaktive Regeln werden nicht angewendet. | Eine deaktivierte Regel wird in einem Testdurchlauf nicht angewendet, obwohl ihre Bedingungen erfüllt sind; nach erneutem Aktivieren wird sie wieder angewendet. | 3 |
| FRG-09 | Änderungen an Fragen und Regeln werden in einem Entwurf gesammelt und wirken erst nach Bestätigen (Veröffentlichen). | Solange der Entwurf nicht bestätigt ist, verwendet der Konfigurator unverändert die zuletzt veröffentlichte Version. | 3 |
| FRG-10 | Vor dem Veröffentlichen prüft das System den Entwurf automatisch auf Fehler (z. B. Regel verwendet einen Key, den keine aktive Frage befüllt, eine entfernte Antwortmöglichkeit oder einen inaktiven Baustein; Key von mehreren Fragen befüllt; fester Key fehlt; unpassender Vergleich; zirkuläre oder falsch angeordnete Folgefragen). | Ein Entwurf mit jedem der genannten Fehler wird mit verständlicher Meldung abgelehnt; ein fehlerfreier Entwurf kann veröffentlicht werden. | 3 |
| FRG-11 | Im Testmodus kann der Entwurf mit Beispielantworten durchgespielt werden; angezeigt werden eingeblendete Fragen, zutreffende Regeln, Bausteine und Kostenspanne. | Für vorbereitete Beispielantworten zeigt der Testmodus die erwarteten Fragen, Regeln, Bausteine und Kostenspanne; dabei entsteht kein Lead. | 3 |
| FRG-12 | Jede Veröffentlichung erzeugt eine neue, unveränderliche Version mit Versionsnummer, Zeitpunkt, Benutzer, optionalem Kommentar und Änderungsliste. | Nach dem Veröffentlichen existiert eine neue Version mit allen genannten Angaben; die Änderungsliste nennt alle neuen, geänderten, verschobenen, aktivierten, deaktivierten und gelöschten Fragen und Regeln. | 3 |
| FRG-13 | Frühere Versionen können eingesehen und als neue Version wiederhergestellt werden. | Eine frühere Version lässt sich anzeigen; das Wiederherstellen erzeugt eine neue Version mit deren Inhalt, ohne bestehende Versionen zu verändern. | 5 |
 
## 3. Bausteine und Kostenschätzung
Bezug: Fachkonzept „Bausteine“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BAU-08 | Ein Baustein hat die Merkmale Bezeichnung, Kategorie, Technologie, Heizleistung, Eignung, Hersteller/Modell, Preis, Artikelnummer und Aktiv. | Für jeden Baustein können alle neun genannten Merkmale gespeichert und wieder ausgelesen werden. | 1 |
| BAU-01 | Das System schätzt den Wärmebedarf aus dem bekannten Verbrauch oder, wenn dieser fehlt, aus Fläche und Gebäudezustand. | Bei vorhandenem Verbrauch wird dieser für die Bedarfsschätzung verwendet; fehlt er, erfolgt die Schätzung anhand von Fläche und Gebäudezustand. | 2 |
| BAU-02 | Das System wählt genau einen Wärmeerzeuger der gewünschten Technologie, dessen Eignungsbereich zum Wärmebedarf passt. Hat der Endkunde beim gewünschten Heizsystem „Ich weiß nicht“ angegeben, wählen Regeln den Wärmeerzeuger. | Für einen Testfall mit passendem Baustein wird genau ein Wärmeerzeuger der gewünschten Technologie und mit passendem Eignungsbereich ausgewählt; bei „Ich weiß nicht“ wird der Wärmeerzeuger der zutreffenden Regel ausgewählt. | 2 |
| BAU-03 | Das System ergänzt Zusatzbausteine (Speicher, Zubehör, Dienstleistungen) über Regeln der Form „Bedingung → Baustein, Menge“; die Menge ist fest oder stammt aus einer Antwort. Liefern mehrere Regeln denselben Baustein, werden die Mengen addiert. | Für jede zutreffende Regel werden der zugehörige Zusatzbaustein und die festgelegte bzw. aus der Antwort übernommene Menge aufgenommen; liefern zwei Regeln denselben Baustein, erscheint er einmal mit der Summe der Mengen. | 2 |
| BAU-04 | Das System berechnet auf Basis der Bausteine automatisch eine Kostenspanne (von–bis). | Die angezeigte Unter- und Obergrenze entspricht der Summe der Preis-Unter- bzw. Obergrenzen der ausgewählten Bausteine. | 2 |
| BAU-05 | Bei „Ich weiß nicht“ verwendet das System einen Standardwert und speichert ihn als Annahme. | Bei Auswahl von „Ich weiß nicht“ wird der konfigurierte Standardwert verwendet und als Annahme gespeichert. | 2 |
| BAU-06 | Gibt es keinen passenden Baustein (z. B. sehr hoher Wärmebedarf, Sonderwunsch) oder mehr als einen passenden Wärmeerzeuger, wird kein automatisches Angebot erstellt und der Lead für persönliche Beratung markiert. | Bei einem Testfall ohne passenden Baustein und bei einem Testfall mit zwei passenden Wärmeerzeugern wird keine automatische Kostenschätzung erstellt und der Lead als beratungsbedürftig markiert. | 2 |
| BAU-07 | Bei technisch nur bedingt passenden Kombinationen wird ein Hinweis „Prüfung vor Ort nötig“ gespeichert. | Bei einer als bedingt passend definierten Kombination wird der Hinweis „Prüfung vor Ort nötig“ gespeichert und ausgegeben. | 2 |
| BAU-09 | Artikel und Preise werden regelmäßig über eine API-Schnittstelle aus der Firmendatenbank des AG übernommen; der Zugriff auf die Firmendatenbank ist nur lesend. | Die API-Schnittstelle übernimmt Artikel und Preise aus der Firmendatenbank; Schreiboperationen auf dieser Datenbank finden nicht statt. | 3 |
| BAU-10 | Der Administrator pflegt Leistung und Eignung der Bausteine in der Verwaltungskonsole, ohne Programmierung; Regeln werden laut FRG-07 bis FRG-12 gepflegt. | Ein Administrator kann Leistung und Eignung ändern; die nächste Berechnung berücksichtigt die Änderungen ohne Codeänderung. | 3 |
| BAU-11 | Bausteine können aktiviert und deaktiviert werden; deaktivierte Bausteine werden nicht ausgewählt. | Ein deaktivierter Baustein wird bei einem Testlauf auch dann nicht ausgewählt, wenn seine übrigen Kriterien passen. | 3 |
 
## 4. PDF mit Kostenschätzung
Bezug: Fachkonzept „Soll-Zustand“, „Sonderfälle“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| PDF-01 | Nach dem Absenden wird automatisch ein PDF mit der Kostenspanne und den gewählten Bausteinen erstellt. | Nach erfolgreichem Absenden wird ein PDF erzeugt, das Kostenspanne und ausgewählte Bausteine enthält. | 2 |
| PDF-02 | Das PDF weist die Kostenschätzung deutlich als unverbindlichen Richtwert aus, nicht als verbindliche Feinplanung. | Im PDF ist die Kostenschätzung eindeutig als unverbindlicher Richtwert und nicht als verbindliche Feinplanung bezeichnet. | 2 |
| PDF-03 | Das PDF enthält alle Annahmen (Standardwerte bei „Ich weiß nicht“) und Hinweise (z. B. „Prüfung vor Ort nötig“). | Alle verwendeten Standardannahmen und alle zutreffenden Hinweise sind im PDF angeführt. | 2 |
| PDF-04 | Der Endkunde kann das PDF nach dem Absenden sofort herunterladen. | Der Download des erzeugten PDFs ist unmittelbar nach dem Absenden möglich und die Datei lässt sich öffnen. | 2 |
| PDF-07 | Kann kein automatisches Angebot erstellt werden, bekommt der Endkunde statt des PDFs eine Bestätigung, dass sich der AG persönlich meldet. | Bei einem Testfall ohne automatisches Angebot wird eine Bestätigung angezeigt, die eine persönliche Rückmeldung des AG ankündigt. | 3 |
| PDF-05 | Das PDF enthält Logo und Kontaktdaten des AG. | Das PDF zeigt das Logo sowie die festgelegten Kontaktdaten des AG. | 4 |
| PDF-06 | Der Endkunde bekommt das PDF zusätzlich per E-Mail. | Nach dem Absenden wird das PDF an die angegebene E-Mail-Adresse versendet. | 4 |
 
## 5. Energieberatung
Bezug: Meeting 19.10.2026 („automatische Energieberatung mit aktuellen Tagespreisen“). **Noch nicht im Fachkonzept beschrieben**, Kapitel muss ergänzt werden.
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| ENB-01 | Das PDF enthält die geschätzten jährlichen Energiekosten der neuen Heizung. | Das PDF weist die geschätzten jährlichen Energiekosten der neuen Heizung aus. | 4 |
| ENB-02 | Bei bekanntem bisherigen Verbrauch werden die Energiekosten der alten und der neuen Heizung verglichen. | Bei vorhandenem Altverbrauch enthält die Ausgabe die jährlichen Kosten der alten und der neuen Heizung in vergleichbarer Form. | 4 |
| ENB-03 | Der Administrator kann die Energiepreise (Heizöl, Gas, Strom, Pellets, Holz) manuell pflegen. | Ein Administrator kann Preise für Heizöl, Gas, Strom, Pellets und Holz ändern und speichern; die Berechnung verwendet die gespeicherten Werte. | 4 |
| ENB-04 | Energiepreise werden täglich automatisch aus einer öffentlichen Quelle übernommen; bei Ausfall wird der letzte bekannte Preis verwendet. | Bei erfolgreichem Abruf werden Preise aktualisiert; bei simuliertem Quellenausfall bleiben die zuletzt gespeicherten Preise in Verwendung. | 5 |
 
## 6. Leadverwaltung
Bezug: Fachkonzept „Leadverwaltung“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| LEA-02 | Die Verwaltungskonsole ist nur nach Anmeldung erreichbar. | Nicht angemeldete Benutzer können keine Seite und keine Daten der Verwaltungskonsole aufrufen. | 1 |
| LEA-03 | Der Zugriff richtet sich nach der Rolle des Benutzers (Geschäftsführer, Mitarbeiter, Administrator). | Benutzer jeder Rolle können nur die gemäß ihrer Rolle freigegebenen Funktionen und Daten verwenden. | 1 |
| LEA-01 | Jede abgeschlossene Konfiguration wird automatisch als Lead mit allen Antworten, Bausteinen und Kostenschätzung gespeichert. | Nach jeder erfolgreich abgeschlossenen Konfiguration existiert genau ein Lead mit Antworten, Bausteinen und Kostenschätzung. | 2 |
| LEA-04 | Benutzer sehen alle für sie freigegebenen Leads in einer Liste mit Sortierung, Filter und Suche. | Die Leadliste zeigt freigegebene Leads; Sortierung, Filter und Suche liefern für vorbereitete Testdaten die erwarteten Ergebnisse. | 2 |
| LEA-05 | Jeder Lead hat einen Status, der manuell geändert werden kann; Änderungen werden mit Zeitpunkt und Benutzer protokolliert. | Ein Benutzer kann den Status ändern; jede Änderung enthält den neuen Status, Zeitpunkt und Benutzer in der Historie. | 2 |
| LEA-06 | Die Detailansicht zeigt alle Angaben des Kunden, die Bausteine und das PDF. | Die Detailansicht enthält sämtliche gespeicherten Kundenangaben, Bausteine und das zugehörige PDF. | 2 |
| LEA-13 | Die Detailansicht zeigt die Versionsnummer des Fragenkatalogs, alle Fragen dieser Version (beantwortet oder nicht eingeblendet), die Antworten samt Annahmen und die Begründung: zutreffende Regeln mit Bedingung, maßgeblichen Antworten und Ergebnis sowie die Art der Wärmeerzeuger-Auswahl. | Für einen Test-Lead werden Versionsnummer, alle Fragen der Version mit Kennzeichnung, alle Antworten und Annahmen sowie jede zutreffende Regel mit Bedingung, Antwortwerten und Ergebnis angezeigt; nach einer Regeländerung bleibt die Anzeige unverändert. | 3 |
| LEA-07 | Das System schlägt für jeden Lead eine Priorität (hoch, mittel, niedrig) vor; der AG kann sie ändern. | Für jeden Lead wird eine Priorität hoch, mittel oder niedrig vorgeschlagen; ein berechtigter Benutzer kann sie ändern. | 3 |
| LEA-08 | Das System zeigt an, ob der Kunde bereits Bestandskunde ist, und wie sicher der Treffer ist. | Bei einem passenden Datenbankeintrag wird ein Bestandskunden-Hinweis samt Treffer-Sicherheit angezeigt; ohne Treffer wird dies ebenfalls erkennbar. | 3 |
| LEA-09 | Beim Status „Verloren“ kann ein Grund erfasst werden. | Beim Setzen des Status „Verloren“ kann ein Verlustgrund eingegeben und anschließend angezeigt werden. | 4 |
| LEA-10 | Einfache Auswertung (Anzahl Leads, Anteil gewonnen/verloren) für den Geschäftsführer. | Für den Geschäftsführer werden Gesamtzahl der Leads sowie Anzahl bzw. Anteil gewonnener und verlorener Leads angezeigt. | 4 |
| LEA-11 | Leads lassen sich als Datei exportieren (z. B. CSV). | Ein berechtigter Benutzer kann Leads exportieren; die exportierte Datei enthält die ausgewählten Lead-Daten in einem lesbaren Format. | 4 |
| LEA-12 | Merkmale und Gewichtung der Priorität sind in der Verwaltungskonsole anpassbar. | Ein Administrator kann Prioritätsmerkmale und deren Gewichtung ändern; die neue Gewichtung wirkt sich auf die nächste Prioritätsberechnung aus. | 5 |

<!--| Leads können einem Mitarbeiter zugewiesen werden. | S |
| Mitarbeiter können Notizen zu einem Lead erfassen. | S |-->
 
## 7. Benachrichtigungssystem
Bezug: Fachkonzept „Benachrichtigungssystem“
 
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BEN-03 | Ein Lead wird gespeichert, auch wenn der Versand fehlschlägt; fehlgeschlagene Versuche werden wiederholt und protokolliert. | Bei simuliertem E-Mail-Ausfall bleibt der Lead gespeichert; Versandversuche werden erneut ausgeführt und der Fehler wird protokolliert. | 2 |
| BEN-01 | Bei jedem neuen Lead werden alle Benutzer mit aktivierter Benachrichtigung per E-Mail benachrichtigt (Kunde, Kontakt, wichtigste Angaben, Kostenschätzung, Link zum Lead). | Bei einem neuen Lead erhalten alle Benutzer mit aktivierter Benachrichtigung eine E-Mail mit Kunde, Kontaktdaten, wichtigsten Angaben, Kostenschätzung und Link zum Lead. | 3 |
| BEN-02 | Die Benachrichtigung wird automatisch und ohne Verzögerung nach Abschluss der Konfiguration ausgelöst. | Die Benachrichtigung wird nach Abschluss der Konfiguration automatisch ausgelöst; der Versand startet ohne manuelle Aktion. | 3 |
| BEN-06 | Der Administrator wird informiert, wenn Angebot oder PDF nicht erstellt werden konnten. | Wenn die PDF- oder Angebotserstellung fehlschlägt, erhält der Administrator eine entsprechende Meldung. | 3 |
| BEN-04 | Der Benachrichtigungsstatus ist in der Verwaltungskonsole beim Lead sichtbar. | In der Leadansicht ist erkennbar, ob die Benachrichtigung gesendet wurde oder fehlgeschlagen ist. | 4 |
| BEN-05 | Dringende Leads werden in der Benachrichtigung hervorgehoben. | Bei einem Lead mit hoher Priorität enthält die Benachrichtigung eine eindeutige Dringlichkeitskennzeichnung. | 4 |
| BEN-07 | Das System ist so aufgebaut, dass weitere Kanäle (Push, SMS, WhatsApp) ohne Umbau ergänzt werden können. | Ein zusätzlicher Benachrichtigungskanal kann über eine klar abgegrenzte Schnittstelle ergänzt werden, ohne bestehende Leadabläufe umzubauen. | 5 |
| BEN-08 | Benutzer wählen selbst, über welche Kanäle sie benachrichtigt werden. | Ein Benutzer kann seine gewünschten Ereignisse und Benachrichtigungskanäle speichern; nur diese Einstellungen werden berücksichtigt. | 5 |
| BEN-09 | Erinnerung bei Leads, die zu lange unbearbeitet bleiben. | Für einen länger unbearbeiteten Lead wird nach Ablauf der konfigurierten Zeit eine Erinnerung an den festgelegten Empfänger ausgelöst. | 5 |
| BEN-10 | Mindestens ein weiterer Kanal neben E-Mail (Push, SMS oder WhatsApp). | Ein konfigurierter zusätzlicher Kanal neben E-Mail kann eine Testbenachrichtigung erfolgreich zustellen. | 5 |
 
# Nicht-funktionale Anforderungen
Jede Anforderung hat Akzeptanzkriterien, anhand derer überprüft wird, ob sie erfüllt ist.
 
## 8. Benutzbarkeit
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| BENUTZ-01 | Die Web-App funktioniert auf Smartphone, Tablet und Laptop. | Alle Seiten sind bei 360 px, 768 px und 1366 px Breite ohne waagrechtes Scrollen bedienbar; getestet in aktuellem Chrome, Firefox, Safari und Edge. | 2 |
| BENUTZ-02 | Der Konfigurator ist ein intuitiver Schritt-für-Schritt-Assistent. | 3 Testpersonen ohne Vorwissen schließen den Konfigurator ohne Hilfe ab; Ziel unter 10 Minuten. | 2 |
| BENUTZ-03 | Alle Texte für Endkunden sind auf Deutsch und ohne Fachbegriffe bzw. mit Erklärung. | Review der Texte durch den AG. | 3 |
 
## 9. Leistung
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| LEIST-02 | Die Kostenschätzung wird ohne spürbare Verzögerung berechnet. | Berechnung inkl. Antwort des Backends in unter 2 Sekunden; PDF-Erstellung in unter 5 Sekunden. | 2 |
| LEIST-01 | Seitenwechsel innerhalb des Konfigurators laden schnell. | Wechsel zur nächsten Frage in unter 1 Sekunde (gemessen im Browser, normale Breitbandverbindung). | 3 |
| LEIST-03 | Die Benachrichtigung erreicht die Empfänger zeitnah. | E-Mail wird innerhalb von 1 Minute nach dem Absenden an die vorgesehenen Empfänger verschickt. | 4 |
 
## 10. Sicherheit und Datenschutz
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| SICH-01 | Personenbezogene Daten werden verschlüsselt in der Datenbank gespeichert. | Datenbank bzw. betroffene Spalten sind verschlüsselt; Klartext ist ohne Schlüssel nicht lesbar. | 1 |
| SICH-02 | Die gesamte Kommunikation läuft verschlüsselt. | Nur HTTPS; Aufrufe über HTTP werden auf HTTPS umgeleitet. | 1 |
| SICH-03 | Passwörter der Benutzer werden nur gehasht gespeichert; die Anmeldung erfolgt über JWT. | Code-Review; kein Passwort im Klartext in der Datenbank. | 1 |
| SICH-04 | Das System ist gegen SQL-Injection und XSS geschützt. | Datenbankzugriff nur über Entity Framework Core; Texteingaben werden im Backend bereinigt; Test mit typischen Angriffseingaben. | 1 |
| SICH-05 | Die Verarbeitung entspricht der DSGVO. | Zustimmung zum Datenschutz wird mit Zeitpunkt gespeichert; Leads können auf Wunsch gelöscht werden; Testdaten sind anonymisiert. | 1 |
| SICH-06 | Logdateien enthalten keine echten Kundendaten. | Review der Logs nach einem Testdurchlauf. | 2 |
 
## 11. Zuverlässigkeit und Wartbarkeit
| Nr. | Anforderung | Akzeptanzkriterien | Prio |
| --- | ----------- | ------------------- | ---- |
| WART-01 | Die Datenbank wird täglich automatisch gesichert. | Täglicher verschlüsselter SQL-Dump auf einem separaten Speichermedium des AG; Wiederherstellung einmal erfolgreich getestet. | 2 |
| WART-03 | Der Code ist automatisch getestet. | Unit-, Integrations- und End-to-End-Tests laufen in der CI/CD-Pipeline bei jedem Merge auf main. | 3 |
| WART-02 | Fragen, Bausteine und Regeln sind ohne Änderung am Programmcode erweiterbar. | Eine neue Frage und ein neuer Baustein werden über die Verwaltungskonsole angelegt und funktionieren ohne neues Deployment. | 4 |

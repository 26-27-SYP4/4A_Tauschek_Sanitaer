# Risikodokument: Heizungskonfigurator

Stand: 04.11.2026  
Version: 0.1.0  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix

## Allgemeines

Die drei größten Risiken sind die Anbindung der fremden Firmendatenbank, das fehlende Fachwissen über Heizungen und eine zu knappe Zeitplanung, weil es unser erstes gemeinsames Projekt ist. Für alle drei sind vorbeugende Maßnahmen und ein Plan B festgelegt.

| Merkmal | Inhalt |
| --- | --- |
| Projekt | Heizungskonfigurator mit Kostenschätzung |
| Auftraggeber (AG) | Tauschek Sanitär Heizung GmbH |
| Projektteam | Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix |
| Grundlage | Fachliches Konzept, Version 0.2.0 |
| Version dieses Dokuments | 0.1.0 |

Ausgangslage des Teams: Erfahrung mit C# und Angular ist vorhanden. Neu für uns sind die Arbeit mit einer fremden Datenbank, die Zusammenarbeit über GitHub, das Erzeugen von PDFs, der automatische Mailversand und das Fachgebiet Heizungstechnik.

## Bewertungsschema

Jedes Risiko wird mit Eintrittswahrscheinlichkeit (W) und Auswirkung (A) auf einer Skala von 1 bis 3 bewertet. Der Risikowert ist W × A.

| Stufe | Eintrittswahrscheinlichkeit (W) | Auswirkung (A) |
| --- | --- | --- |
| 1 – gering | eher unwahrscheinlich | kleine Verzögerung, mit wenig Aufwand behebbar |
| 2 – mittel | kann gut passieren | spürbare Verzögerung oder Funktion nur eingeschränkt nutzbar |
| 3 – hoch | sehr wahrscheinlich | Meilenstein oder Kernfunktion gefährdet, Projektziel in Gefahr |

| Risikowert | Klasse | Umgang |
| --- | --- | --- |
| 6–9 | hoch | Maßnahmen sofort starten, bei jedem Teammeeting prüfen |
| 3–4 | mittel | Maßnahmen einplanen, bei jedem Meilenstein prüfen |
| 1–2 | niedrig | beobachten |

## Risikoübersicht

15 Risiken sind erfasst: 9 mit hohem und 6 mit mittlerem Risikowert. Sortiert nach Risikowert.

| ID | Risiko | Kategorie | W | A | Wert | Klasse | Status |
| --- | --- | --- | --- | --- | --- | --- | --- |
| R01 | Anbindung der fremden Firmendatenbank scheitert oder verzögert sich | Technik | 3 | 3 | 9 | hoch | Offen |
| R02 | Fehlendes Fachwissen führt zu falscher Bausteinauswahl und Kostenschätzung | Fachlich | 3 | 3 | 9 | hoch | Offen |
| R03 | Aufwand wird unterschätzt, Zeitplan nicht haltbar | Organisation | 3 | 3 | 9 | hoch | Offen |
| R04 | Anforderungen sind unklar oder ändern sich (Fragenkatalog noch in Arbeit) | Anforderungen | 3 | 2 | 6 | hoch | Offen |
| R05 | Probleme bei der Zusammenarbeit über GitHub | Organisation | 3 | 2 | 6 | hoch | Offen |
| R06 | Zugangsdaten (Datenbank, Mail) landen im GitHub-Repository | Sicherheit | 2 | 3 | 6 | hoch | Offen |
| R07 | Falsche oder veraltete Preise in der Kostenschätzung | Fachlich | 2 | 3 | 6 | hoch | Offen |
| R08 | Verstoß gegen den Datenschutz (DSGVO) bei Kontaktdaten | Recht | 2 | 3 | 6 | hoch | Offen |
| R09 | Fragenkatalog lässt sich nicht flexibel erweitern | Technik | 2 | 3 | 6 | hoch | Offen |
| R10 | PDF-Erzeugung ist aufwendiger als erwartet | Technik | 2 | 2 | 4 | mittel | Offen |
| R11 | Automatischer Mailversand funktioniert nicht zuverlässig | Technik | 2 | 2 | 4 | mittel | Offen |
| R12 | Einbindung in die Website und Hosting sind unklar | Technik | 2 | 2 | 4 | mittel | Offen |
| R13 | Missbrauch des öffentlichen Formulars (Spam, Angriffe) | Sicherheit | 2 | 2 | 4 | mittel | Offen |
| R14 | Ausfall eines Teammitglieds, Wissen nur bei einer Person | Organisation | 2 | 2 | 4 | mittel | Offen |
| R15 | Auftraggeber ist für Rückfragen schwer erreichbar | Organisation | 2 | 2 | 4 | mittel | Offen |

Mögliche Status: Offen, In Bearbeitung, Eingetreten, Geschlossen.

## Risiken im Detail

Jedes Risiko hat vorbeugende Maßnahmen, einen Plan B für den Fall, dass es eintritt, und ein Frühwarnzeichen, an dem wir es rechtzeitig erkennen.

### R01 – Anbindung der fremden Firmendatenbank

- **Ursache:** Aufbau, Datenbanksystem und Zugriffsweg der Firmendatenbank sind uns unbekannt. Leistung und Eignung der Bausteine stehen dort nicht drin, sie müssen ergänzt werden.
- **Auswirkung:** Ohne Bausteine keine Kostenschätzung; die Kernfunktion fehlt.
- **Vorbeugend:** Früh mit dem AG klären, welches System im Einsatz ist und ob ein Export (z. B. CSV) möglich ist. Nur lesend zugreifen, nie direkt in die Firmendatenbank schreiben. Eine eigene Import-Schicht bauen, die Felder der Firmendatenbank auf unsere Bausteine abbildet. Mit einem Beispielauszug entwickeln.
- **Plan B:** Bausteine vorerst manuell oder per CSV in die eigene Datenbank eintragen; der automatische Import folgt später.
- **Frühwarnzeichen:** Kein Datenauszug bis zum ersten Meilenstein.

### R02 – Fehlendes Fachwissen über Heizungen

- **Ursache:** Niemand im Team kennt sich mit Heizungstechnik aus (Wärmebedarf, Leistungsstufen, Vorlauftemperatur).
- **Auswirkung:** Falsche Regeln führen zu falschen Bausteinen und unrealistischen Preisen; der Konfigurator verliert das Vertrauen der Endkunden.
- **Vorbeugend:** Den Geschäftsführer als Fachexperten einbinden. Alle Regeln („Bedingung → Baustein“) vom AG formulieren und schriftlich bestätigen lassen. Ein Glossar der Fachbegriffe anlegen. Regeln konfigurierbar statt im Code speichern, damit der AG sie korrigieren kann. Ergebnisse mit früheren echten Angeboten des AG vergleichen.
- **Plan B:** Im Zweifel keine automatische Schätzung, sondern den Lead für persönliche Beratung markieren (im Konzept bereits vorgesehen).
- **Frühwarnzeichen:** Der AG korrigiert bei Testläufen viele Ergebnisse.

### R03 – Aufwand unterschätzt

- **Ursache:** Erstes gemeinsames Projekt, keine Erfahrungswerte für Schätzungen; daneben laufen Unterricht und Prüfungen.
- **Auswirkung:** Meilensteine werden verpasst, Funktionen fehlen bei der Abgabe.
- **Vorbeugend:** Arbeit in Meilensteine und kurze Iterationen teilen. Zuerst ein durchgängiges Grundgerüst bauen: Fragen → Bausteine → Preis → PDF → Mail. Muss- und Kann-Funktionen festlegen. Puffer einplanen und Zeiten erfassen, um Schätzungen zu verbessern.
- **Plan B:** Kann-Funktionen in Absprache mit dem AG und der Betreuungslehrkraft streichen oder verschieben.
- **Frühwarnzeichen:** Zwei Iterationen hintereinander werden nicht fertig.

### R04 – Unklare oder sich ändernde Anforderungen

- **Ursache:** Der Fragenkatalog und die Alternativantworten („Ich weiß nicht“) sind laut Konzept noch in Arbeit.
- **Auswirkung:** Bereits gebaute Teile müssen umgebaut werden.
- **Vorbeugend:** Anforderungen schriftlich festhalten und vom AG bestätigen lassen. Früh Mockups und Prototypen zeigen. Änderungen über die Version des Konzepts nachvollziehen.
- **Plan B:** Neue Wünsche in die nächste Iteration verschieben statt laufende Arbeit zu unterbrechen.
- **Frühwarnzeichen:** Bei Abstimmungen kommen häufig neue oder geänderte Fragen dazu.

### R05 – Zusammenarbeit über GitHub

- **Ursache:** Wir haben noch nie gemeinsam in einem Repository gearbeitet.
- **Auswirkung:** Merge-Konflikte, überschriebene Arbeit, ein nicht lauffähiger Hauptzweig.
- **Vorbeugend:** Zu Beginn einen gemeinsamen Übungsdurchlauf machen. Pro Funktion ein eigener Branch, Merge nur über Pull Request mit Review, Hauptzweig schützen. Klein und oft committen. Passende .gitignore für .NET und Angular (bin, obj, node_modules). Aufgaben als Issues auf einem Projektboard verwalten.
- **Plan B:** Fehlerhafte Änderungen mit git revert zurücknehmen; funktionierende Stände mit Tags markieren.
- **Frühwarnzeichen:** Wiederholte Merge-Konflikte oder Commits direkt auf den Hauptzweig.

### R06 – Zugangsdaten im Repository

- **Ursache:** Verbindungsdaten für Datenbank und Mailserver stehen schnell in einer Konfigurationsdatei, die mit committet wird.
- **Auswirkung:** Fremde könnten auf Firmendaten oder das Mailkonto des AG zugreifen.
- **Vorbeugend:** Zugangsdaten nie committen, sondern über .NET User Secrets bzw. Umgebungsvariablen laden. Konfigurationsdateien mit Geheimnissen in die .gitignore. Repository privat halten und Secret Scanning aktivieren.
- **Plan B:** Betroffene Passwörter sofort ändern lassen; nur die Git-Historie zu bereinigen reicht nicht.
- **Frühwarnzeichen:** Passwörter oder Verbindungsstrings tauchen in einem Pull Request auf.

### R07 – Falsche oder veraltete Preise

- **Ursache:** Preise kommen per Import und können fehlen, falsch übernommen werden oder veralten.
- **Auswirkung:** Endkunden erhalten unrealistische Erwartungen; das fällt auf den AG zurück.
- **Vorbeugend:** Preise als Spanne ausgeben und das PDF klar als unverbindliche Schätzung kennzeichnen. Datum des letzten Imports im PDF angeben. Beim Import prüfen: fehlender Preis oder Preis 0 setzt den Baustein auf inaktiv.
- **Plan B:** Betroffene Bausteine über das Merkmal „Aktiv“ sofort abschalten.
- **Frühwarnzeichen:** Der Import meldet Bausteine ohne Preis oder mit starken Preissprüngen.

### R08 – Datenschutz (DSGVO)

- **Ursache:** Wir speichern Kontaktdaten und Angaben zum Haus, also personenbezogene Daten.
- **Auswirkung:** Rechtliche Folgen und Imageschaden für den AG.
- **Vorbeugend:** Einwilligung per nicht vorangekreuzter Checkbox (im Konzept vorgesehen). Datenschutztext vom AG liefern lassen, nicht selbst formulieren. Nur nötige Daten erfassen und Löschfristen mit dem AG festlegen. Daten nur verschlüsselt übertragen (HTTPS).
- **Plan B:** Funktion erst online stellen, wenn der AG die Datenschutztexte freigegeben hat.
- **Frühwarnzeichen:** Kurz vor dem Go-live fehlt noch die Datenschutzerklärung.

### R09 – Fragenkatalog nicht flexibel erweiterbar

- **Ursache:** Fragen, Antworten und Folgefragen werden leicht fest im Angular-Code eingebaut.
- **Auswirkung:** Jede Änderung des Fragenkatalogs braucht einen Programmierer; die Anforderung aus dem Konzept ist verfehlt.
- **Vorbeugend:** Fragen, Antwortoptionen, Bedingungen für Folgefragen und Regeln in der Datenbank ablegen. Das Formular dynamisch aus dieser Konfiguration aufbauen. Früh einen Prototyp mit zwei, drei Fragetypen bauen.
- **Plan B:** Den Fragenkatalog zuerst als JSON-Konfiguration führen; die Bearbeitung in der Verwaltungskonsole folgt später.
- **Frühwarnzeichen:** Eine neue Frage erfordert Änderungen an mehreren Stellen im Code.

### R10 – PDF-Erzeugung

- **Ursache:** Wir haben noch nie PDFs erzeugt.
- **Auswirkung:** Verzögerung; das PDF sieht unprofessionell aus.
- **Vorbeugend:** Früh einen kleinen technischen Test (Spike) machen. Eine Bibliothek für .NET wählen (z. B. QuestPDF oder PDFsharp) und die Lizenzbedingungen für den Einsatz beim AG prüfen. Layout, Logo und Pflichtangaben mit dem AG abstimmen.
- **Plan B:** Schlichtes Layout zuerst, Gestaltung später verbessern.
- **Frühwarnzeichen:** Der Spike liefert nach einer Woche noch kein brauchbares PDF.

### R11 – Automatischer Mailversand

- **Ursache:** Kein Know-how zu SMTP; Zugang zum Mailserver des AG fehlt; Mails können im Spam landen.
- **Auswirkung:** Der AG erfährt nichts von neuen Leads.
- **Vorbeugend:** Mailzugang und Absenderadresse früh mit dem AG klären. In der Entwicklung einen lokalen Test-Mailserver verwenden (z. B. smtp4dev). Den Lead immer zuerst in der Datenbank speichern und dann mailen; fehlgeschlagene Mails erneut versuchen.
- **Plan B:** Leads sind in der Leadverwaltung sichtbar, auch wenn eine Mail nicht ankommt.
- **Frühwarnzeichen:** Testmails landen im Spam oder kommen nicht an.

### R12 – Einbindung in die Website und Hosting

- **Ursache:** Unklar ist, wer die Website des AG betreibt und ob dort ein .NET-Backend laufen kann.
- **Auswirkung:** Fertige Software kann am Ende nicht in Betrieb gehen.
- **Vorbeugend:** Früh klären, wer die Website betreut, wo das Backend läuft und wie der Konfigurator eingebunden wird (Unterseite, Link oder iframe). Eine erste Version früh auf einem Testserver veröffentlichen, nicht erst zum Schluss.
- **Plan B:** Den Konfigurator auf einer eigenen Subdomain betreiben und von der Website darauf verlinken.
- **Frühwarnzeichen:** Bis zur Hälfte der Projektzeit gibt es keinen Testserver.

### R13 – Missbrauch des öffentlichen Formulars

- **Ursache:** Das Formular ist für alle im Internet erreichbar.
- **Auswirkung:** Spam-Leads, Mail-Flut beim AG, im schlimmsten Fall Angriffe auf die Datenbank.
- **Vorbeugend:** Alle Eingaben serverseitig prüfen. Nur parametrisierte Abfragen bzw. Entity Framework verwenden (Schutz vor SQL-Injection). Rate Limiting im ASP.NET-Core-Backend und ein Honeypot-Feld oder Captcha gegen Bots. Eingaben in Mails und PDFs escapen.
- **Plan B:** Captcha nachrüsten, auffällige Leads herausfiltern.
- **Frühwarnzeichen:** Viele Anfragen in kurzer Zeit oder sinnlose Eingaben.

### R14 – Ausfall eines Teammitglieds

- **Ursache:** Krankheit oder Prüfungsphasen; Wissen über einen Teil liegt nur bei einer Person.
- **Auswirkung:** Ein Teil des Projekts steht still.
- **Vorbeugend:** Code-Reviews und Pair-Programming, damit mindestens zwei Personen jeden Teil kennen. README mit Setup-Anleitung im Repository. Aufgaben für alle sichtbar auf dem Projektboard.
- **Plan B:** Aufgaben umverteilen, Umfang reduzieren.
- **Frühwarnzeichen:** Nur eine Person kann einen Teil des Systems starten oder erklären.

### R15 – Erreichbarkeit des Auftraggebers

- **Ursache:** Der AG führt einen Betrieb und hat wenig Zeit für Rückfragen.
- **Auswirkung:** Fachliche Entscheidungen bleiben offen, die Arbeit stockt.
- **Vorbeugend:** Eine feste Ansprechperson und regelmäßige Termine vereinbaren. Fragen sammeln statt einzeln zu stellen. Entscheidungen protokollieren.
- **Plan B:** Mit dokumentierten Annahmen weiterarbeiten und diese später bestätigen lassen.
- **Frühwarnzeichen:** Offene Fragen bleiben länger als eine Woche unbeantwortet.

## Risikomatrix

Kein Risiko liegt im niedrigen Bereich: Neun Risiken sind hoch, sechs mittel. Die rechte obere Ecke (R01 bis R03) hat bei jedem Teammeeting Vorrang.

| W \ A | 1 – gering | 2 – mittel | 3 – hoch |
| --- | --- | --- | --- |
| **3 – hoch** | Wert 3 (mittel): – | Wert 6 (hoch): R04, R05 | Wert 9 (hoch): **R01, R02, R03** |
| **2 – mittel** | Wert 2 (niedrig): – | Wert 4 (mittel): R10, R11, R12, R13, R14, R15 | Wert 6 (hoch): R06, R07, R08, R09 |
| **1 – gering** | Wert 1 (niedrig): – | Wert 2 (niedrig): – | Wert 3 (mittel): – |

Zeilen: Eintrittswahrscheinlichkeit (W), Spalten: Auswirkung (A).

## Überwachung und Aktualisierung

Das Risikodokument ist ein lebendes Dokument und wird während des ganzen Projekts gepflegt.

- **Jedes Teammeeting:** hohe Risiken und ihre Frühwarnzeichen kurz durchgehen, Status in der Übersicht aktualisieren.
- **Jeder Meilenstein:** alle Risiken neu bewerten, Maßnahmen prüfen, neue Risiken aufnehmen.
- **Neue Risiken** bekommen die nächste freie ID. Erledigte Risiken werden nicht gelöscht, sondern auf „Geschlossen“ gesetzt.
- **Eingetretene Risiken:** Plan B starten und im Teammeeting festhalten, was wir daraus lernen.

### Änderungsverlauf

| Version | Datum | Änderung |
| --- | --- | --- |
| 0.1.0 | 04.11.2026 | Erstfassung mit 15 Risiken |

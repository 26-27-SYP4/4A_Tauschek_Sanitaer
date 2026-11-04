# Fachliches Konzept: Heizungskonfigurator
Version: 0.3.0  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH  
AG bedeutet im nächsten Abschnitt: Auftraggeber  
Endkunde bedeutet im nächstem Abschnitt: Kunde des Ag
## Ziel des Projekts
Ziel des Projekts ist die Entwicklung eines Heizungskonfigurators mit integrierter Kostenschätzung. Dieser soll potenziellen Endkunden ermöglichen, eine passende Heizung zu konfigurieren und eine erste Einschätzung der zu erwartenden Kosten zu erhalten. Dadurch kann der AG bereits im Vorfeld das Interesse der potenziellen Käufer besser einschätzen und gezielt auf interessierte Endkunden zugehen.
## Wer wird das System verwenden?
Der Heizungskonfigurator wird von den Endkunden der Firma verwendet. Die Leadverwaltungssoftware wird von den Geschäftsführern, Mitarbeiter und Administratoren der Firma genutzt.
## Ist-Zustand
Endkunde geht in die Firma wenn er Heizung kaufen will. AG stellt Fragen zur Heizung, wie es aktuell ist und was sich der Endkunde vorstellt. AG schreibt selbst Angebot, sie gehen das Angebot dann gemeinsam nochmal durch und wenn alles passt wird der Auftrag bestätigt und die Heizung montiert.
## Soll-Zustand
Wenn der Endkunde eine Heizung will, geht er auf die Website der Firma, wo es einen neuen Abschnitt “Heizungskonfigurator” gibt. Der Endkunde kann beim Heizungskonfigurator bequem eingeben, was er will. Es wird automatisch ein PDF generiert, indem die Kostenschätzung enthalten ist. Gleichzeitig wird eine Mail an den AG gesendet und der Lead auf einer Verwaltungsseite gespeichert
## Fragenkatalog
Für die Entwicklung des Heizungskonfigurators wurde vom Geschäftsführer ein Fragenkatalog zur Verfügung gestellt. Dieser dient als Grundlage für die Gestaltung des Konfigurators und legt fest, welche Informationen von den Endkunden abgefragt werden sollen.

Der Fragenkatalog umfasst mehrere Schritte, in denen Informationen zum bestehenden Heizsystem, zum Gebäude, zum Warmwasserbedarf und zum gewünschten neuen Heizsystem erhoben werden. Je nach Auswahl werden zusätzliche Fragen eingeblendet, um die Anforderungen und Wünsche der Endkunden möglichst genau zu erfassen.

Am Ende des Konfigurationsprozesses geben die Endkunden ihre Kontaktdaten bekannt und stimmen den Datenschutzbestimmungen zu. Die gesammelten Informationen dienen als Grundlage für eine erste Kostenschätzung und die anschließende Bearbeitung der Kundenanfragen.

Der Konfigurator muss flexibel in der Erweiterbarkeit der Fragen sein, damit bestehende Fragen angepasst, entfernt oder neue Fragen hinzugefügt werden können. Für Fragen, die Endkunden möglicherweise nicht beantworten können, müssen geeignete alternative Antwortmöglichkeiten gefunden werden. An diesen Anpassungen wird derzeit noch gearbeitet.

## Bausteine
Die Kostenschätzung wird aus standardisierten Bausteinen zusammengesetzt. Ein Baustein ist ein konkretes, vordefiniertes Produkt oder Leistungspaket aus dem Sortiment des AG, zum Beispiel eine Luft-Wärmepumpe mit bestimmter Heizleistung, die für ein großes Einfamilienhaus geeignet ist. Das System wählt anhand der Antworten des Endkunden passende Bausteine aus und zählt ihre Preise zu einem groben Angebot zusammen.

Die Bausteine werden aus der Firmendatenbank des AG in eine eigene Datenbank übernommen und automatisch zusammengeführt.

### Merkmale eines Bausteins
| Merkmal | Beschreibung | Beispiel |
| ------- | ------------ | -------- |
| Bezeichnung | Name, wie er im Angebot erscheint | Luft-Wärmepumpe für große Einfamilienhäuser |
| Kategorie | Art des Bausteins | Wärmeerzeuger |
| Technologie | genauere Bauart | Wärmepumpe, Wärmequelle Luft |
| Heizleistung (kW) | Nennleistung bzw. Leistungsbereich | Wert aus der Firmendatenbank |
| Eignung | für welche Gebäude der Baustein passt, z. B. Wärmebedarf von–bis | großes Haus, gut gedämmt |
| Hersteller und Modell | optional, für den AG | laut Firmendatenbank |
| Preis (€) | Richtpreis für das grobe Angebot | laut Firmendatenbank |
| Artikelnummer | Verweis auf den Artikel in der Firmendatenbank | laut Firmendatenbank |
| Aktiv | ob der Baustein im Konfigurator verwendet wird | ja / nein |

### Kategorien
| Kategorie | Beispiele | Im Angebot |
| --------- | --------- | ---------- |
| Wärmeerzeuger | Wärmepumpe (Luft, Erdreich, Grundwasser), Pelletkessel, Kombikessel; jeweils in mehreren Leistungsstufen | immer genau einer |
| Speicher | Pufferspeicher, Warmwasserspeicher in verschiedenen Größen | je nach Heizsystem und Warmwasserbedarf |
| Zubehör | Heizkreisgruppen, Pelletlager, Kaminsanierung, Solaranbindung | je nach Gebäude und Wünschen |
| Dienstleistungen | Montage und Inbetriebnahme, Demontage der Altanlage, Öltank-Entsorgung | je nach Ausgangssituation |

### Vom Kriterium zum Baustein
Aus den Antworten des Endkunden entstehen Kriterien wie Gebäudegröße, Wärmebedarf, gewünschtes Heizsystem, Warmwasserbedarf und Ausgangssituation (Neubau oder Sanierung). Wenn der Endkunde seine Daten eingibt, ist also bekannt, was der Endkunde angibt, aber noch nicht, welche Bausteine benötigt werden. Diese ermittelt das System in folgenden Schritten:

1. **Wärmebedarf abschätzen:** aus bekanntem Verbrauch oder aus Fläche und Gebäudezustand.
2. **Wärmeerzeuger wählen:** den Baustein der gewünschten Technologie, dessen Eignungsbereich zum Wärmebedarf passt.
3. **Zusatzbausteine ergänzen:** über Regeln der Form „Bedingung → Baustein, Menge“.
4. **Preis berechnen:** Preise aller Bausteine summieren und als Preisspanne ausgeben.
5. **Annahmen und Hinweise** ins PDF übernehmen.

Beispiele für Regeln:

| Bedingung | Baustein |
| --------- | -------- |
| Heizsystem Biomasse | Pufferspeicher passend zur Kesselleistung |
| Warmwasser soll über die neue Heizung laufen | Warmwasserspeicher passend zur Personenzahl |
| Sanierung mit bestehender Heizung | Demontage der Altanlage |
| Mehrere Heizkreise | eine Heizkreisgruppe je Heizkreis |

### Sonderfälle
- **Unbekannte Angaben** („Ich weiß nicht“): Das System verwendet einen Standardwert und weist ihn im PDF als Annahme aus.
- **Kein passender Baustein**, etwa bei sehr hohem Wärmebedarf oder einem Sonderwunsch: Es wird kein automatisches Angebot erstellt; der Lead wird für eine persönliche Beratung markiert.
- **Technisch nur bedingt passende Kombinationen**, etwa eine Wärmepumpe bei hoher Vorlauftemperatur: Das Angebot enthält einen Hinweis, dass eine Prüfung vor Ort nötig ist.

### Pflege der Bausteine
Artikel und Preise werden per Import aus der Firmendatenbank übernommen. Leistung, Eignung und Regeln pflegt der Administrator in der Verwaltungskonsole, ohne Programmierung. Der Import läuft regelmäßig, damit die Preise aktuell bleiben.

## Leadverwaltung

### Entstehung eines Leads
Ein Lead entsteht automatisch, wenn der Kunde den Konfigurator absendet. Er enthält:
- Kontaktdaten des Kunden und die Zustimmung zum Datenschutz
- alle Antworten aus dem Fragenkatalog
- die ermittelten Bausteine und die Kostenschätzung
- Zeitpunkt der Anfrage
- Status, zuständigen Mitarbeiter und Notizen des AG

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
- alle Antworten des Kunden und die gewählten Bausteine
- das grobe Angebot als PDF
- Kontaktdaten mit Schnellaktionen (anrufen, E-Mail schreiben)
- Statusverlauf und Notizen
- Hinweis, ob der Kunde bereits Bestandskunde ist
- Benachrichtigungsstatus (wann und wie wurde der AG informiert)

### Vorsortierung nach Interesse
Der AG möchte erkennen, welche Kunden wirklich Interesse haben. Das System kennzeichnet jeden Lead mit einer Priorität (hoch, mittel, niedrig) anhand nachvollziehbarer Merkmale, zum Beispiel:
- gewünschter Umsetzungszeitraum (kurzfristig = höher)
- Vollständigkeit der Angaben und Kontaktdaten
- ob ein automatisches Angebot erstellt werden konnte
- Sonderwunsch oder Beratungsbedarf

Die Priorität ist nur ein Vorschlag; der AG kann sie jederzeit ändern. Die Merkmale und ihre Gewichtung werden mit dem AG festgelegt und sollen später anpassbar sein.

### Bestandskunden
Das System prüft beim Eingang eines Leads, ob der Kunde bereits in der Firmendatenbank des AG vorkommt (Abgleich z. B. über E-Mail, Name und Adresse). Ein Treffer wird in Liste und Detailansicht angezeigt. Der Zugriff auf die Firmendatenbank erfolgt nur lesend. Wie sicher ein Treffer ist, wird dem Benutzer angezeigt, da ein Abgleich nicht immer eindeutig ist.

### Anforderungen Leadverwaltung
Priorität nach MoSCoW: M = Must, S = Should, C = Could.

| Nr. | Anforderung | Prio |
| --- | ----------- | ---- |
| LV-01 | Jede abgeschlossene Konfiguration wird automatisch als Lead mit allen Antworten, Bausteinen und Kostenschätzung gespeichert. | M |
| LV-02 | Die Verwaltungskonsole ist nur nach Anmeldung erreichbar. | M |
| LV-03 | Der Zugriff richtet sich nach der Rolle des Benutzers (Geschäftsführer, Mitarbeiter, Administrator). | M |
| LV-04 | Benutzer sehen alle für sie freigegebenen Leads in einer Liste mit Sortierung, Filter und Suche. | M |
| LV-05 | Jeder Lead hat einen Status, der manuell geändert werden kann; Änderungen werden protokolliert. | M |
| LV-06 | Die Detailansicht zeigt alle Angaben des Kunden, die Bausteine und das PDF. | M |
| LV-07 | Leads können einem Mitarbeiter zugewiesen werden. | S |
| LV-08 | Mitarbeiter können Notizen zu einem Lead erfassen. | S |
| LV-09 | Das System schlägt für jeden Lead eine Priorität vor; der AG kann sie ändern. | S |
| LV-10 | Das System zeigt an, ob der Kunde bereits Bestandskunde ist. | S |
| LV-11 | Beim Status „Verloren“ kann ein Grund erfasst werden. | C |
| LV-12 | Einfache Auswertung (Anzahl Leads, Anteil gewonnen/verloren) für den Geschäftsführer. | C |
| LV-13 | Leads lassen sich als Datei exportieren (z. B. CSV). | C |

## Benachrichtigungssystem

### Auslöser
| Ereignis | Empfänger | Prio |
| -------- | --------- | ---- |
| Neuer Lead ist eingegangen | alle Benutzer mit aktivierter Benachrichtigung | M |
| Lead mit hoher Priorität (z. B. kurzfristiger Umsetzungswunsch) | wie oben, mit Hinweis „dringend“ | S |
| Angebot oder PDF konnte nicht erstellt werden | Administrator | S |
| Lead ist länger als eine festgelegte Zeit unbearbeitet | zuständiger Mitarbeiter oder Geschäftsführer | C |

### Inhalt einer Benachrichtigung
Die Nachricht ist kurz und nennt nur das Nötige, zum Beispiel: „Neue Anfrage von Max Mustermann: Wärmepumpe, Kostenschätzung ca. 25.000 €.“ Die Details stehen in der Verwaltungskonsole und sind über einen Link erreichbar. Die E-Mail enthält mehr Details (Kontaktdaten, wichtigste Angaben, Kostenschätzung, Bestandskunden-Hinweis). Über Kanäle, die Dritte mitlesen könnten (SMS, Push auf dem Sperrbildschirm), werden möglichst wenige personenbezogene Daten übertragen.

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

### Anforderungen Benachrichtigungssystem
| Nr. | Anforderung | Prio |
| --- | ----------- | ---- |
| BN-01 | Bei jedem neuen Lead wird der AG per E-Mail benachrichtigt (Kunde, Kontakt, wichtigste Angaben, Kostenschätzung, Link zum Lead). | M |
| BN-02 | Die Benachrichtigung wird automatisch und ohne Verzögerung nach Abschluss der Konfiguration ausgelöst. | M |
| BN-03 | Ein Lead wird gespeichert, auch wenn der Versand fehlschlägt; fehlgeschlagene Versuche werden wiederholt und protokolliert. | M |
| BN-04 | Der Benachrichtigungsstatus ist in der Verwaltungskonsole beim Lead sichtbar. | S |
| BN-05 | Dringende Leads werden in der Benachrichtigung hervorgehoben. | S |
| BN-06 | Der Administrator wird informiert, wenn Angebot oder PDF nicht erstellt werden konnten. | S |
| BN-07 | Das System ist so aufgebaut, dass weitere Kanäle (Push, SMS, WhatsApp) ohne Umbau ergänzt werden können. | S |
| BN-08 | Benutzer wählen selbst, über welche Kanäle sie benachrichtigt werden. | C |
| BN-09 | Erinnerung bei Leads, die zu lange unbearbeitet bleiben. | C |
| BN-10 | Mindestens ein weiterer Kanal neben E-Mail (Push, SMS oder WhatsApp). | C |
>>>>>>> d247e309c6bef1512f646a7fae193efbfbc8121e

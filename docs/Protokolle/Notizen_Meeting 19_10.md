# Meeting 19.10.26

# Datein
- Anforderungen.md durchgehen
- Fachliches_Konzept.md besprechen
- Naechste_Schritte.md durchgehen

# Fragen
- Welche genaue Formel oder Logik steckt hinter der Preisberechnung für die Kostenschätzung?
- Können Sie uns 2-3 konkrete Fallbeispiele durchrechnen (z. B. typisches Einfamilienhaus vs. Altbau), damit wir die Parameter validieren können?
- Wie sollen die Informationen in der Verwaltungssoftware angezeigt werden?

# Gewonnene Infos
Auf der Website soll es einen Heizungskonfigurator  geben zur kesselauswahl, mit ein paar Fragen(Fragen werden von Herr Tauschek zur Verfügung gestellt). Email soll zur verständigung an die firma gehen, Kunde xy hat angefragt...
- 1.Datenbank = Kundenanfrage
- 2.Datenbank aus Bausteine: bsp.: Pelletsheizung, Pufferspeicher usw... 
- 3.Zugriff auf Firmendatenbank
bausteine zusammenzählen und angebot generieren. Grobes Angebot in kürze an Kunden geschickt mit den verschiedenen Bausteinen
Man möchte vorsortieren welcher Kunde am Ende wirkliches Interesse hat.

Müssen schauen ob es getestet, sicher ist, wenn mit Ki gearbeitet wird.
Website soll flexibel sein, leicht erweiterbar sein
Fragen sollen auf einer Konfigurationsbasis erstellt/bearbeitet werden, um auch zukünftig eventuell Fragen hinzufügen zu können
Daten von firmendatenbank exportieren auf eigene Datenbank, bausteine sollen automatisch zusammengeführt werden.
Kunde möchte eine Verwaltungskonsole sehen können im Hintergrund,
möchte auch benachrichtigt werden nach dem der Lead den Heizungskonfigurator benutzt hat.

Wenn Kunde die Daten eingibt, wissen wir was der Kunde gibt, wissen aber noch nicht welche Bausteine wir brauchen.
Unterschiedliche Rollen, Status für die Leads, eigene UI für die Verwaltung der Leads
Automatische Energieberatung mit aktuellen Tagespreisen soll zum Groben Angebot mitgeschickt werden, 
Kunde bekommt das Grobe Angebot via Pdf
Bei Testdaten schauen dass es anonymisierte Daten sind.
Sehen können ob ein Kunde bereits ein bestehender Kunde ist.

## Nächster Schritt:
- Alles was wir neu erfahren haben in ein Fachkonzept fassen
- Ist-Zustand, Kunde macht alles Manuell, ist im Grunde eine Digitalisierung
- Fachkonzept mal von der KI erstellen lassen und das dann genauer überarbeiten

# Meeting 19.10.26

# Dateien
- `../Anforderungen.md` durchgehen
- `../Fachliches_Konzept.md` besprechen
- `Naechste_Schritte.md` durchgehen (Datei im Projektbestand nicht vorhanden; Ablage bzw. Ersatz klären).

# Fragen
- Welche genaue Formel oder Logik steckt hinter der Preisberechnung für die Kostenschätzung?
- Können Sie uns 2-3 konkrete Fallbeispiele durchrechnen (z. B. typisches Einfamilienhaus vs. Altbau), damit wir die Parameter validieren können?
- Wie sollen die Informationen in der Verwaltungssoftware angezeigt werden?

# Gewonnene Infos
Auf der Website soll es einen Heizungskonfigurator geben zur Kesselauswahl, mit ein paar Fragen (Fragen werden von Herrn Tauschek zur Verfügung gestellt). E-Mail soll zur Verständigung an die Firma gehen, Kunde xy hat angefragt...
- 1.Datenbank = Kundenanfrage
- 2.Datenbank aus Bausteinen: z. B.: Pelletsheizung, Pufferspeicher usw... 
- 3.Zugriff auf Firmendatenbank
Bausteine zusammenzählen und Angebot generieren. Grobes Angebot in Kürze an Kunden geschickt mit den verschiedenen Bausteinen
Man möchte vorsortieren, welcher Kunde am Ende wirkliches Interesse hat.

Müssen schauen, ob es getestet und sicher ist, wenn mit KI gearbeitet wird.
Website soll flexibel sein, leicht erweiterbar sein
Fragen sollen auf einer Konfigurationsbasis erstellt/bearbeitet werden, um auch zukünftig eventuell Fragen hinzufügen zu können
Daten von Firmendatenbank exportieren auf eigene Datenbank, Bausteine sollen automatisch zusammengeführt werden.
Kunde möchte eine Verwaltungskonsole sehen können im Hintergrund,
möchte auch benachrichtigt werden nachdem der Lead den Heizungskonfigurator benutzt hat.

Wenn Kunde die Daten eingibt, wissen wir, was der Kunde angibt, wissen aber noch nicht welche Bausteine wir brauchen.
Unterschiedliche Rollen, Status für die Leads, eigene UI für die Verwaltung der Leads
Automatische Energieberatung mit aktuellen Tagespreisen soll zum groben Angebot mitgeschickt werden, 
Kunde bekommt das grobe Angebot via PDF
Bei Testdaten schauen, dass es anonymisierte Daten sind.
Sehen können, ob ein Kunde bereits ein bestehender Kunde ist.

## Nächster Schritt:
- Alles, was wir neu erfahren haben in ein Fachkonzept fassen
- Ist-Zustand, Kunde macht alles manuell, ist im Grunde eine Digitalisierung
- Fachkonzept mal von der KI erstellen lassen und das dann genauer überarbeiten

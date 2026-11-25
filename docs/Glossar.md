# Glossar
Version: 0.2.0  
Datum: 18.11.2026  
Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix  
Auftraggeber: Tauschek Sanitär Heizung GmbH

Begriffe, die in allen Projektdokumenten einheitlich verwendet werden. Alphabetisch sortiert.

| Begriff | Bedeutung |
| ------- | --------- |
| Antworttyp | Art der Antwort auf eine Frage: Auswahl (Enum), Zahl (Number) oder Ja/Nein (Boolean). Freitextfragen gibt es nicht. |
| Auftraggeber (AG) | Geschäftsführer der Tauschek Sanitär Heizung GmbH |
| Bedingung | Vergleich der Antwort auf eine Frage mit einem Wert, z. B. „heizsystem ist gleich Biomasse“. Bedingungen werden in Gruppen mit UND, zwischen den Gruppen mit ODER verknüpft. |
| Endkunde | Der Kunde unseres Kunden; verwendet den Heizungskonfigurator |
| Fester Key | Key, der für den Ablauf zwingend gebraucht wird (z. B. gewünschtes Heizsystem, Fläche). In jeder Version muss ihn genau eine aktive Frage mit dem erwarteten Antworttyp befüllen. |
| Key | Name der Variable, in der die Antwort auf eine Frage gespeichert wird, z. B. „heizsystem“. Regeln beziehen sich auf Keys, nicht auf Fragen. In einer Version befüllt jeder Key genau eine Frage; eine neue Frage kann einen bestehenden Key übernehmen. |
| Kunde | Unser Kunde, die Tauschek Sanitär Heizung GmbH. Nicht zu verwechseln mit dem Endkunden. |
| KundenDB | Bestehende Datenbank der Firma, in der die Bausteine und Informationen über Kunden gespeichert sind. Auch „Firmendatenbank“ genannt. Zugriff nur lesend über eine API-Schnittstelle. |
| LeadDB | Datenbank der Verwaltungsapp, in der die Leads gespeichert werden |
| Regel | Bedingungen plus eine Aktion (z. B. Baustein hinzufügen, Folgefrage einblenden, Hinweis). Einzeln aktivierbar und deaktivierbar. |
| Verwaltungsapp | Interne Anwendung zur Leadverwaltung, nur nach Anmeldung erreichbar. Ihre Oberfläche wird im Fachlichen Konzept als „Verwaltungskonsole“ bezeichnet. |
| Version (des Fragenkatalogs) | Unveränderlicher Stand aller Fragen und Regeln, der beim Veröffentlichen eines Entwurfs entsteht. Hat eine fortlaufende Versionsnummer; jeder Lead verweist auf die Version, mit der er erstellt wurde. |

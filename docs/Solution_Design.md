# Solution Design
- Version 0.1.0
- Datum: 07.10.2026
- Autor(en): Geßwagner David, Tauschek Stefan, Weißenhofer Moritz, Wiesner Felix
- Auftraggeber: Tauschek Sanitär Heizung GmbH
## Applikations-Typ:
- Web-Applikation
## System Architektur:

## Software Architektur:
- Frontend-Backend-Datenbank

## Verwendete Technologien:
- Fronted: Angular, HTML, JS, TS, CSS
- Backend: C#
- Datenbank: SQL

## Konvention
- Konventionen in Konventionen.md festgelegt

## Persistierung
- Datenspeicherung: Alle generierten Leads werden relational in der Kundendatenbank persistiert.
- Backups: Da Kundendaten generiert werden, muss ein automatisierter täglicher SQL-Dump eingerichtet werden. (Backups müssen verschlüsselt und DSGVO-konform auf einem separaten Speichermedium des Unternehmens abgelegt werden).
## Sicherheit
- Authentifizierung: Kein Login für Kunden, stattdessen reCaptcha & Rate-Limiting gegen Bots. Installateur Login läuft später über JWT.
- Verschlüsselung: Kompletter Traffic über HTTPS. Datenbank verschlüsselt, Passwörter hashen.
- Logging: Fehler und Systemevents aufzeichnen, niemals echte Kundendaten ins Logfile schreiben
- App-Sicherheit:
    - Datenbank: Nur Entity Framework Core (ORM) nutzen, um SQL-Injections abzublocken
    - XSS: Alle Textfelder im Backend entschärfen
## Verwendete Programme:
- Visual Studio Code: Frontend, Datenbank
- Visual Studio: Backend
- Browser: Testen, KI

## Developer Test Strategie:
- Unit-Tests
- Integrationstests(Schnittstelle)
- End-to-End(Simuliert den echten User-Flow im Browser)
- CI/CD


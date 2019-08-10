# Gerontocracy

## Zuerst herunterladen von:

[NodeJS](https://nodejs.org/) Für Frontend Entwicklung  
[PostgreSQL](https://www.postgresql.org/download/) Für Datenbank  
[.Net Core SDK](https://dotnet.microsoft.com/download) Für Backend Entwicklung

## Einmalige weitere Vorbereitungen nachdem du ausgecheckt hast

Connection String in der appsettings.Development.json einfügen oder per Umgebungsvariable `ConnectionString__Gerontocracy=Host=localhost;Port=5432;Database=<DEIN-DB-NAME>;Username=<DEIN-USER>;Password=<DEIN-PASSWORT>` setzen.

## Vor dem Entwickeln folgendes ausführen

Konsolenbefehl `set ASPNETCORE_ENVIRONMENT=Development` ausführen oder Umgebungsvariable setzen  
Datenbank mit `dotnet ef database update` auf neuesten Stand bringen

## Während dem Entwickeln

Konsolenbefehl `npm start` um Frontend Dev-Server zu starten - das Frontend aktualisiert sich beim Speichern von selbst.

In Visual Studio wurde eine LaunchSettings.json mit SwaggerUI eingerichtet.
Es reicht die Applikation im Debug-Modus mit F5 zu starten

### Für die Backend Devs
Um eine Datenbankmigration anzulegen

1. Entities bearbeiten
2. `dotnet ef migrations add Migrationname` generiert eine Datenbank-Migration
3. `dotnet ef database update` bringt eure Datenbank auf den neuesten Stand

## Build

Dieser Schritt ist nur notwendig, um zu testen, ob ein Prod-Build möglich ist.  
Konsolenbefehl `ng build --prod` baut das Frontend und legt das Resultat in den wwwroot-Ordner.
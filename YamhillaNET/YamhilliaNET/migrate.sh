#!/bin/bash

# Generate two migrations, one for Postgres and one for SQLite
dotnet ef migrations add $1 --context PostgresYamhilliaContext --output-dir Migrations/PostgresMigrations

# Pushing through the update will fail if a foreign key is modified (known issue with SQLite)
# When this happens: https://stackoverflow.com/a/41942557
# IE: create new table with whatever key is throwing issue, copy contents from old table, drop old table (rename it would be safer)
# then rename the new table to the copied table
dotnet ef migrations add $1 --context DesignTimeSqliteContext --output-dir Migrations/SqliteMigrations
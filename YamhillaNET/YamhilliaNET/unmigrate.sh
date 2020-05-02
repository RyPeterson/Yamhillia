#!/bin/bash

# Undoes the last migration. LORD help us if one gets out of sync
dotnet ef migrations remove --context DesignTimePostrgresContext
dotnet ef migrations remove  --context DesignTimeSqliteContext
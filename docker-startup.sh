#!/bin/bash

./bundle --connection "User ID=postgres;Password=miva;Host=${DB_EVENT_HOST};Port=5432;Database=EventDB;Pooling=false; Connection Idle Lifetime=10; Max Auto Prepare=20;"
dotnet EventService.dll
#!/bin/bash

./bundle --connection "User ID=postgres;Password=miva;Host=${DB_ANALYSIS_HOST};Port=5432;Database=AnalysisDB;Pooling=false; Connection Idle Lifetime=10; Max Auto Prepare=20;"
dotnet AnalysisService.dll
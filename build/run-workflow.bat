@echo off

cd ..

echo ==== Install dependencies 📁
dotnet restore -v m

echo ==== Build 📦
dotnet build --configuration Release --no-restore /p:ContinuousIntegrationBuild=true

echo ==== Test 🧪
dotnet test --configuration Release --no-build

pause
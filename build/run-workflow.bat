@echo off

cd ..

echo ==== Install dependencies 📁
dotnet restore

echo ==== Build 📦
dotnet build --configuration Release --no-restore

echo ==== Test 🧪
dotnet test --configuration Release --no-build

pause
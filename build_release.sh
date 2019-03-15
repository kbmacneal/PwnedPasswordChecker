#! /bin/bash

rm -r -f bin

dotnet publish --self-contained=yes -r win10-x64

dotnet publish --self-contained=yes -r win10-x86

dotnet publish --self-contained=yes -r linux-x64

dotnet publish --self-contained=yes -r centos-x64

dotnet publish --self-contained=yes -r ubuntu.18.04-x64

dotnet publish --self-contained=yes -r ubuntu.16.04-x64

dotnet publish --self-contained=yes -r osx-x64


cd bin/Debug/netcoreapp2.2

zip -r win10-x64.zip win10-x64/*
zip -r win10-x86.zip win10-x86/*
zip -r linux-x64.zip linux-x64/*
zip -r centos-x64.zip centos-x64/*
zip -r ubuntu.18.04-x64.zip ubuntu.18.04-x64/*
zip -r ubuntu.16.04-x64.zip ubuntu.16.04-x64/*
zip -r osx-x64.zip osx-x64/*
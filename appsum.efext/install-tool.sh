#!/usr/bin/env bash
dotnet pack
dotnet tool uninstall --global appsum.efext
dotnet tool install --global --add-source ./nupkg appsum.efext 
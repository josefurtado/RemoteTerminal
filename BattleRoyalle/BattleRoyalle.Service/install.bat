set currentPath=%~dp0
echo %currentPath%
START /WAIT %currentPath%\bin\Debug\netcoreapp3.1\BattleRoyalle.Service.exe install
START /WAIT %currentPath%\bin\Debug\netcoreapp3.1\BattleRoyalle.Service.exe start
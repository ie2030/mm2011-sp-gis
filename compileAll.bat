@echo off
:: Необходимо прописать путь к csc.exe в PATH
mkdir Binaries
:: Компиляция IComponent.dll
csc /target:library /out:Binaries\IComponent.dll /optimize IComponent\Icomponent\*.cs
:: Компиляция DBComponent
csc /target:library /out:Binaries\DBComponent.dll /optimize /reference:Binaries\IComponent.dll DBComponent\DBComponent\*.cs
:: Компиляция AlgorithmComponent
csc /target:library /out:Binaries\AlgorithmComponent.dll /optimize /reference:Binaries\IComponent.dll,Binaries\DBComponent.dll AlgorithmComponent\AlgorithmComponent\*.cs
:: Компиляция StartServer
csc /out:Binaries\StartServer.exe /optimize /reference:Binaries\IComponent.dll StartServer\StartServer\*.cs
:: Копирование файла конфигурации
copy StartServer\StartServer\app.config Binaries\StartServer.exe.config
echo Done!

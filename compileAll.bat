@echo off
:: Необходимо прописать путь к csc.exe в PATH
:: Компиляция IComponent.dll
csc /target:library /out:IComponent.dll /optimize IComponent\Icomponent\*.cs
:: Компиляция DBComponent
csc /target:library /out:DBComponent.dll /optimize /reference:IComponent.dll DBComponent\DBComponent\*.cs
:: Компиляция AlgorithmComponent
csc /target:library /out:AlgorithmComponent.dll /optimize /reference:IComponent.dll,DBComponent.dll AlgorithmComponent\AlgorithmComponent\*.cs
:: Компиляция StartServer
csc /out:StartServer.exe /optimize /reference:IComponent.dll,DBComponent.dll StartServer\StartServer\*.cs
:: Копирование файла конфигурации
copy StartServer\StartServer\app.config StartServer.exe.config
echo Done!

@echo off
:: ���������� ��������� ���� � csc.exe � PATH
mkdir Binaries
:: ���������� IComponent.dll
csc /target:library /out:Binaries\IComponent.dll /optimize IComponent\Icomponent\*.cs
:: ���������� DBComponent
csc /target:library /out:Binaries\DBComponent.dll /optimize /reference:Binaries\IComponent.dll DBComponent\DBComponent\*.cs
:: ���������� AlgorithmComponent
csc /target:library /out:Binaries\AlgorithmComponent.dll /optimize /reference:Binaries\IComponent.dll,Binaries\DBComponent.dll AlgorithmComponent\AlgorithmComponent\*.cs
:: ���������� StartServer
csc /out:Binaries\StartServer.exe /optimize /reference:Binaries\IComponent.dll StartServer\StartServer\*.cs
:: ����������� ����� ������������
copy StartServer\StartServer\app.config Binaries\StartServer.exe.config
echo Done!

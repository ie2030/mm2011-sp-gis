@echo off
:: ���������� ��������� ���� � csc.exe � PATH
:: ���������� IComponent.dll
csc /target:library /out:IComponent.dll /optimize IComponent\Icomponent\*.cs
:: ���������� DBComponent
csc /target:library /out:DBComponent.dll /optimize /reference:IComponent.dll DBComponent\DBComponent\*.cs
:: ���������� StartServer
csc /out:StartServer.exe /optimize /reference:IComponent.dll,DBComponent.dll StartServer\StartServer\*.cs
:: ����������� ����� ������������
copy StartServer\StartServer\app.config StartServer.exe.config
echo Done!

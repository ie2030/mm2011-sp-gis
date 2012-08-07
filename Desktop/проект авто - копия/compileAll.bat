:: Compile Server Part
msbuild IComponent\IComponent\IComponent.csproj
msbuild DBComponent\DBComponent\DBComponent.csproj
msbuild AlgorithmComponent\AlgorithmComponent\AlgorithmComponent.csproj
msbuild StartServer\StartServer\StartServer.csproj

:: Compile Client Part
msbuild Maps\FlashAxLib\FlashAxLib\FlashAxLib.csproj
msbuild Maps\ExternalInterfaceProxy\ExternalInterfaceProxy.csproj
msbuild Maps\GoogleMapsFlashInWpf\GoogleMapsFlashInWpf\GoogleMapsFlashInWpf.csproj

pause
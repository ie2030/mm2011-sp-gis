:: Compile Server Part
msbuild IComponent\IComponent\IComponent.csproj
msbuild DBComponent\DBComponent\DBComponent.csproj
msbuild AlgorithmComponent\AlgorithmComponent\AlgorithmComponent.csproj
msbuild StartServer\StartServer\StartServer.csproj

:: Compile Client Part
msbuild Maps\GoogleMapsFlashInWpf\GoogleMapsFlashInWpf\GoogleMapsFlashInWpf.csproj

pause
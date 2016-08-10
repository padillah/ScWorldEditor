@ECHO OFF
SETLOCAL
SET sqName=ScWorldEdit

SET path=C:\sonarqube-6.0\MSBuild.SonarQube.Runner-2.1;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow;C:\Program Files (x86)\MSBuild\14.0\bin;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\;C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\BIN;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools;C:\Windows\Microsoft.NET\Framework\v4.0.30319;C:\Program Files (x86)\Microsoft Visual Studio 14.0\VC\VCPackages;C:\Program Files (x86)\HTML Help Workshop;C:\Program Files (x86)\Microsoft Visual Studio 14.0\Team Tools\Performance Tools;C:\Program Files (x86)\Windows Kits\10\bin\x86;C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\;%path%

SET sqKey=%sqName%Key
SET sqVersion=%1

msbuild.sonarQube.runner begin /n:%sqName% /k:%sqKey% /v:%sqVersion%

msbuild /t:rebuild /fl /p:SQProjectKey=%sqKey%

msbuild.sonarQube.runner end > SonarResults.log

#!/usr/bin/env bash

artifactsFolder="./artifacts"

echo "Looking if $artifactsFolder folder exists"
if [ -d $artifactsFolder ]; then
    echo "Deleting $artifactsFolder folder"
    rm -R $artifactsFolder
fi

echo 'Launching dotnet restore'
dotnet restore

echo 'Launching dotnet build'
dotnet build ./src/Devpro.TfsApi -c Release -f net452 --no-incremental
dotnet build ./test/Devpro.TfsApi.Lib.Tests -c Release -f net452

echo 'Launching test with mono'
mono \
./test/Devpro.TfsApi.Lib.Tests/bin/Release/net452/*/dotnet-test-xunit.exe \
./test/Devpro.TfsApi.Lib.Tests/bin/Release/net452/*/Devpro.TfsApi.Lib.Tests.dll

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "beta%04d" $revision)
echo "Revision is $revision"

echo 'Launching dotnet pack'
dotnet pack ./src/Devpro.TfsApi.Dto -c Release -o ./artifacts --version-suffix=$revision

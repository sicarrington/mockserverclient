#!/bin/bash

if [ -n "$COVERALLS_REPO_TOKEN" ]
then
  dotnet tool install coveralls.net --version 1.0.0 --tool-path tools
  ./tools/csmacnz.Coveralls --opencover -i ./mockserver.client.net.tests.unit/coverage.opencover.xml --useRelativePaths
fi
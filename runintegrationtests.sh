docker-compose -f ./integration-tests.docker-compose.yml up -d --build

bash -c 'while [[ "$(curl -s -o /dev/null -w ''%{http_code}'' localhost:1080)" != "404" ]]; do sleep 5; done'

dotnet test

docker-compose -f ./integration-tests.docker-compose.yml stop
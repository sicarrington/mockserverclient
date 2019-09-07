# docker-compose -f ./integration-tests.docker-compose.yml build

docker build -f integration-tests.Dockerfile -t tests .

docker-compose -f ./integration-tests.docker-compose.yml up -d

# docker run --network mockserverclient_integreationnetwork tests

dotnet test

docker-compose -f ./integration-tests.docker-compose.yml stop
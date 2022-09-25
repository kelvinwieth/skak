#!/bin/bash
docker ps -f name=skak -q | xargs --no-run-if-empty docker container stop
docker container ls -a -fname=skak -q | xargs -r docker container rm
docker run -d -e SKAK_TOKEN=$SKAK_TOKEN --name skak skak:latest
docker system prune -f

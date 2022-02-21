#! /bin/sh

sudo systemctl start mariadb
sudo systemctl start mysql
sudo systemctl start docker
sudo systemctl restart docker.service
docker container start  mongo-api

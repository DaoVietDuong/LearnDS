Guide
----consul
docker run --net my-net --name consul -d --hostname consul -p 8500:8500 consul
docker run --net my-net --name consul -d -p 8500:8500 consul

----rabbitmq
docker run --net my-net --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management

---fabio
docker run --net my-net --name fabio -d -p 9998:9998 -p 9999:9999 --env FABIO_REGISTRY_CONSUL_ADDR=consul:8500 --hostname fabio fabiolb/fabio


---redis

docker run --name my-redis -d redis
---
docker build -t  demo2-service:demo -f ./Dev.Dockerfile .
---
docker exec -ti <container-id> /bin/bash
apt-get update && apt-get install iputils-ping
apt-get update && apt-get install -y telnet
ping -w 4 <host-name>
--------------------
need to be in the same network
--

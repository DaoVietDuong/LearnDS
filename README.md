#Introduction  
Learn to build microservices  
Componenents  
Api Gateway  
Common 
Notification Service  
Order Service  
Display Service  
Management Service  

---

#How to setup  
----consul  
docker run --net my-net --name consul -d --hostname consul -p 8500:8500 consul  
docker run --net my-net --name consul -d -p 8500:8500 consul  

----rabbitmq  
docker run --net my-net --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management  

---fabio  
docker run --net my-net --name fabio -d -p 9998:9998 -p 9999:9999 --env FABIO_REGISTRY_CONSUL_ADDR=consul:8500 --hostname fabio fabiolb/fabio  

---redis  
docker run -it --net my-net --rm -p 6379:6379 redis redis-cli -h my-redis  
docker run -it -p 6379:6379 --name some-redis --rm  -d redis  

---mongo  
docker run --name mongo -d -p 27017:27017 mongo:4  
 
docker build -t  demo2-service:demo -f ./Dev.Dockerfile .  

docker exec -ti <container-id> /bin/bash  
apt-get update && apt-get install iputils-ping  
apt-get update && apt-get install -y telnet  
ping -w 4 <host-name>  

#need to be in the same network  


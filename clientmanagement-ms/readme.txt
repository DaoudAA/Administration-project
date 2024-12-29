docker run --name crm-clientmanagement -e MYSQL_ROOT_PASSWORD=mysql -e MYSQL_DATABASE=clients -p 3306:3306 -d mysql:latest
 for my sql 


dependencies : 
djangorestframework  
pip install kafka-python  
kafka-python
mysqlclient
django
theres an issue with the python kafka connector , the creator of the connecteur said to run  pip install git+https://github.com/dpkp/kafka-python.git  , sauce : https://github.com/dpkp/kafka-python/issues/2412   (works fine running the command, creator said should be fixed the noral pip install way) 


example of interaction being sent to  kafka prod : {"id": 1, "type": "phone_call", "remarque": "Client called to inquire about the service status.", "dateInteraction": "2024-12-27"} 
name of topic  client-interaction kafka port 9092 




 
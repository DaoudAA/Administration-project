from django.apps import AppConfig

from django.apps import AppConfig
import threading
#from .kafkaconsumers.interaction import kafka_consumer
class ClientmanagementConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    name = 'clientmanagement'
   # def ready(self):
        #threading.Thread(target=kafka_consumer, daemon=True).start()
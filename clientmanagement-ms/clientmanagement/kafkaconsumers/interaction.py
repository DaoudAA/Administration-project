# import threading

# from kafka import KafkaConsumer
# from django.apps import apps 
# import json
# from django.utils.timezone import now
# def save_interaction(message_value):
#     try:
    

#         interaction_data = json.loads(message_value)
 

#         client_id = interaction_data.get('id')   
#         interaction_type = interaction_data.get('type')   
#         remarque = interaction_data.get('remarque')   
#         date_interaction = interaction_data.get('dateInteraction', now().date())

         
#         Client = apps.get_model('clientmanagement', 'Client')
#         Interaction = apps.get_model('clientmanagement', 'Interaction')

#         if not (client_id and interaction_type):
#             print("Invalid interaction data: Missing required fields.")
#             return
 
#         try:
#             client = Client.objects.get(id=client_id)
#         except Client.DoesNotExist:
#             print(f"Client with ID {client_id} does not exist.")
#             return

 
#         Interaction.objects.create(
#             client=client,
#             dateInteraction=date_interaction,
#             type=interaction_type,
#             remarque=remarque
#         )
#         print(f"Saved interaction for client {client.nom} ({client.email}).")

#     except json.JSONDecodeError:
#         print("Failed to decode message: Invalid JSON format.")
#     except Exception as e:
#         print(f"Error processing interaction: {str(e)}")

# def kafka_consumer():
#     consumer = KafkaConsumer('client-interaction',
#                              group_id='my-group',
#                              bootstrap_servers=['localhost:9092'],
#                              auto_offset_reset='earliest')
#     for message in consumer:
#         print(f"{message.topic}:{message.partition}:{message.offset}: key={message.key} value={message.value.decode('utf-8')}")
#         save_interaction(message.value.decode('utf-8'))
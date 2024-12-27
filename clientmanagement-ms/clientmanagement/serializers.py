from rest_framework import serializers
from .models import Client, Interaction

class ClientSerializer(serializers.ModelSerializer):
    class Meta:
        model = Client
        fields = ['id', 'nom', 'email', 'telephone', 'adresse', 'dateInscription']

class InteractionSerializer(serializers.ModelSerializer):
    class Meta:
        model = Interaction
        fields = ['id', 'client', 'dateInteraction', 'type', 'remarque']

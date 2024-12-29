from django.shortcuts import render

from rest_framework.views import APIView
from rest_framework.response import Response
from rest_framework import status
from .models import Client, Interaction
from .serializers import ClientSerializer, InteractionSerializer

class ClientCreateAPIView(APIView):
    def post(self, request):
        serializer = ClientSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    def get(self, request, id=None):
        if id:
            try:
                client = Client.objects.get(id=id)
                serializer = ClientSerializer(client)
                return Response(serializer.data, status=status.HTTP_200_OK)
            except Client.DoesNotExist:
                return Response({"error": "Client not found."}, status=status.HTTP_404_NOT_FOUND)
        else:
            # For general queries
            name = request.query_params.get('name', None)
            email = request.query_params.get('email', None)

            clients = Client.objects.all()
            if name:
                clients = clients.filter(nom__icontains=name)
            if email:
                clients = clients.filter(email__icontains=email)

            serializer = ClientSerializer(clients, many=True)
            return Response(serializer.data)
    def getAll(self, request):
        clients = Client.objects.all()
        serializer = ClientSerializer(clients, many=True)
        return Response(serializer.data)
class InteractionCreateAPIView(APIView):
    def post(self, request):
        serializer = InteractionSerializer(data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data, status=status.HTTP_201_CREATED)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)
    
    def get(self, request):
        interaction_type = request.query_params.get('type', None)
        client_id = request.query_params.get('client_id', None)

        interactions = Interaction.objects.all()
        if interaction_type:
            interactions = interactions.filter(type__icontains=interaction_type)
        if client_id:
            interactions = interactions.filter(client_id=client_id)

        serializer = InteractionSerializer(interactions, many=True)
        return Response(serializer.data)
    def getAll(self, request):
         
        interactions = Interaction.objects.all()
        serializer = InteractionSerializer(interactions, many=True)
        return Response(serializer.data)
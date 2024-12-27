from django.db import models


class Client(models.Model):
    nom = models.CharField(max_length=100)
    email = models.EmailField()
    telephone = models.CharField(max_length=15)
    adresse = models.CharField(max_length=255)
    dateInscription = models.DateField()

    class Meta:
        db_table = "client"

 


class Interaction(models.Model):
    client = models.ForeignKey(Client, related_name='interactions', on_delete=models.CASCADE)
    dateInteraction = models.DateField()
    type = models.CharField(max_length=50)
    remarque = models.TextField()

    class Meta:
        db_table = "interaction"
 
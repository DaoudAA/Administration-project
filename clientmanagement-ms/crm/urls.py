"""
URL configuration for crm project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/5.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from clientmanagement.views import ClientCreateAPIView, InteractionCreateAPIView 
urlpatterns = [
    path('admin/', admin.site.urls), 
    path('clients/', ClientCreateAPIView.as_view(), name='create_client'),
    path('interactions/', InteractionCreateAPIView.as_view(), name='create_interaction'),
    path('clients/<int:id>/', ClientCreateAPIView.as_view(), name='get-client-by-id'),
    
]

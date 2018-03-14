# WS_REST_and_WS_SOAP
### Auteur : Antoine Dezarnaud - SI4 - G2

Ce petit projet a pour but de créer un service web intermédiaire entre un service web pour Velib ([JCDecaux API](https://developer.jcdecaux.com) ) et un client.
Le service web intermédiaire (contenu dans le dossier Service) expose une API SOAP.
Le client est une interface utilisateur permettant de chercher les stations disponibles par ville et afficher des infos les concernant.

## Extensions
* Interface utilisateur graphique pour le client
* Ajout d'un cache dans le service pour réduire les communications entre le service intermédiaire et l'api JCDecaux

# WS_REST_and_WS_SOAP
### Auteur : Antoine Dezarnaud - SI4 - G2

Ce petit projet a pour but de créer un service web intermédiaire entre un service web pour Velib ([JCDecaux API](https://developer.jcdecaux.com) ) et un client.
Le service web intermédiaire (contenu dans le dossier Service) expose une API SOAP.

Il existe 3 clients:
- un client console permettant de chercher les stations disponibles par ville et afficher des infos les concernant ainsi que de modifier la durée du cache du service.
- un client graphique avec une interface utilisateur permettant de faire la même chose sans modifier la durée de mise en cache.
- un client moniteur avec une interface graphique permettant de visualiser des données concernant le service (nombre de requêtes effectuées sur le service JCDecaux et sur le service intermédiaire, nombre de données en cache, temps moyen de réponse du service intermédiaire).

## Extensions
* Interface utilisateur graphique pour le client
* Ajout d'un cache dans le service pour réduire les communications entre le service intermédiaire et l'api JCDecaux
* Ajout d'une partie moniteur avec un client qui se connecte à la partie moniteur du service SOAP.
* Communication Event Based
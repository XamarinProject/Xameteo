# Xameteo

# Sabri Habib
# Alexandre Armando
# [30/03/2020 - *]

Technologies utilisées :
-	Xamarin forms
-	Shell navigation
- SQLite	

Environnement de développement :
-	Windows 10
-	Visual Studio 2019
-	Git flow
-	Gitkraken

Plugins utilisés :
-	FFImageLoading.Svg.Forms
- Microsoft.NET.Http
- Newtonsoft.Json

----------------------------------------------------------------------------------------------------------------------------

Thème :
Le thème global de l’application sera la météo et d’autres données liées à la ville. Le but étant de créer une application personnalisable en proposant à l’utilisateur de paramétrer ses villes favorites et d’obtenir rapidement les informations qui l’intéresse (tendance actuelle, température, heure, etc.). 
Structure et contenu :
L’application sera composée d’au moins 4 écrans :
-	Un écran d’accueil contenant les villes ajoutées comme favoris avec la possibilité d’éditer cette liste dynamiquement
-	Un écran de recherche avec des champs inputs personnalisés nativement
-	Un écran de détails présentant la météo et les informations (heure, jour/nuit, etc.) de la ville sélectionnée et la possibilité de sauvegarder la ville dans la liste en l’ajoutant aux favoris
-	Un écran de paramètre permettant de gérer les données enregistrées et les données à afficher (activation/désactivation des web services, prévisualisation sur l’écran d’accueil, tendance, température, humidité, etc.)

----------------------------------------------------------------------------------------------------------------------------

Navigation :
Nous utiliserons le Shell pour gérer la navigation entre l’écran d’accueil, la page de recherche et cette de paramètre. Nous proposerons donc un menu lors du swipe vers la droite.

----------------------------------------------------------------------------------------------------------------------------

Api(s) :
-	OpenWeatherMap ( https://openweathermap.org/current )
-	GeoDB ( http://geodb-free-service.wirefreethought.com/ )

----------------------------------------------------------------------------------------------------------------------------

Cas d’utilisations :
En tant qu’utilisateur, je veux pouvoir lancer l’application et arriver sur la page d’accueil afin de consulter les informations liées à mes villes favorites.
En tant qu’utilisateur, je veux pouvoir sélectionner une ville de ma liste afin de pouvoir consulter des détails sur sa météo actuelle et ses prévisions.
En tant qu’utilisateur, je veux pouvoir naviguer entre les pages via le menu afin de me permettre de découvrir les fonctionnalités.
En tant qu’utilisateur, je veux pouvoir rechercher une ville afin de pouvoir consulter sa météo.
En tant qu’utilisateur, je veux pouvoir ajouter une ville que j’ai recherché à mes favoris, afin de pouvoir y accéder rapidement depuis mon écran d’accueil.
En tant qu’utilisateur, je veux pouvoir accéder aux paramètres afin de pouvoir sélectionner les informations que je souhaite afficher sur mon écran d’accueil.
En tant qu’utilisateur, je veux que mes informations et préférences soient enregistrées afin que mes favoris soient conservés la prochaine fois que je lance l’application.

----------------------------------------------------------------------------------------------------------------------------


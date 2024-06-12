# GestionMatos

## Description

GestionMatos est une application Windows Form développée en C# et utilisant SQL Server pour la gestion de bases de données. Ce logiciel est conçu pour optimiser la gestion des interventions de maintenance d'équipements informatiques pour divers clients à travers la France. Il permet la gestion efficace du matériel, des clients, et des interventions, aussi bien préventives que curatives.

## Fonctionnalités

- **Gestion du matériel :** Ajouter, modifier, supprimer et visualiser des informations détaillées sur le matériel.
- **Gestion des clients :** Enregistrer, mettre à jour, supprimer et consulter les clients et leurs informations.
- **Planification des interventions :** Programmer et valider les interventions de maintenance, avec des options de filtrage par date, matériel, et client.
- **Rapports et statistiques :** Produire des rapports détaillés sur les interventions et des statistiques pour évaluer la performance des opérations de maintenance.

## Prérequis

- Microsoft .NET Framework 4.7.2 ou supérieur.
- SQL Server 2019 ou supérieur.

## Installation

1. **Base de données :**
   - Installez SQL Server si ce n'est pas déjà fait.
   - Exécutez le script SQL fourni dans le dossier `Database` pour créer et configurer la base de données.

2. **Application :**
   - Clonez le dépôt ou téléchargez l'archive ZIP du projet.
   - Ouvrez la solution dans Visual Studio.
   - Restaurez les packages NuGet nécessaires.
   - Compilez la solution pour générer l'exécutable.

## Configuration

- Configurez la chaîne de connexion dans le fichier `app.config` pour pointer vers votre instance de SQL Server.
- Assurez-vous que les permissions appropriées sont configurées dans SQL Server pour permettre à l'application de lire et écrire dans la base de données.

## Utilisation

- Lancez l'application en exécutant le fichier exécutable généré.
- Connectez-vous en utilisant les identifiants fournis par votre administrateur système.
- Naviguez à travers les différents menus pour accéder aux fonctionnalités de gestion des matériels, des clients et des interventions.

## Support

Pour toute assistance technique, veuillez contacter le support technique à [support@gestionmatos.com](mailto:support@gestionmatos.com). Nous offrons un support technique du lundi au vendredi, de 9h à 17h (heure locale).

## Contribuer

Les contributions à ce projet sont bienvenues. Veuillez suivre les instructions suivantes pour contribuer :
- Fork le dépôt.
- Créez votre branche de fonctionnalité (`git checkout -b feature/AmazingFeature`).
- Validez vos modifications (`git commit -m 'Add some AmazingFeature'`).
- Poussez la branche (`git push origin feature/AmazingFeature`).
- Ouvrez une Pull Request.

## Licence

Ce projet est sous licence XYZ. Veuillez consulter le fichier LICENSE pour plus d'informations.



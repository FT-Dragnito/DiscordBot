
# DiscordBot

Un modèle de bot Discord simple, modulaire et dockerisé, développé avec DSharpPlus en C#.

---

## 📋 Prérequis

Avant de commencer, assurez-vous d'avoir :

- [Docker](https://www.docker.com/) et [Docker Compose](https://docs.docker.com/compose/) installés.
- Un token Discord pour le bot ([guide ici](https://discord.com/developers/applications)).

---

## 🚀 Installation

### 1. Clonez le Projet

```bash
git clone https://github.com/FT-Dragnito/DiscordBot.git
cd DiscordBot
```

### 2. Configurez les Variables d'Environnement

Copiez le contenu du fichier `.env.template` et collez-le dans un fichier `.env` situé à la racine du projet:

```env
TOKEN=<DISCORD_TOKEN>
```

Remplacez `<DISCORD_TOKEN>` par le token de votre bot Discord.

---

## 🔧 Structure du Projet

- **`Commands/`** : Contient les modules de commandes (par exemple, `GeneralCommands.cs`).
- **`Services/`** : Contient les services utilisés par le bot (comme `DiscordService.cs` pour gérer le client Discord).
- **`Program.cs`** : Point d'entrée principal de l'application.
- **`Startup.cs`** : Configure le bot et enregistre les services nécessaires.
- **`.env`** : Contient le token Discord (ne pas le partager publiquement !).

---

## ✏️ Modification des Commandes

### Ajouter une Nouvelle Commande

1. Créez un fichier dans le dossier `Commands` (par exemple, `NewCommands.cs`).
2. Ajoutez le code suivant comme exemple :

   ```csharp
   using DSharpPlus.SlashCommands;
   using System.Threading.Tasks;

   namespace DiscordBot.Commands
   {
       public class NewCommands : ApplicationCommandModule
       {
           [SlashCommand("hello", "Says hello!")]
           public async Task HelloAsync(InteractionContext ctx)
           {
               await ctx.CreateResponseAsync("Hello, world!");
           }
       }
   }
   ```

3. Enregistrez le module dans `DiscordService.cs` :

   ```csharp
   slashCommands.RegisterCommands<Commands.NewCommands>();
   ```

4. Relancez le bot pour appliquer les changements :

   ```bash
   docker-compose restart
   ```

---

## 🐳 Déploiement avec Docker Compose

### 1. Créez le Fichier `docker-compose.yml`

Voici un exemple de fichier `docker-compose.yml` à placer à la racine du projet :

```yaml
services:
  discordbot:
    build:
      context: .
    container_name: discordbot
    restart: unless-stopped
```

### 2. Démarrez le Bot

Assurez-vous que le fichier `.env` est configuré, puis lancez :

```bash
docker-compose up --build -d
```

Le bot sera déployé dans un conteneur Docker.

### 3. Redémarrez ou Arrêtez le Bot

- Redémarrer le bot : `docker-compose restart`
- Arrêter le bot : `docker-compose down`

---

## ⚙️ Développement Local

### Exécuter le Bot en Local

Pour tester ou développer en local, assurez-vous que le token est configuré dans le fichier `.env`, puis exécutez :

```bash
dotnet run
```

Le bot se connectera à Discord et sera prêt à exécuter vos commandes.

---

## 🛠️ Conseils pour Modifier le Code

- **Ajoutez de nouvelles commandes dans `Commands/`.**
- **Regroupez les commandes similaires dans des classes séparées pour une meilleure organisation.**
- **Utilisez le service `DiscordService` pour gérer la logique liée au bot.**

---

## 🎯 Débogage

Si le bot ne répond pas ou si vous rencontrez des problèmes :

1. Vérifiez les logs du conteneur Docker :

   ```bash
   docker logs discordbot
   ```

2. Assurez-vous que le token est valide et correctement configuré dans le fichier `.env`.

3. Relancez le conteneur si nécessaire :

   ```bash
   docker-compose restart
   ```

---

## 📖 Documentation Supplémentaire

- [Guide DSharpPlus](https://dsharpplus.github.io/)
- [Guide Docker Compose](https://docs.docker.com/compose/)

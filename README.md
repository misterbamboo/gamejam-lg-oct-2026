# Peppers Please 🌶️

GameJam Lionel-Groulx (octobre 2026) — **Thème : Réflexion épicée**

Tu tiens un comptoir à chili. Des clients font la file, chacun avec sa tolérance au piquant.
Choisis tes piments, cuisine le chili, sers la bonne louche à la bonne personne… et ne fais pas faillite!

## ▶️ Jouer en ligne

**[Jouer à Peppers Please sur itch.io](https://mrbamboo.itch.io/peppers-please)**, directement dans le navigateur, sans installation.

## Équipe

- IGuessPotato aka Pierce
- wlwCheesecake aka Mélanie
- misterbamboo aka Nicolas
- TotemPear aka Charles

## Comment jouer

1. **Clique sur le panier** pour obtenir un nouveau piment (coûte **1 $**). Son drapeau indique son pays d'origine.
2. **Examine le piment** : tourne-le avec **W / A / S / D / Q / E** (garde le clic droit enfoncé pour arrêter la rotation).
   Sa couleur et son pays déterminent son piquant selon les [instructions](INSTRUCTIONS.md)… à toi de réfléchir!
3. **Glisse le piment dans la marmite**. Le chili monte, prend la couleur du mélange et affiche son **SHU** (piquant moyen) au-dessus de la marmite.
4. **Clique sur la marmite** et, sans relâcher, **glisse la louche** vers un client en haut à droite.
5. Le client goûte :
   - SHU dans sa fourchette → **« Thanks ! »** et il paie **5 $**;
   - sinon → **« Eurk ! »** et il ne paie rien.
6. Si ton argent passe **sous 0 $**, c'est la **faillite** : retour à l'écran titre.

| Touche | Action |
|---|---|
| Clic gauche (glisser) | Prendre / lancer un piment ou une louche |
| W A S D Q E | Tourner le piment |
| Échap | En jeu : retour à l'écran titre · Écran titre : quitter |
| F1 | Panneau de débogage du piment cliqué (éditeur / build de dev) |

## Fonctionnalités

### File de clients
- Un client arrive toutes les **10 s** (le premier tout de suite), **6 maximum** en même temps.
- File vide → le prochain arrive en **2 s**.
- Chaque client a un **nom québécois** (100 prénoms), une **phrase** pour annoncer ce qu'il aime et une **fourchette de piquant** de 1 à 3 de large sur l'échelle 0–10.
- Cartes avec barre de piquant (dégradé vert → jaune → orange → rouge), animations d'arrivée, de survol et de départ.

### Marmite
- Capacité de **10 portions** (1 piment = 1 portion).
- **SHU du chili** = moyenne des piments ajoutés, affichée sur une étiquette 3D.
- **Couleur du chili** = mélange des couleurs des piments, façon peinture (rouge + rouge = rouge, rouge + vert = brun 🤎).
- Chaque louche retire une portion; la relâcher sur la marmite la reverse dedans.

### Piments
- Générés aléatoirement par **lots de 10**; un nouveau lot est créé quand le précédent est vide.
- Couleur (Rouge, Vert, Jaune) + pays (9 pays avec drapeau) → piquant calculé avec la **recette** de l'équipe (`SpiceRecipe.cs`, traduite de `main.py`).
- Propriétés supplémentaires pour la formule d'appréciation (`Shared/User Enjoyment Formula.txt`) : volume, sécheresse, texture, réflexion, teinte…

### Physique
- Piments et louches tombent, roulent et entrent en collision avec la table, le sol et la marmite.
- Un mouvement rapide de la souris les lance.

### Argent
- Départ à **10 $**, −1 $ par piment, +5 $ par client satisfait.

## Architecture

Projet **Unity 6** (6000.3.23f1), URP, nouveau Input System. Code dans `src/ReflexionEpicee/Assets` :

| Dossier | Rôle |
|---|---|
| `CustomerQueueDomain/` | File de clients : entités (`Customer`, `PreferenceRange`), file (`CustomerQueue`), interface (cartes) |
| `CookingPot/` | Marmite : entité `ChiliPeppersMix` (niveau, SHU, couleur), `CookingPot`, `Ladle` |
| `Piments/` | `Piment`, `IPiment`, génération (`PimentData`, `PimentBatch`, `SpiceRecipe`), `PimentsManager` |
| `Panier/` | Le panier qui distribue les piments |
| `Shared/` | `EventBus`, événements (`GameEvents`), `Grabbable` (glisser + physique), `HeatGradient` |
| `SplashScreen/` | Écran titre, bouton Jouer, touche Échap |

Les domaines ne se connaissent pas : ils communiquent par un **EventBus** avec des événements nommés.

| Événement | Envoyé par | Écouté par |
|---|---|---|
| `PimentRequested` | Panier | PimentsManager (crée un piment) |
| `PimentDropped` | Piment relâché | Marmite (l'ajoute si elle est dessous) |
| `LadleDragged` / `LadleReleased` | Louche | File de clients (survol / service) |
| `ChiliServed` | File de clients | PimentsManager (argent), Louche, Marmite |

## Lancer le projet

1. Ouvrir `src/ReflexionEpicee` avec **Unity 6000.3.23f1** (Unity Hub).
2. Ouvrir `Assets/SplashScreen/SplashScreenScene.unity` et appuyer sur **Play**.
3. **Build** : *File → Build Profiles*, scènes `SplashScreenScene` puis `Version2`.
   Pour **Unity Play**, choisir la plateforme **Web**, faire le build, puis *Publish → Locate Build*.

> Le contrôle de version se fait avec **Git** : dans Unity, *Project Settings → Version Control* doit rester sur **Visible Meta Files** (le script `Shared/ForceVisibleMetaFiles.cs` le remet automatiquement).

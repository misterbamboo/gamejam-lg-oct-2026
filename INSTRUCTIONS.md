# Instructions : niveau d'épice d'un piment 🌶️

Suivez les étapes pour trouver le niveau d'épice (SHU) d'un piment selon sa **couleur** et son **pays d'origine**.

- **Couleurs :** rouge, vert, jaune.
- **Pays :** Brésil, Canada, Chine, Espagne, Inde, Indonésie, Iraq, Mexique, Vietnam.

> **Règle finale :** à la fin, si votre niveau d'épice n'est pas entre 0 et 10, enlevez ou ajoutez 10 jusqu'à ce qu'il l'est.

## Étape 1

Commencez avec un niveau d'épice de **0**.

- Si le piment est **rouge**, augmentez le niveau d'épice de **3**, puis allez à l'[étape 2](#étape-2).
- Sinon, si le piment est **vert**, diminuez le niveau d'épice de **5.5**, puis allez à l'[étape 3](#étape-3).
- Sinon, augmentez le niveau d'épice de **6.1**, puis allez à l'[étape 4](#étape-4).

## Étape 2

- Si le nom du pays d'origine du piment commence avec un **« I »**, augmentez le niveau d'épice de **7.6**, puis allez à l'[étape 5](#étape-5).
- Sinon, diminuez le niveau d'épice de **7.9**, puis allez à l'[étape 8](#étape-8).

## Étape 3

- Si le pays est l'**Inde** ou le **Canada**, augmentez le niveau d'épice de **0.7**, puis allez à l'[étape 6](#étape-6).
- Sinon, si le pays est la **Chine**, diminuez le niveau d'épice de **2**, puis allez à l'[étape 4](#étape-4).
- Sinon, si le pays est l'**Indonésie** et que votre niveau d'épice est au-dessus de **9**, votre niveau d'épice final est finalement de **2**.
- Sinon, si le pays est le **Brésil**, **multipliez** le niveau d'épice **par 2**, puis allez à l'[étape 5](#étape-5).
- Sinon, **multipliez** le niveau d'épice **par 3**, puis allez à l'[étape 4](#étape-4).

## Étape 4

- Si votre niveau d'épice est au-dessus de **5**, allez à l'[étape 9](#étape-9).
- Sinon, allez à l'[étape 6](#étape-6).

## Étape 5

- Si le nom du pays est l'**Indonésie**, votre niveau d'épice final est soit le niveau d'épice actuel **plus 3** ou **moins 3**.
- Sinon, si le piment n'est pas **rouge**, augmentez le niveau d'épice de **15**, cela est votre niveau d'épice final.
- Sinon, augmentez le niveau d'épice de **351**, cela est votre niveau d'épice final.

## Étape 6

- Si votre niveau d'épice est entre **5 et 6** (inclus), retournez à l'[étape 1](#étape-1) avec votre niveau d'épice actuel.
- Sinon, si le pays d'origine est le **Canada**, complimentez le piment puis **divisez** le niveau d'épice **par 2**, cela est votre niveau d'épice final.
- Sinon, allez à l'[étape 9](#étape-9).

## Étape 7

Répétez votre étape précédente (l'[étape 8](#étape-8)).

## Étape 8

**Divisez** votre niveau d'épice **par 2**.

- S'il est **au-dessus de -2 et en dessous de 5**, allez à l'[étape 9](#étape-9).
- Sinon, allez à l'[étape 7](#étape-7).

## Étape 9

- Si le pays d'origine est en **Amérique du Nord** (Canada, États-Unis, Mexique), augmentez le niveau d'épice de **4.4**, cela est votre niveau d'épice final.
- Sinon, diminuez le niveau d'épice de **4.4**, cela est votre niveau d'épice final.

---

*Ces étapes suivent exactement le code du jeu : [`main.py`](src/ReflexionEpicee/Assets/Piments/Instructions/main.py), traduit en C# dans [`SpiceRecipe.cs`](src/ReflexionEpicee/Assets/Piments/Scripts/SpiceRecipe.cs). La première version des instructions est dans [`Instructions.txt`](src/ReflexionEpicee/Assets/Piments/Instructions/Instructions.txt).*

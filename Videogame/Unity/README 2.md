# Game local installation instructions

## Download and install local game

---

1. Clone the repository

```sh
git clone git@github.com:Toootiz/TC2005B-Reto.git
```
2. Open unity and add.
   
<img title="" alt="" src="https://i.imgur.com/LoZJXwm.png">

3. Look for the repository folder, and within video games select "Dinosur_Game".

<img title="" alt="" src="https://i.imgur.com/bknIm6V.png">

4. Open the project.

## Load database

---

1. Enter the cloned folder of the repository.
<img title="" alt="" src="https://i.imgur.com/ZsqSOtm.png">
2. Enter Database.
<img title="" alt="" src="">
3. Open the database_dinocard and dinocard_Data files with MySQL.
<img title="" alt="" src="https://i.imgur.com/XwlIgI9.png">
4. Run both scripts.
<img title="" alt="" src="https://i.imgur.com/Ei9H9g8.png">
5. Add a new user in SQL, to do it in MySQL workbench, in the top menu, select server, users and privileges.
<img title="" alt="" src="https://i.imgur.com/mC0wMNj.png">
6. To do it in the workbench, in the menu above, select server, users and privileges, add.
<img title="" alt="" src="https://i.imgur.com/LU7IOSI.png">
7. Name: p1, Password: 123456.
<img title="" alt="" src="https://i.imgur.com/Wdyta1B.png">
8. Give schema privileges, just give it the image privileges..
  <img title="" alt="" src="https://i.imgur.com/0Oj2Dem.png">
9. Add to schema api_game_db.
  <img title="" alt="" src="https://i.imgur.com/Xagad20.png">
11. Apply
  <img title="" alt="" src="https://i.imgur.com/sA9GXyK.png">

## Run API

---

1. Open terminal
2. Go to API in the clone repository (the path may vary depending on where you saved it)
```sh
cd "..\TC2005B-Reto\web\api"
```
3. Install node modules
```sh
npm i
```
4. Run Api
```sh
nodemon .\app.js
```

## Play prototype 

---

1. Open Unity game
2. Go to folder "scenes"
3. Select "MenuInicial"
4. Play on Unity
5. On main menu select PLAY

## Game Mechanics

When you start the game, you will be given 5 cards on the bench, you can drag cards, as long as it is your turn and you have the necessary amount of amber, the amount of amber can be seen in the cost section on the card, once you card is on the board, you can decide whether to draw another card, attack or pass, you can attack enemy cards or the enemy base, you can only attack the enemy base if 5 turns have passed and there are no cards in the enemy play area, in Otherwise you must eliminate all the enemy cards, the maximum amount of energy you can have is 40, if you reach the maximum you will not be given more energy until you go below that amount, the game ends when one of the two bases are destroyed.

## Things that are missing

---

- Fix bugs in the main game.
- Connect API to deck scene.
- Connect API to Login.
- Save statistics.
- Make certain cards have abilities.


# Pair o'Dice - Dice Game

Pair o'Dice is a simple 3D Dice Game made in Unity/C#.

(c) 2020 - Juliano Gauciniski.

Any issues and/or sugestions, please conctacte me: juliano.gauciniski@gmail.com

## Getting Started

This project was developed in Unity 2019.2.14f1.
To get a copy of the project up and running on your local machine, you need to start from '00 Splash.unity' scene.
The scene is located under 'Assets/Scenes/' folder.

I also provided a build 'PairODiceGameBuild.apk' for testing on Android device, located in the repository root.

### Prerequisites

Unity 2019.2.14f1.

### Installing

To install the apk, just open it in any Android device.
For testing purposes, just clone the repository and run from '00 Splash.unity'.

### Game Rules

1. The player rolls 2 six-sided dice.
	a. If they roll doubles, that’s a 0.
	b. Anything else is added together to become their score.
2. Then the bot rolls their own 2 dice with the same scoring system as the player.
3. The one with the highest score wins the round.
	a. If it's a tie, then an even roll total goes to the player and an odd one to the bot.
4. There are 11 rounds.
5. The winner is the one who wins the best-of-of-11.
6. In addition, the player and bot both get 3 rerolls each.
	a. The reroll decision and reroll itself occurs after the player and bot roll but before
the game selects the round winner.
	b. If the player rolled double-odds that round, they can’t reroll.
	c. If the bot rolled double-evens that round, they can’t reroll.

*PS.: Item 3.a (a. If it's a tie, then an even roll total goes to the player and an odd one to the bot.
** Zero (0) is considered even in case of both players get a zero (0).

The game handles more than 2 dice or more than 11 rounds if it's desired.
Also the Game Stats is saved in playerprefs.

## Main Classes

Scripts located under 'Assets/Scripts/' folder.

LevelManager: Auto loads next scene from Splash scene and handles all scenes transitions.
MusicManager: Singleton used to play scenes specific music.
OptionsController: Handles the option menu.
PlayerPrefsManager: Static class that saves the data in playerprefs.
UIManager: Class that handles all  the UI and popups in game.
GameManager: That's the core of game. It leaves in the game scene, called '02 Level_01.unity'. It handles all the game rules and the debug menu.
Player: Base class for the Player (PlayerHuman class) and the Bot (PlayerBot class).
Dice: Class to handle the dice physics.
SideNumber: Class to handle the dice results and send to the GameManager.
Popup: Base class for all the UI popups.

## Debug Settings - GameManager Unity Inspector

## Next Roll Manually
For testing purposes, through the GameManager, it is possible to set the Player and/or Bot’s next roll manually.
In order to do that, it's necessary select the GameManager Gameobject in the game scene ('02 Level_01.unity'), and enable the checkbox 'Next Roll Manual'.
The 'Next Roll Manual' checkbox is located under the section 'DEBUG - Manual Roll Settings' in the inspector. Once the checkbox is enabled, it's possible to set the dice results manually.
*IMPORTANT: The checkbox needs to be enabled before roll the dice in order to set the current result (there's a popup to help with in game).

#BOT'S REROLL-DECISION-MAKING
Under the section 'DEBUG - Bot's Reroll decision settings', in the inspector, it's possible to edit the Bot’s reroll-decision-making.
The Min slider (set to 3) and the Max slider (set to 9) is how the Bot handles the reroll decision.

The Bot will only reroll when the score is less than the player.
- if its less or equal to 'Min' value, do the reroll;
- if the player gets a score greater than the 'Min' value and less or equal than 'Max' value, it's a random decision of 50% of chances for a reroll for the Bot.
The reroll decision also takes in consideration the game rules ('If the bot rolled double-evens that round, they can’t reroll').

## Thirdy-party Assets

All the assets used in this game were downloaded and free for reuse. Some of them were changed as needed.

Dice Model:
https://assetstore.unity.com/packages/templates/packs/dice-pack-light-165

Wood Texture:
https://www.wallpaperflare.com/brown-surface-wood-texture-pattern-material-timber-hardwood-wallpaper-wjxij

Tree/Island:
https://pixabay.com/pt/vectors/coqueiro-palmeira-duna-%C3%A1rvore-ilha-1892861/

Sea Background:
https://www.publicdomainpictures.net/en/view-image.php?image=76205&picture=seascape-backdrop

Porthole/Lens:
https://www.needpix.com/photo/28951/lens-camera-lens-video-lens-photography-lens-photography-camera-video-cam-lenses

Velvet Texture:
https://www.pickpik.com/texture-roughcast-plaster-wall-structure-surface-110574

UI Icons:
https://assetstore.unity.com/packages/2d/gui/icons/icons-ui-95116

Dice Logo Cartoon:
https://pixabay.com/pt/vectors/dos-dados-jogos-jogar-1294902/

Button Background:
https://pixabay.com/pt/vectors/branco-brilhante-bot%C3%A3o-retangular-37292/

FONTS:
Beachman: https://www.1001freefonts.com/beachman-script.font
Clean Sports: https://www.dafont.com/clean-sports.font?text=DICE+GAME+%2A
Cowboys: https://www.sharkshock.net/fontpage.html

SFX Button Click
https://freesound.org/people/Christopherderp/sounds/342200/

SFX Winner
https://freesound.org/people/FunWithSound/sounds/456966/

SFX Score
https://freesound.org/people/Scrampunk/sounds/345297/

SFX Dice
https://freesound.org/people/j1987/sounds/335751/

MUSIC
https://www.glitchthegame.com/downloads/


## Acknowledgments

* Thank you.



**Send It! Math (Demo):** 
**Send It! Math** is a simple math learning tool made to help struggling elementary and middle school students who are having difficulties with learning math. Send it! Math gamifies the learning process in order to help students pay attention. Since this game is a demo there is no save functionality and you will have to restart if you close the game. There are 10 scripts that make are game work which are:

**Math:**
This is the main script of the game handling the core functionality of our game. This script creates phases containing different math equations that are generated dynamically based on the phase. The script can generate additional, subtraction, multiplication, division, and parentheses.

**Player:** 
This script controls the visual actions as well as the players health. When the player gets a question right the player will attack the opposing enemy doing damage and playing an attack animation. When the player gets a question wrong or skips the question the player will take damage. If the player's health reaches zero a death animation will play and you will be sent to the main menu.

**Enemy:** 
This script is similar to the player as it handles what happens when the player gets a question wrong, right, or skips.

**Enemy Spawner:** 
This script handles the interactions between the math script and the enemy script as well as dynamically choosing an enemy to spawn after the previous one dies.

**Shop:** 
This script handles the effects that the shop items give when the player purchases an item. There are three different options in the shop: damage upgrade, heal, and give answer.

**Number Spiral:** 
This script is responsible for creating the spiral seen at the top of the game screen. This spiral has all correct answers the player has done. There is no limit to the amount of numbers the spiral can hold.

**UiHandler:** 
This script handles the button functionality of the main menu allowing for the player to start the game or quit the application.

**MenuSpiral:** 
This script, similar to the number spiral, generates a number spiral in the middle of the screen. However this version dynamically chooses numbers to place in the spiral along with having the spiral increase and decrease in size slightly giving a breathing effect.

**Demo:** 
This script handles the breathing effect on the demo text at the bottom right corner of the title on the start screen.

Credits
Background:
Raou

Fonts:
Yuki

Characters/Enemies:
Luiz Melo

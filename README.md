# DJ Rush

A 2D arcade game, product of a group project, in which Engineers, Designers and Artist come together to develop a game in 2 weeks.
The game supports control input handling from a custom-made arcade controller.

<p align="center">
  <img src="Media/demo.gif"><br/>
  *<i>Low frame rate caused by gif limitations</i>*
</p>

## Overview

In the heart of a mystical cave, an elemental spirit goes on a quest to reunite with the mother spirit, Gaia, to help him restore his wings.
The spirit navigates through a labyrinthine world filled with obstacles and floating rocks.
Possessing the remarkable ability to change between the elemental states of fire and water, it ascends the towering walls, each step a delicate balance between gaining power and losing it.

Developed by utilizing the GxP 2D Game Engine, provided by the institution.

## Features

### Gameplay (Engineers)

- **Player Movement:** Launching itself from place to place
- **Player Element Swtich:** Switching between Ice and Fire element
- **Walls:** Growing or shrinking in size depending on whether the element of the wall and that of the player match or not
- **Obstacles:**
  1. Player element and obstacle element match:
     - If player mass is lower than the obstacle's, the player sticks and grows until it reaches the given mass, then it starts sliding
     - If not, it starts sliding immediately
  2. Player element and obstacle element do not match:
     - If player mass is lower than the obstacle's, the player dies
     - If not, the obstacle gets destroyed and the player's mass gets decreased by the obstacle's

### UI/UX, SFX, VFX (Designers and Artists)

- **Start and end screen**
- **HUD:**
  1. Size indicator
  2. Score
- **Sounds**
  1. Background sounds
  2. Sound effects
- **Particle:**
  1. Smoke particle when the obstacle gets destroyed
 
### Level editing
Done by using Tiled level editor

## Controls

- **Player movement:**
  1. Press and hold the left mouse button and by dragging the mouse, modify the launch direction and power
  2. When done, release the left mouse button to launch yourself
- **Player element switch:** Press the right mouse button to switch elements

## Level Design
Done by: 

## Art
Done by: 

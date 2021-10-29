# IK-Character

<img src="https://thumbs.gfycat.com/DiscreteMeatyHectorsdolphin-size_restricted.gif" width="400"  />

## Additions

* **Dummy** folder: Contains Model .fbx, Animations and Controllers. 
* **Prefabs** folder: Contains two prefabs for the Dummy model: One with walk-only animations and other with a wider spectrum of movement.
* *MotionCharacer.cs*: Contains the motion script for the character. Since it is using a Character Controller, all external forces have been programmed from scratch, such as gravity. In addition, the Input Control and IK features are contained here.
* **Cinemachine**: Camera System for third-person movement. Settings are already configured.
* Albedo and Normal maps used for Terrain Material for better visibility and to look nicer.

## Modifications:

Now, two cameras are in the scene: 

* **MainCamera** (*MainCamera* tag): Third-person camera to follow the player.
* **BirdCamera** (*SecondCamera* tag): Bird'eye view for terrain manipulation.

During play, both cameras can be switched by changing the Display number in the Game window, in order to control the character and modify the terrain without stopping the game.
*CustomTerrain.cs* has been modified, in order to be able to use both, character control and terrain manipulation. Now the pointer uses the *SecondCamera* tag to modify the terrain (commented in code).

Note: The addition has been created with Unity 2020.1.7f1

Enjoy!
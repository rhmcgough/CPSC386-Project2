# Project 2

* Ryan McGough

## Links: 
* WebGL Game: https://play.unity.com/api/v1/games/game/201c8957-478f-4d12-bcc4-5cb705b42242/build/latest/frame
* Github: https://github.com/rhmcgough/CPSC386-Project2

## Scripts:

* PlayerController.cs: Reads move and jump from the input actions asset and moves the Rigidbody2D, with an overlapcircle ground check gating the jump (as created in class). Additionally sets animator parameters, will play the collect particle burst, and respawns the player after falling or being hit. 
* Coin0.cs: When a player collects a coin, it adds to the score, plays a sound, fires the particle burst, and destroys itself.
* EnemyStats.cs: A scriptableobject holding an enemy's name, speed, and color. Each enemy reads its values from an asset instance.
* Enemy.cs: Patrols left and right using the speed and color from its EnemyStats asset. It returns the player to spawn on contact. 
* Switch.cs: When the player enters its trigger, it turns green, plays a beep, and reports its ID to the Door.
* Door.cs: Tracks which switches are activated and slides open when all are on.
* GameSession.cs: A static class holding the current score and the high score stored in PlayerPrefs.
* HudController.cs: Updates the score and best-score labels and handles the pause panel with Escape and Time.timeScale.
* SceneLoader.cs: Loads a scene from a button or when the player touches the goal, optionally saving the high score first. When the player touches the goal, it also plays a clip through AudioManager.PlaySfx . 
* CameraFollow.cs: Moves the Main Camera to the player's position.
* PlatformGenerator.cs: Builds a platform layout from a seeded random number generator, with the seed and ranges exposed in the Inspector. The ranges are set below the player's jump limits so every seed is still beatable.
* AudioManager.cs: A persistent single audiomanager (DontDestroyOnLoad with a duplicate guard) that holds the music and SFX AudioSources and plays sound effects. It restores the saved music volume from PlayerPrefs at launch and saves it when changed with the + and - keys.
* FallSfx.cs: A StateMachineBehaviour that plays a sound through AudioManager when the Animator exits a state after at least 0.4 seconds. Note that in the animator, the default value is not used, rather 1 second is used to activate the fall sfx. 
## Scenes:

* MainMenu
	* Description: A main menu with a start button. 
	* GameObjects: 
		* Main Camera: GameObject that allows the player to view the game. 
		* Canvas: Stores button and menu elements
		* BestText: Displays the highest score achieved in the game
		* PlayButton: Opens the Game scene
		* MenuManager: Stores functions that populate the main menu
		* AudioManager (shared): Plays music and sfx, all audio is routed through it. Starts the music and persists into Game.
* Game
	* Description: A basic platformer level where a player has to climb a wall and dodge enemies around the playable area. Player is returned to the spawn on impact with an enemy or falling into a pit. Activate all switches to open the goal. Player can also collect coins to show off. 
	* GameObjects:
		* AudioManager (shared): Plays music and sfx, all audio is routed through it.
			* FallSfx.cs plays fall impact noise via animator behaviours, fulfilling requirement 6 for animation events. 
			* All audio is routed through the persistent Audiomanager (including FallSfx), fulfilling requirement 3.  
		* Playable level for the player to traverse and activate all switches.
		* Main Camera: GameObject that allows the player to view the game. Has been modified from standard Unity GameObject with CameraFollow.cs so the camera follows the player. 
		* Grid with Ground Tilemap: Solid ground for the playable area.
		* Coin(s): Collectable item that plays a sparkburst and increases a value in the game. 
		* Switches: Must be activated in order to complete the level. Player must find and activate switch, activation will play a beep. 
		* Door: Prevents the player from reaching the goal. Opens after player activates all switches. 
		* Goal: Returns the player to the main menu. Plays the windows 3.1 startup sound effect as a victory sound. 
		* PlatformGenerator: spawns the platforms at start, platforms are generated based on a seeded value. 
		* Canvas: Activates pause menu
		* GameManager (HudController): drives the score text and pause panel

## Requirements

| Category                                                | Requirement                                                                                                                                                                                                   | How Requirement is Met                                                                                                                                                                                            |
| ------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Requirement 1: Platformer movement                      | Input Actions asset drives movement and jumping with Rigidbody2D, and a ground check gates the jump.                                                                                                          | PlayerControls (Input Actions) sends messages to PlayerController through PlayerInput, which sets Rigidbody2D velocity. Jumping requires Physics2D.OverlapCircle to find the Ground layer at the GroundCheck.     |
| Requirement 2: Puzzle state tracking                    | A controller tracks completed pieces through triggers or events, and a clear win condition fires when all are complete.                                                                                       | Each Switch reports its ID from OnTriggerEnter2D, and the Door stores the IDs in a HashSet. When all are in, the Door will open.                                                                                  |
| Requirement 3: Audio and persistence                    | Music and two or more sound effects play through an AudioMixer with Music and SFX groups, a persistent audio object survives a scene change without duplicates, and a saved volume setting survives relaunch. | AudioManager persists with DontDestroyOnLoad and a duplicate guard, plays music and sound effects (sfx_beep, coin, music, slime_impact, win31 ) from separate AudioSources, and saves MusicVolume in PlayerPrefs. |
| Requirement 4: ScriptableObject and pure C#             | A ScriptableObject type with two or more asset instances is used by the game, and a pure C# class holds real game logic.                                                                                      | EnemyStats is a ScriptableObject asset used by three enemy instances.  GameSession is a pure C# class that holds the switch logic and is used by Door.                                                            |
| Requirement 5: Animator and blend tree                  | An Animator Controller with three or more states and a blend tree, driven by script parameters.                                                                                                               | Animator has three states: Locomotion, Player_Jump and Player_Fall. Locomotion is a blend tree with Player_Idle at 0 and Player_Run at 1. PlayerController sets Speed, isGrounded, and yVelocity each frame.      |
| Requirement 6: Animation event or StateMachineBehaviour | An Animation Event or StateMachineBehaviour visibly drives a gameplay moment.                                                                                                                                 | FallSfx is a StateMachineBehaviour on the Animator that plays slime_impact when the player leaves the fall state after a fall of at least 1 second.                                                               |
| Requirement 7: Particle effect from code                | A particle effect fires at a gameplay moment through a script call.                                                                                                                                           | Coin0.cs calls PlayCollectEffect on PlayerController, which calls Play() on the SparkBurst particle system.                                                                                                       |
| Requirement 8: Procedural generation                    | Part of the game is generated from noise or seeded randomness, with an exposed Inspector parameter, and the level stays beatable.                                                                             | Builds a platform layout from Perlin noise ( Mathf.PerlinNoise ), with seed, scale and heightMultiplier exposed in the Inspector.                                                                                 |

## Third-Party Assets

* All code is created using resources covered in class or through Unity user documentation unless otherwise stated. New code is very minimal and created without researching outside sources.
* Camerafollow.cs: Sourced from https://gamedevbeginner.com/how-to-follow-the-player-with-a-camera-in-unity-with-or-without-cinemachine/
* Audio: 
	* Music: Looped sample from Destrokk by MGMT
	* SFX Fall Impact Sound: Sampled from minecraft. 
	* SFX Goal Sound: Sampled from Windows 3.1 by Microsoft. 

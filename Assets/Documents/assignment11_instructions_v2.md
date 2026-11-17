Assignment 11: Profiling, Debugging, and Game AI

Due: Tuesday, November 17, 2026, at 11:59 PM PST.

What to submit: Upload one ZIP containing the following:

1. BugMuseum.cs (the fixed version from Exercise 2).
2. Wanderer.cs (the optimized version from Exercise 4) and Wanderer_before.txt (the slow version, saved as a text file).
3. GuardStateMachine.cs (the version with Flee from Exercise 6).
4. ArcherAI.cs (Exercise 7).
5. BTNode.cs, Coin.cs, and CollectorAI.cs (Exercise 8).
6. A plain text file named a11_notes.txt, with sections labeled Exercise 1, Exercise 2, Exercise 3, Exercise 4, Exercise 5, Exercise 7, Exercise 9, and Exercise 10.
7. Four clearly named screenshots: a11-ex03-profiler.png, a11-ex05-breakpoint.png, a11-ex06-guard-flee.png, and a11-ex08-collector.png.

Exercises 1 to 5 are about debugging and profiling. Exercises 6 to 9 are about game AI: state machines, decision trees, and behavior trees. Exercise 10 pulls both halves together.

Exercise 1: Build the Bug Museum and Read Three Errors

If you built this on Tuesday 11/3, open that project and skip any step you already did.

1. In Unity Hub click New project, choose the Universal 2D template, name it Week11Lab, and click Create project. Choose File > Save As and name the scene BugMuseum. Create a folder Scripts under Assets.
2. Select Main Camera. Set Position to 0, 0, -10, confirm Projection is Orthographic, set Size to 6.5, and set Background Type to Solid Color with Hex 1E2A44.
3. Add a Square (2D Object > Sprites > Square), rename it Museum, and set its Sprite Renderer Color to Hex FF7900. Do not add a Rigidbody 2D.
4. Unzip week11-tues-scripts.zip (the file posted with the Week 11 Tuesday module). Copy BugMuseum.cs from the stage01_console_bugs folder into your Scripts folder (drag it into the Project panel).
5. Save, return to Unity, and confirm no red errors. Drag BugMuseum onto Museum. Open Window > General > Console.
6. For each of the three bugs, set Bug To Run on Museum, press Play, click the red line in the Console, read the stack trace, double click the first line that names BugMuseum.cs, and stop Play. Do this for Null Reference, Off By One, and Modified Collection.
7. In a11_notes.txt, under a line that reads Exercise 1, write one line per bug: the exception name, the line number the Console pointed at, and one sentence on why it happened.

Exercise 2: Fix the Three Bugs

1. In BugMuseum.cs make three changes yourself, without copying a finished file. First, add an Awake() method just above Start() that stores GetComponent<Rigidbody2D>() in the existing body field.
2. In OffByOneBug, change i <= coinValues.Length to i < coinValues.Length.
3. In ModifiedCollectionBug, replace the foreach loop with a for loop that starts at enemies.Count - 1, counts down to 0, and calls RemoveAt(i) when enemies[i] is "Bat".
4. Save and return to Unity. Set Bug To Run to Null Reference and press Play. You now get a different red message, a MissingComponentException saying there is no Rigidbody2D on Museum. Stop Play. Select Museum, click Add Component, add Rigidbody 2D, and set Gravity Scale to 0.
5. Press Play for each bug again. Null Reference prints Gravity turned off, Off By One prints Total coin value: 16, and Modified Collection prints Enemies left: 2. Choose File > Save.
6. In a11_notes.txt, under a line that reads Exercise 2, write two or three sentences. Say what the fix was for each of the three bugs, and explain how the message in step 4 differs from the one in Exercise 1.

Exercise 3: Profile the Slow Crowd

1. Choose File > Save As and name the scene Crowd. Delete Museum. Create a folder Prefabs under Assets. Confirm the camera is the same as in Exercise 1.
2. Unzip week11-tues-scripts.zip (the file posted with the Week 11 Tuesday module) if you have not. Copy Wanderer.cs and CrowdSpawner.cs from the stage03_crowd_slow folder into your Scripts folder. Wanderer has a profiler marker named Wanderer.FindNearest and deliberately searches the whole scene every frame. You do not need FrameTimeLogger.cs here.
3. Save, return to Unity, and confirm no red errors. Add a Square, rename it Dot, set Scale to 0.3, 0.3, 1, and drag Wanderer onto it. Drag Dot into the Prefabs folder, then delete it from the Hierarchy.
4. Create an empty GameObject named Spawner, attach CrowdSpawner, drag the Dot prefab into Dot Prefab, and confirm Count is 300. Choose File > Save. Press Play: three hundred colored squares drift around. Stop Play.
5. Choose Window > Analysis > Profiler. Select CPU Usage and turn Record on. Press Play, wait about five seconds, and press Play again to stop. Click a frame in the middle of the chart, switch the bottom half from Timeline to Hierarchy, and click the Time ms column header. Hold Alt (Option on Mac) and click the arrow beside PlayerLoop to expand it. Find the row named Wanderer.FindNearest.
6. Take a screenshot showing the Profiler with a frame selected, the Hierarchy table, and the Wanderer.FindNearest row with its Calls, GC Alloc, and Time ms visible. Save it as a11-ex03-profiler.png.
7. In a11_notes.txt, under a line that reads Exercise 3, write the Time ms, GC Alloc, and Calls for Wanderer.FindNearest, and one sentence on why 300 dots makes the number so large.

Exercise 4: Optimize the Crowd

1. Open Wanderer.cs, select all the text, copy it, and paste it into a new plain text file named Wanderer_before.txt. Save that file.
2. In Wanderer.cs select all the text and delete it. Open Wanderer.cs from the stage04_crowd_optimized folder of the same zip, copy all of it, and paste it in. This version registers every Wanderer in a static list, caches the transform, and only searches every tenth frame.
3. Save, return to Unity, and confirm no red errors. Repeat Exercise 3 step 5 with the new script and read the Time ms, GC Alloc, and Calls of Wanderer.FindNearest again.
4. In a11_notes.txt, under a line that reads Exercise 4, write a before and after pair for Time ms, GC Alloc, and Calls. Then write two or three sentences naming the three changes in the new script (registering in a list instead of searching the scene, caching the transform, and only searching every tenth frame) and which one you think helped most.

Exercise 5: Break on a Line

1. Choose Unity > Settings > External Tools on Mac (Edit > Preferences > External Tools on Windows). Set External Script Editor to Visual Studio Code, and make sure the Microsoft Unity extension is installed in VS Code.
2. Open the BugMuseum scene, select Museum, set Bug To Run to Off By One, and choose File > Save. Open BugMuseum.cs in VS Code by double clicking it. In the bottom right corner of the Unity window, click the small bug icon and make sure it reads Debug Mode.
3. In VS Code open Run and Debug (Cmd+Shift+D), choose Attach to Unity, and click the green play button. Click in the gutter beside the line total += coinValues[i]; so a red dot appears.
4. Switch to Unity and press Play. Unity freezes and VS Code shows that line highlighted in yellow. In the Variables panel open Locals and read i and total. Press Continue (F5) twice and read them again on the third stop.
5. Take a screenshot of VS Code paused at the breakpoint with the Variables panel and the yellow line visible. Save it as a11-ex05-breakpoint.png. Press Step Over (F10) once, press the red Stop square to disconnect, and stop Play in Unity.
6. In a11_notes.txt, under a line that reads Exercise 5, write the values of i and total on the third stop, and one sentence on what a breakpoint shows you that a Debug.Log line does not.

Exercise 6: Build the Guard State Machine

1. Choose File > Save As and name a new scene AIArena. Choose Edit > Project Settings > Player, open Other Settings, and make sure Active Input Handling reads Input System Package (New) or Both. Choose Window > TextMeshPro > Import TMP Essential Resources and click Import. Same camera as before.
2. Add a Square named Arena with Scale 21, 11.5, 1, Color Hex 2A3C63, and Order in Layer -10. Add a Square named Player at Position 0, -4.5, 0 with Scale 0.8, 0.8, 1, Color Hex 3DDC84, and Order in Layer 1.
3. Unzip week12-tues-scripts.zip (the file posted with the Week 12 Tuesday module). Copy AILabel.cs, Health.cs, and PlayerMover.cs from the stage01_arena folder into your Scripts folder. AILabel draws a text label above each enemy, Health holds health that regenerates 5 per second, and PlayerMover moves with W, A, S, D or the arrows and hits everything with Health within 6 units for 25 damage when you press H.
4. Attach PlayerMover to Player.
5. Create an empty GameObject named Waypoints, and add three child Squares named WP1, WP2, and WP3 with Scale 0.3, 0.3, 1, Color Hex 9AA5B1, and Positions -8, 4, 0, then -4, 4, 0, then -4, 1, 0.
6. Add a Square named Guard at Position -8, 4, 0 with Scale 0.9, 0.9, 1, Color Hex 4DA3FF, and Order in Layer 1. Add the Health component to it.
7. Copy GuardStateMachine.cs from the stage03_guard_flee folder of the same zip into your Scripts folder. It has the states Patrol, Chase, Attack, and Flee, with Flee Health 30 and Recover Health 60.
8. Attach it to Guard, drag Player into the Player field, and set Waypoints Size to 3 with WP1, WP2, and WP3 in it. Choose File > Save.
9. Clear the Console and press Play. Click the Game view. The Guard is blue and patrols. Walk within 6 units: it turns orange (Chase). Let it reach you: it turns red (Attack) and logs Guard swings at the player. Walk away: it goes back to blue. Then stand within 6 units and press H three times: it turns purple (Flee), runs away, and returns to Patrol after about 7 seconds as its health regenerates to 60.
10. Take a screenshot showing the Game view with the Guard labeled Guard Flee and the Console showing the Chase to Flee line. Save it as a11-ex06-guard-flee.png. Stop Play and choose File > Save.

Exercise 7: Build the Archer Decision Tree

1. Add a Square named Archer at Position 9, 4, 0 with Scale 0.9, 0.9, 1, Color Hex 9AA5B1, and Order in Layer 1. Add the Health component to it.
2. Copy ArcherAI.cs from the stage04_archer_decision_tree folder of the same zip into your Scripts folder. Its Decide method is the decision tree.
3. Attach it to Archer and drag Player into the Player field. Choose File > Save. Press Play and test: far away it is gray (Idle), within 12 units orange (Approach), within 8 units red (Shoot) with the Console counting arrows down, at 0 arrows yellow (Reload) for 1.5 seconds. Stand about 5.5 units away and press H three times to see Hide, then closer than 5 units and press H three times to see Flee. Stop Play.
4. In a11_notes.txt, under a line that reads Exercise 7, write the action Decide returns for each case, working from the code first: (a) health 100, distance 15, arrows 5. (b) health 100, distance 6, arrows 0. (c) health 100, distance 3, arrows 0. (d) health 20, distance 7, arrows 5. (e) health 20, distance 3, arrows 5. Then write two sentences on why the reload does not flicker between Reload and Shoot, using the words memory and timer.

Exercise 8: Build the Collector Behavior Tree

1. Copy BTNode.cs, Coin.cs, and CollectorAI.cs from the stage05_collector_behavior_tree folder of the same zip into your Scripts folder. BTNode.cs holds the Selector, Sequence, Condition, and Action nodes, and CollectorAI.cs builds the tree in Start.
2. Add a Square named Base at Position -8.5, -4.5, 0 with Scale 1.2, 1.2, 1, Color Hex 3B5B8C, and Order in Layer -5. Add a Square named Coin1 at Position -3, -1, 0 with Scale 0.35, 0.35, 1 and Color Hex FFD400, add the Coin component, and duplicate it with Cmd+D three times as Coin2 at 4, -3, 0, Coin3 at 6, 0.5, 0, and Coin4 at -6, -3.5, 0.
3. Add a Square named Collector at Position -5, -2, 0 with Scale 0.9, 0.9, 1, Color Hex 9AA5B1, and Order in Layer 1. Attach CollectorAI, drag Player into Player, and drag Base into Base Spot. Choose File > Save.
4. Clear the Console and press Play. Keep the player in the top right corner. The Collector turns orange (Fetch), picks up a coin, turns yellow (Deliver), reaches the base, and the Console prints Collector delivered a coin (total 1). Then walk within 2.5 units of it: it turns purple (Evade), even while carrying a coin.
5. Take a screenshot showing the Game view with the Collector labeled Collector Evade or Collector Deliver and the Console showing at least three Collector lines. Save it as a11-ex08-collector.png. Stop Play and choose File > Save.

Exercise 9: Reorder the Tree and Map the Guard

1. In CollectorAI.cs, cut the Evade Sequence (the one with PlayerTooClose and RunAway) from the top of the Selector and paste it just above new ActionNode(Wander). Mind the commas. Save and Play, and move the player next to the Collector while it carries a coin, and while it does not. Then undo the change.
2. In a11_notes.txt, under a line that reads Exercise 9, write three or four sentences on what changed when Evade moved down, which branch won when the player was close, and what this shows about the order of a Selector's children.
3. Also under Exercise 9, list every transition in the Guard, one per line, as From state, To state, condition, counting Flee as reachable from each of the other three states (there are eight). Then write two sentences on why fleeHealth is 30 but recoverHealth is 60.

Exercise 10: Choose the Tool

In a11_notes.txt, under a line that reads Exercise 10, write five short answers. For the first two, name the first tool you would reach for (Console, a breakpoint, the Profiler, or the Frame Debugger) and give one sentence of why. For the last three, name the best fit (state machine, decision tree, or behavior tree) and give one sentence of why.

1. A boss fight drops to 20 frames per second only when 40 enemies are on screen.
2. A shop menu adds 2 coins when you buy a 1 coin item, and you cannot tell which line is adding twice.
3. A traffic light that stays red for 8 seconds, then green for 6, then yellow for 2, then repeats.
4. A shopkeeper who chooses one greeting from the player's level, reputation, and whether the player owes money, every time the player walks up.
5. A rescue drone that flees danger first, otherwise delivers a rescued survivor to base, otherwise flies to the nearest survivor, otherwise searches the map.

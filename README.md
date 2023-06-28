# race_OS
## Functionalities
As simple racing game in parts of Osnabrück.

A start menu offers the possibility to start the game by clicking a button or to quit the game with another button. Therefore, one has to start from the scene 'Menu'.
After a countdown with matching sound counts down to zero, the car can be moved. You can move left, right, forwards ...
Background music is played during the race. Engine sounds are also part of the game. One sound represents the acceleration of the engine, it is played while moving forward and when the sound is not already playing. It stops when the car is not moving forward. A second sound represents the general engine sound and is always played when the car is moving.
Both engine sounds stop when the car crosses the finish line or when the car stops moving.
Pressing 'p' brings up the 'Pause' menu. Here the player can choose between 'Play', 'Quit' and 'Restart'. 'Play' continues the game, 'Quit' quitts the game and 'Restart' restarts the game at the countdown.
There are several items scattered along the way.
One of them is a clock. A collision with it reduces the time to five seconds. It makes a bing sound and the clock disappears. To inform the player about the function of the clock, a text will be displayed saying '-5 sec'.
A second item is...
Figures representing us (Melih, Johanna, Mohammad) are placed along the road. When the car runs over them, each character shouts in our voices and the car slows down.
Finally, the driver reaches the finish line. Depending on the time, a certain sound and an appropriate image are displayed. Images and sounds are classified as good, naja (neutral) and bad. After that a 'Game Over' menu is displayed. It offers the option to restart or quit the game.


## Todos

### Unity

-   [ ] Make the car freeze while Timer is running
-   [ ] Create a start and finish line
    -   [ ] Track how far the car has traveled  
             (prevent reaching the goal by driving backwards)
    -   [ ] Create a time tracker to display the needed time
-   [ ] Create a leaderboard
    -   [ ] Store the times of each run
    -   [ ] Load the values from storage
    -   [ ] Create interface for name
-   [ ] Create Nitro Item
-   [ ] Create other Item
-   [ ] Implement a better road
-   [ ] Create a car selection menu (dynamically spawn each car)
-   [ ] Add computer cars (following a line trough the map)
-   [ ] Add Component that tracks the car normal vector and flips the car when upside down

### Blender

-   [ ] create customized cars
-   [ ] create customized env obj

-   Heger Tor
-   Include the parks
-   Create building textures

## Requirements

-   Unity 2021.3.22f

## Source

-   Crating the road system: [Tutorial](https://www.youtube.com/watch?v=vUNfK4Nl_ec)
-   Improve Car stability : [Tutorial - Pablos Lab](https://www.youtube.com/watch?v=BwL3Dm8GJtQ)

## Problems

-   Creating a useful raod system
-   Functioning Car Physics
    -   14.06
        -   Car is not steering right. It pushesh the car into a direction with a too large force
        -   Car is just drifting right and left. going straight seems no easy task
        -   When driving backwards the car takes ages to go back straight again
        -   Debugging of the backwards driving is also a nightmare because the camera is not tracking the car correctly. It takes to long to move behind the car and thus makes it unpossible to test the driving. But also increasing the speed of the camera causes the view to "wobble"
- At high speed the items/obstacles did not make any sound -> Increasing the Min. Distance in the Audio Source setting caused the sound attenuation  to start later
- When car was set as trigger in collider component, it started to move continuously and uncontrollable -> instead we used event system, which allowed to set only collision object as trigger
- Integration of time delays -> StartCoroutine > IEnumerator
- Lighting problem when integrating the menu: using the start menu to switch to the game scene results in a game scene without light -> Unity automatically adjusts the menu scene lighting settings (no light) to the game scene. We had to adjust these settings individually to avoid this problem.
- Keeping track of all details (is it tagged, is the trigger set in the collision component, are all scripts attached, are all relevant scripts referenced)


## Grading Scheme

**Project Documentation 10P**

-   [ ] useful commit messages
-   [ ] explanations what your code should do
-   [ ] stating issues you've coped with
-   [ ] undestand the project troughout the documentation

**Code quality 15P**

-   [ ] effective coding
-   [ ] functions where useful
-   [ ] no redunndant code
-   [ ] useful data types
-   [ ] demonstrating knowledge (code and data structure)

**C# useful coding 25P**

-   [ ] naming conventions
-   [ ] comments
-   [ ] structure
-   [ ] explanations
-   [ ] no redundant or meaningful comments

**Unity structure 5P**

-   [ ] hierarchy
-   [ ] sensible folder structure

**Bug fixing 10P**

-   [ ] no major bugs impacting the gameplay

**Functionalities 40P**
how complex are the features. Document in the readme the implemented functionalities

**Bonus (max 15. P)**

-   [ ] original ideas
-   [ ] well chosen assets
-   [ ] self created assets

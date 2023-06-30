# race_OS

## Functionalities

A simple racing game in parts of Osnabrück.

A start menu offers the possibility to start the game by clicking a button or to quit the game with another button. Therefore, one has to start from the scene 'Menu'.
After a countdown with matching sound counts down to zero, the car can be moved. You can move left, right, forwards ...
Background music is played during the race. Engine sounds are also part of the game. One sound represents the acceleration of the engine, it is played while moving forward and when the sound is not already playing. It stops when the car is not moving forward. A second sound represents the general engine sound and is always played when the car is moving.
Both engine sounds stop when the car crosses the finish line or when the car stops moving.
Pressing 'p' brings up the 'Pause' menu. Here the player can choose between 'Play', 'Quit' and 'Restart'. 'Play' continues the game, 'Quit' quitts the game and 'Restart' restarts the game at the countdown.
The car has also a turbo button "Q", that provides a nitrous shot to the car with the special effect that is displayed behind it.
There are several items scattered along the way.
One of them is a clock. A collision with it reduces the time to five seconds. It makes a bing sound and the clock disappears. To inform the player about the function of the clock, a text will be displayed saying '-5 sec'.
A second item is...
Figures representing us (Melih, Johanna, Mohammad) are placed along the road. When the car runs over them, each character shouts in our voices and the car slows down.
A third item is the portal, that would transfer the car from point A to point B forward on the road.
Finally, the driver reaches the finish line. Depending on the time, a certain sound and an appropriate image are displayed. Images and sounds are classified as good, naja (neutral) and bad. After that a 'Game Over' menu is displayed. It offers the option to restart or quit the game.
The car mechanics can be divided into four parts.

-   acceleration
    This function updates the acceleration of the car based on the input and current speed. It calculates the car's speed, normalizes it, and determines the available torque based on the acceleration input and speed multiplier. The function then applies the acceleration force in the forward direction and the steering force in the right direction to the car's Rigidbody. The acceleration force is drawn as a white line for debugging purposes.
-   drag
    This function applies drag to the car's tires. It calculates the tire's velocity and applies a drag force proportional to the tire's velocity and the drag factor. The force is applied at the tire's position using the car's Rigidbody.
-   steering
    This function updates the steering of the car. It calculates the tire's velocity in the steering direction and determines the grip factor using an animation curve. The desired change in velocity is calculated based on the current steering velocity and the grip factor. The desired resiting force is then applied into the opposite direction of the steering force
-   suspension
    This function updates the suspension of the car. It performs a raycast from the tire position in the opposite direction of the car's up vector to check if the tire is touching the ground. If the raycast hits the ground, it calculates the spring force and applies it to the car's Rigidbody at the tire's position. The force is calculated based on the offset between the rest suspension length and the distance from the tire to the ground, as well as the velocity of the tire in the spring direction. The suspension force is drawn as a green line for debugging purposes.

## Requirements

-   Unity 2021.3.22f

## Source

-   Crating the road system: [Tutorial](https://www.youtube.com/watch?v=vUNfK4Nl_ec)
-   Improve Car stability : [Tutorial - Pablos Lab](https://www.youtube.com/watch?v=BwL3Dm8GJtQ)
-   Physics based driving [Link](https://www.youtube.com/watch?v=CdPYlj5uZeI&pp=ygUUdW5pdHkgY2FyIGNvbnRyb2xsZXI%3D)

## Problems

-   Creating a useful raod system
    -   previously we had a terrain that was realistic and thus was angled a lot
    -   the angled surface thus made it annoying to create a usefull road
        -   parts of the ground sticked out
        -   we tried different assets but neither of them worked for us
    -   as a solution we started to creata path create with which we wanted to create the road
        -   creating the editor turned out to be a little to compex
        -   we decided to simplify the ground to a simple flat surface
-   Useful driving system

    -   Wheel collider based driving
        -   Car is not steering right. It pushesh the car into a direction with a too large force
        -   Car is just drifting right and left. going straight seems no easy task
        -   When driving backwards the car takes ages to go back straight again
        -   Debugging of the backwards driving is also a nightmare because the camera is not tracking the car correctly. It takes to long to move behind the car and thus makes it unpossible to test the driving. But also increasing the speed of the camera causes the view to "wobble"
        -   after fixing some previous issues the car misaligned its velocity after driving backwards making it unpossible to drive reasonably

-   At high speed the items/obstacles did not make any sound
    -   Increasing the Min. Distance in the Audio Source setting caused the sound attenuation to start later
-   When car was set as trigger in collider component, it started to move continuously and uncontrollable -> instead we used event system, which allowed to set only collision object as trigger
-   Integration of time delays -> StartCoroutine > IEnumerator
-   Lighting problem when integrating the menu: using the start menu to switch to the game scene results in a game scene without light -> Unity automatically adjusts the menu scene lighting settings (no light) to the game scene. We had to adjust these settings individually to avoid this problem.
-   Keeping track of all details (is it tagged, is the trigger set in the collision component, are all scripts attached, are all relevant scripts referenced)

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

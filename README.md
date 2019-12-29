# Indy 500

[![Indy 500 Demo Video](https://img.youtube.com/vi/lZTEWK2c5Dc/0.jpg)](https://www.youtube.com/watch?v=lZTEWK2c5Dc)

On 28 December 2019, a group of four people made a clone of the classic Atari game _Indy 500_ in an 8-hour game development challenge.

We were able to get a lot of the main features of the game implemented in this short time frame:

* You can drive your car with the keyboard (left and right arrow keys to steer, space key to accelerate).
* The game detects when you've driven off into the dirt and slows you down.
* The game keeps track of how many laps each player has gone.
* The game can tell when a player has completed enough laps to win, and ends the game.
* Particle effects when you leave the track and drive on the dirt.
* AI players that are good, but intentionally variable in skill level so they don't all bunch up.
* Basic rendering for the game.
* A simple menu system that lets you see the main menu, the game in progress, and the credits.

We also had background music working at one point, but then a merge conflict borked it.

## Don't Judge the Code!

While the code is here, in the open, free for people to see what we did, don't judge the quality of the code too harshly:

* It was done by many people, without preemptively deciding on a coding style.
* We all knew it was throwaway code, not something we were going to maintain for the long haul. 8 hours really isn't a lot of time.
* The priority was on getting features working, not on maintainable clean code.

We all believe strongly in the value of clean code, and to a large extent, we stuck to good coding and design practices when making this game.
But this is probably not the best example of what code _ought_ to look like, if you want to build on it for days, weeks, or years.

Boulderados/Gibrock
============

Note: As of April 2015, @sasoh is suspening his involvement to continue working on [a new project for the new month](https://github.com/sasoh/Lander). [Windows (x86) build here](http://sourceforge.net/projects/gibrock/files/builds/024_LevelSelectionScores.rar/download). To be continued.

![](http://i.imgur.com/5wYdJOI.jpg)

Two-player versus game for PC. Core gameplay: one boulder is available, which takes 0.7 seconds to pick up. After it is picked up, the player can throw it, killing the opposing player upon impact.
Once the boulder has been picked up, the player not wielding the boulder  can pick up a pebble. It is is thrown at the player wielding the boulder fast, interrupting any throw charging he is doing and causing him to drop the boulder.
Physics big part of gameplay, defining the feeling of picking up and throwing the boulder, also impact.

Defining the gameplay:

- boulder pick-up and charge timers (influenced by biome type)
- boulder physics
- boulders provide different skills/perks
- player mobility (i.e. via double jump/air jump and roll/air roll and combo of both)
- level design (3-heights levels in different biomes)
- if the throwing player is higher than the player being thrown at, the boulder will be influenced by gravity and fall faster and do more damage
- detailed animations

Roadmap:
========

PreAlpha (2 weeks from project start) - Completed on 04.03.2015

- [x] simple rectangular arena
- [x] x and y axis movement
- [x] jumping
- [x] pne player
- [x] boulder, picking up and throwing the boulder
- [x] basic UI
- [x] programmer art

Alpha (4 weeks from project start)

- [x] two players
- [x] double jump and roll mechanic
- [x] throwing while moving
- [x] improved ui (health bars, etc)
- [x] basic impact
- [x] death & respawn

Beta (end of March 2015)

- [x] different types of bouders which influence gameplay while wielding the boulder:
- speed boulder (30% increased movement speed)
- big boulder (one-shots enemy player)
- shield boulder (less damage taken)
- jumpy boulder (30% higher jump)
- dash boulder (50% less dash cooldown)
- [x] basic level design (multi level arena with platforms etc)
- [x] three levels with different size
- [x] improved UI which offers level choice and shows the winning player

Final

- [ ] music and sounds
- [ ] final art
- [ ] final level design
- [ ] final biome count, generation

Review

- define overall future
- discuss game quality
- discuss sales feasibility

Gibrock plus
===

Backlog

- [ ] new input system
- [ ] jumping on stones
- [x] fix platform collision bug
- [x] hide mouse while ingame
- [ ] replays
- [ ] shield rock should save from instagib when player in full health
- [ ] single player level(s)
- [ ] shooting projection/assistance
- [ ] drop rock if it's hit by a flying rock
- [ ] camera smoothing
- [ ] ragdoll deaths
- [ ] minimum throw should not hurt player if no movement occurs
- [ ] end round screen polish

# The Wizard's Athenaeum

A regular workday at the Illusory Athenaeum is quickly turned into a magical crisis once Riley opens a long-forgotten tome, the ancient diary of Zoviar. Riley is thrown from the archives and must fight their way through magical enemies to save the patrons of the library, banish the evil wizard, and get their job back!

In The Wizard's Athenaeum, play as Riley, a frustrated archivist attempting to save her library from an evil wizard. Battle your way through each floor of the library up to the source of the wizard's power and destroy it. Collect spells and new tomes for your collection, rolling the dice of fate to mark your successes and failures. 

# Table of Contents:
1. [Current Prototype](#news)
2. [Level Design](#level)
3. [Enemies](#enemies)
4. [Spells](#spells)
    1. [Damaging Spells](#damage) 
        1. [Lightning Bolt](#lbolt) 
    2. [Area of Effect Spells](#aoe) 
        1. [Fireball](#fireball) 
    3. [Buffing Spells](#buff) 
        1. [Blessed](#blessed) 
5. [Current Bugs](#bugs)

# Current Prototype <a name="news"></a>

There are two scenes currently within the prototype. The scene "MainScene" contains a skeleton of what the main area may look like. The scene "LevelGeneration" shows the levels being generated, and creates unique levels for the player until they lose all of their health.

The player can currently move, attack the enemies when standing next to them, and can pick up scrolls. When the player reaches the exit area, the next level is generated. 

The enemies currently move towards the player, can attack the player when standing next to them, and disappear when they are killed by the player.

Scrolls can be picked up, though have no functionality yet.

The structure of the spell system is in the early stages of development, each spell is being determined on paper before being introduced to the game.

# Level Design <a name="level"></a>

The library features four floors, each with distinctive enemies and visuals. Each floor includes 5 rooms that are procedurally generated. The generation system is shown within the unity project using placeholder sprites. 

Levels are generated in an 8 by 8 grid, with bookshelves, spell scrolls, and enemies randomly generated in the middle. There are ranges for how many of each item can be place in the level. The player always begins in the bottom left corner and the exit is always in the top right corner.

In the generated levels: brown is a bookshelf, cream is a scroll, red is an enemy, and the player is shown as an adventurer sprite.

Here are some examples of generated levels:

![Example1]("The Wizard's Athenaeum/SampleLevels/Example1.png")

![Example2]("The Wizard's Athenaeum/SampleLevels/Example2.png")

![Example3]("The Wizard's Athenaeum/SampleLevels/Example3.png")

# Enemies <a name="enemies"></a>

Each floor of the library features different enemies. Enemies follow different walking patterns, some follow a set path, some tracking the player. The enemies featured in the unity project follow the player, tracking generally on the x-axis, and only moving on the y-axis when the player is directly above or below them. These enemies also move on every other turn, allowing the player to follow their movements. 

Floor 1 Enemies:
* Skeleton
* Ghost
* Giant rat

Floor 2 Enemies:
* Flying book
* Animated broom
* Animated armour

Floor 3 Enemies:
* Slime
* Awakened tree
* Centaur

Floor 4 Enemies:
* Dragon
* Witch
* Cyclops

Floor 5 Enemies:
* Zoviar

# Spells <a name="spells"></a>

There are three different categories of spells: damaging, area of effect, and buff. Each spell has benefits to the player. Spell scrolls are randomly generated throughout the library for the player to pick up and add to their spellbook. At the end of each run, the spellbook is wiped clean, causing the player to start fresh in a new run. As the player progresses and gains levels in the game, spells become easier to cast and higher level spells may be found.

## Damaging Spells <a name="damage"></a>

These spells causes damage to enemies one time.

### Lightning Bolt <a name="lbolt"></a>

A bolt of lightning strikes down on an enemy within range. The player may choose an enemy on an adjacent square, doing 5 damage.

## Area of Effect Spells <a name="aoe"></a>

These spells cause damage in a area of the board, though are harder to handle.

### Fireball <a name="fireball"></a>

A large explosion of fire appears where you desire, destroying your enemies. The player chooses a center point within range to cause the explosion with radius 2, doing 10 damage to each enemy in the area.

## Buffing Spells <a name="buff"></a>

These spells increase the players abilities. They may increase the damage of other spells, increase the hp of the player, increase the damage of physical attacks, etc.

### Blessed <a name="blessed"></a>

This spell casts a divine ward over the player, protecting them from harm. Take 50% less damage for 10 turns.

# Current Bugs: <a name="bugs"></a>
* Floor sorting layer is not consistent
    * Enemies hide behind floor tiles
    * Player sometimes cannot pass through floor tiles
* Attaching main area to level generation, new GameManager?

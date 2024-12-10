# Bowling Game

Welcome to **Bowling Game**, an immersive two-level physics-based bowling experience designed to test your precision and skill. Knock down all the pins in two throws to advance to the next level, or face defeat in this thrilling challenge. With realistic physics and dynamic gameplay mechanics, this game delivers a fun and engaging bowling simulation.

---

## Table of Contents
1. [Game Overview](#game-overview)
2. [Gameplay Mechanics](#gameplay-mechanics)
3. [Physics Details](#physics-details)
4. [Scripts Breakdown](#scripts-breakdown)
5. [How to Play](#how-to-play)
6. [Credits](#credits)

---

## Game Overview

### Objective
- Successfully knock down all the pins in **two throws**.
- Advance through **two levels**:
  1. **Easy Level**: Pins are arranged with a wider gap.
  2. **Harder Level**: Pins are closely packed, requiring precise aim.

### Rules
- If all pins are knocked down within two throws, proceed to the next stage.
- Failure to knock down all pins results in a **Game Over**.

---

## Gameplay Mechanics

1. **Pin Interaction**:
   - Each pin is individually tracked using the `PinController` script, which determines whether a pin is down based on its tilt angle.
   - Pins slow down naturally after being hit, following realistic physics rules.

2. **Ball Dynamics**:
   - The ball's movement is managed by the `BallController` script, simulating rolling and collisions with pins.

3. **Throw Turns**:
   - Players have two attempts per level to knock down all pins.
   - The `LevelManager` script tracks turns and handles transitions between levels.

4. **Power Gauge**:
   - A dynamic power slider (`SliderController` script) allows players to control the strength of their throws.
   - Time your power to achieve the optimal force for knocking down pins.

---

## Physics Details

The Bowling Game leverages Unity's **Rigidbody** and **Collider** components to create a realistic physics simulation. Below are the key physics methods used:

### 1. Pin Orientation Detection
- **Method**: `Vector3.Angle(transform.up, Vector3.up)`
- **Purpose**: Determines whether a pin has fallen by measuring the angle between its upward vector and the global upward vector.
- **Threshold**: Pins are considered down if the angle exceeds 45 degrees.

### 2. Slowdown Detection
- **Method**: `pinRB.linearVelocity.magnitude`
- **Purpose**: Checks if a pin's velocity is below a threshold (e.g., 0.1 m/s) to confirm it has stopped moving.

### 3. Power Slider
- **Dynamic Slider**: Gradually fills and un-fills to allow precise control of throw strength.
- **Implementation**: Utilizes Unity's `Slider` component with timed updates in the `SliderController` script.

---

## Scripts Breakdown

### 1. LevelManager
- Handles game progression and level transitions.
- Tracks pin states, throw turns, and determines win/loss conditions.

### 2. PinController
- Detects when a pin is knocked down using orientation and velocity checks.
- Removes downed pins to streamline gameplay.

### 3. SliderController
- Implements the power gauge mechanics, enabling players to control throw strength dynamically.

### 4. BallController
- Manages ball movement and collision detection with pins.
- Resets the ball's position after each throw.

---

## How to Play

1. **Start the Game**:
   - Launch the bowling game in Unity.

2. **Throw the Ball**:
   - Use the **Power Slider** to control the ball's speed.
   - Aim carefully and release the ball.

3. **Clear the Pins**:
   - Knock down all pins in two throws to proceed to the next level.

4. **Win or Lose**:
   - Clear all pins in the harder level to win the game.
   - Fail to clear the pins, and it's Game Over.

---

## Credits

- **Game Design and Programming**: Tal Sahar
- **Physics Mechanics**: Leveraged Unity's Rigidbody and Collider systems for realistic interactions.
- **Built With**: Unity Engine

---

Thank you for playing! Enjoy your bowling adventure!

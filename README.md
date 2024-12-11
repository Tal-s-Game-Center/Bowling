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
7. [Gameplay Image](#gameplay-image)

---

## Game Overview

### Objective
- Knock down all pins in **two throws**.
- Advance through **two levels**:
  1. **Easy Level**: Pins are arranged with a wider gap.
  2. **Harder Level**: Pins are closely packed, requiring precise aim.

### Rules
- Knock down all pins within two throws to proceed to the next stage.
- Fail to knock down all pins, and it's **Game Over**.

---

## Gameplay Mechanics

1. **Pin Interaction**:
   - Each pin is tracked using the `PinController` script to detect when it's knocked down by checking its tilt angle. In this script, we use the method `Vector3.Angle(transform.up, Vector3.up)` to measure the angle between the pin's orientation and the global upward direction to detect if the pin is down. If the angle exceeds 45 degrees, the pin is considered knocked over.

2. **Ball Dynamics**:
   - The `BallController` script manages the ball's movement, including interactions with the pins using physics. The ball's motion is controlled by applying force via Unity's `Rigidbody` component, and collisions with pins are detected automatically using Unity's built-in collision system.

3. **Throw Turns**:
   - Players have two attempts per level. The `LevelManager` script keeps track of turns and handles level transitions. This script checks whether all pins are knocked down and moves to the next level or ends the game based on the result.

4. **Power Gauge**:
   - The `SliderController` script allows players to control the strength of their throws via a dynamic power slider. This slider uses Unity's `Slider` component, and its value is updated over time to reflect the player's input, adjusting the throw's power accordingly.

---

## Physics Details

The game uses **Rigidbody** and **Collider** components for realistic physics simulations. Below are key methods used:

### 1. Pin Orientation Detection
- **Method**: `Vector3.Angle(transform.up, Vector3.up)`
- **Purpose**: Checks if the pin has fallen by comparing its tilt angle to the global up vector.
- **Threshold**: If the angle exceeds 45 degrees, the pin is considered down.

### 2. Slowdown Detection
- **Method**: `pinRB.linearVelocity.magnitude`
- **Purpose**: Confirms if a pin has stopped moving by checking if its velocity is below a threshold.

### 3. Power Slider
- **Method**: `Slider.value`
- **Purpose**: Adjusts the throw strength, providing control over the ball's speed.

---

## Scripts Breakdown

### 1. LevelManager

The `LevelManager` script handles the game progression, tracking throw turns and level transitions. It checks if all pins are knocked down and moves to the next level:

- The method `CheckWinConditions()` verifies whether all pins are knocked down. If true, it transitions to the next level.

### 2. PinController

The `PinController` script detects when a pin is knocked down using physics-based orientation checks:

- The method `CheckPinDown()` uses `Vector3.Angle(transform.up, Vector3.up)` to detect if the pin has fallen by comparing the pin's orientation to the global up vector.

### 3. SliderController

The `SliderController` script controls the dynamic power slider that adjusts the strength of each throw:

- The method `UpdateSlider()` updates the slider's value, adjusting the power over time using `Mathf.PingPong(Time.time, 1)`.

### 4. BallController

The `BallController` script manages the ball's movement and collision detection:

- The method `ThrowBall(float power)` applies a force to the ball using `ballRB.AddForce(transform.forward * power)`, where `power` is determined by the player's input via the power slider.

---

## How to Play

1. **Start the Game**:
   - Launch the bowling game in Unity.

2. **Throw the Ball**:
   - Use the **Power Slider** to control the throw speed.

3. **Clear the Pins**:
   - Knock down all pins in two throws to proceed.

4. **Win or Lose**:
   - Clear all pins in the harder level to win the game.
   - Fail to clear the pins, and it's **Game Over**.

---

## Credits

- **Game Design and Programming**: Tal Sahar
- **Physics Mechanics**: Leveraged Unity's Rigidbody and Collider systems for realistic interactions.
- **Built With**: Unity Engine

---

## Gameplay Image

Here’s a preview of the gameplay in action!

![צילום מסך 2024-12-11 030914](https://github.com/user-attachments/assets/4b922693-a6cc-40bc-a3c1-8a6b173004f1)


---

Thank you for playing! Enjoy your bowling adventure!

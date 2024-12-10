# Space Shooter

An endless arcade-style game where players pilot a spaceship to battle an unending stream of enemies, collect power-ups, and aim for the highest score. 
The game emphasizes performance optimization and modular design, featuring a generic object pooling system that supports subtypes for efficient resource management.

---

## Features

- **Spaceship Mechanics**: Intuitive controls for seamless movement and shooting, enabling an immersive gameplay experience.
- **Endless Gameplay**: Enemies spawn continuously, challenging players to survive as long as possible and achieve a high score.
- **Scoring System**: Tracks the player’s score based on the number of enemies destroyed.
- **Power-ups**:
  - **Shield**: Grants temporary protection against collisions and enemy attacks.
  - **Rapid Fire**: Increases the firing speed of the spaceship's weapons.
  - **Double Turret**: Two bullets gets fired parallely instead of one.
- **Generic Object Pooling**: Implements a reusable pooling system that supports subtypes, applied across bullets, explosions, enemies, and power-ups for 
optimized performance.

---

## __Learning Outcomes__

- **Object Pooling**: Developed a generic object pooling system supporting subtypes, enhancing resource efficiency and scalability.
- **Modular Architecture**: Designed reusable components for better maintainability and adaptability.
- **Gameplay Balancing**: Designed levels to maintain a balance between difficulty and player engagement.

---

## __Project Structure__

### **1. GameService**
The central manager that coordinates the game flow and facilitates seamless communication between all other services.


### **2. EnemyService**
Manages all enemy-related functionality, including spawning enemies, handling their behavior.
- **EnemyController**: Controls individual enemy behaviors, such as movement and collision handling.
- **EnemyView**: Displays the visual representation of enemies.
- **EnemyScriptableObject**: Defines enemy attributes, such as speed, health, and damage.
- **EnemyPool**: Implements object pooling to manage enemy reuse and improve performance.


### **3. PlayerService**
Manages the player's Controller and Bullet Pool Creation.
- **PlayerController**: Handles player actions like movement, shooting, and collision detection.
- **PlayerView**: Displays the player's spaceship visually.
- **PlayerScriptableObject**: Configures the player's attributes, such as health, movement speed, and fire rate etc.


### **4. PowerUpService**
Handles the spawning, functionality of power-ups in the game.
- **PowerUpController**: Manages the behavior and activation of individual power-ups.
  - **DoubleTurretController**: Enables dual weapon firing for a limited time.
  - **RapidFireController**: Increases the firing rate of the player's weapons temporarily.
  - **ShieldController**: Grants temporary invincibility to the player.
- **PowerUpView**: Handles the visual representation of power-ups during gameplay.
- **PowerUpType**: Enum categorizing different power-up types.
- **PowerUpScriptableObject**: Defines properties of power-ups, such as effect duration and type.
- **PowerUpPool**: Implements object pooling for power-ups to optimize spawning and reuse.
- **IPowerUp**: Interface for power-up behavior, allowing flexible implementation of unique effects for different power-ups.


### **5. BulletFiles**
No BulletService as all bullet launches happen by PlayerService.
- **BulletController**: Controls individual bullet behavior, such as movement and collision detection.
- **BulletView**: Displays the visual representation of bullets during gameplay.
- **BulletScriptableObject**: Defines bullet properties, such as speed and damage.
- **BulletPool**: Implements object pooling to optimize bullet reuse.
- **IBullet**: Interface for standardizing bullet-related functionality.


### **6. SoundService**
Responsible for managing sound effects and background music in the game.
- **SoundScriptableObject**: Stores all the sounds type and their respective audio file.
- **SoundType**: Enum for categorizing sound effects (e.g., background music, explosions, power-ups).
- **Sounds**: Holds the Structure to have a sound type and its respective audio file.


### **7. VFXService**
Manages all visual effects in the game, such as explosions and bullet trails, to enhance the player experience.
- **VFXController**: Controls the behavior and triggering of visual effects.
- **VFXView**: Displays visual effects during gameplay events and holds particle system for all VFX Type.
- **VFXType**: Enum categorizing various visual effect types (e.g., Explosion, Spark, Trail).
- **VFXPool**: Implements object pooling for visual effects to improve performance.


### **8. UIFiles**
UI Files manages display of UI Elements on screen.
- **UIView**: Handles the rendering and interaction of UI elements, such as score and health.


### **9. Utilities**
- **GenericMonoSingleton**: Provides a reusable singleton implementation for global services, such as GameService.
- **GenericObjectPool**: Implements a flexible pooling system that supports object subtypes, used for bullets, enemies, power-ups, and effects.
- **ScreenWrapperView**: Ensures objects exiting one side of the screen reappear on the opposite side, maintaining seamless gameplay.


### **10. Other Interfaces**

- **IDamageable**: Defines the contract for objects that can take damage, ensuring consistent implementation (e.g., for enemies and the player).

---

## __Setting Up the Project__

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/123rishiag/Space-Shooter.git
   ```
2. Open the project in Unity.

---

## __Video Demo__

[__Watch the Gameplay Video__](https://www.loom.com/share/27b550f09f634da7b98f62cb72a99d4a?sid=9899da8c-9484-452c-8fd3-1889449c164b)

## __Play Link__

[__Play the Game__](https://outscal.com/narishabhgarg/game/play-space-shooter-2-game)
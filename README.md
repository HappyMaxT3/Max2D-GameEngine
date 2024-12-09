# Max2D-GameEngine

---

## The best 2D Game Engine in the World...
Max2D is a simple and powerful 2D engine written in C# using Windows Forms. This engine is ideal for creating lightweight 2D games, providing an intuitive interface and development functionality. It's easy to extend and add your own modules!

---

## How to Get Started?
1. **Install the Project**  
   Download and open the engine in Visual Studio.

   Ensure that all necessary dependencies are installed and included in your project:
   - **C#**
   - **Windows Forms**
   - **.NET Framework**
   - **NAudio** (if audio functionality is required)

2. **Check Engine Modules**  
   Verify that all engine modules and classes are included in the project. The engine comes with a demo game (*Treasure Hunter*) located in the `DemoGame` folder. This demo showcases the engine's functionality and capabilities.  

   To test the engine, build and run the demo game in Visual Studio.  

3. **Organize Your Game Files**  
   All engine modules are stored in the `GameEngine` folder.  
   - To create your own game, add a separate folder for your game files and use the engine's modules to streamline development.  
   - Game assets such as sprites, music, and UI elements are stored in the `Assets` folder.

4. **Build and Release**  
   Once your game is ready, prepare it for release in Visual Studio by building an `.exe` file for distribution.

---

## Basics

### Folder structure
```
Max2D-GameEngine/
├── Assets/                 # Folder containing all game resources
│    ├── Sprites/               # Images and textures for the game
│    ├── Media/                 # Audio files (music, sound effects, etc.)
│    └── UI/                    # UI elements (buttons, icons, panels, etc.)
│
├── GameEngine/             # Core modules and classes for the game engine
│    ├── AnimatedSprite2D.cs    # Handles animations for 2D sprites
│    ├── AudioManager.cs        # Manages audio playback and sound effects
│    ├── GameEngine.cs          # The main engine class, controls game loops and rendering
│    ├── Log.cs                 # Provides logging functionality for debugging
│    ├── Shape2D.cs             # Base class for creating 2D shapes
│    ├── Sprite2D.cs            # Manages 2D sprite rendering and positioning
│    └── Vector2D.cs            # A utility class for 2D vector operations (position, scaling, etc.)
│
├── DemoGame.cs             # A demo game showcasing the engine's capabilities
│          
├── Program.cs              # The entry point of the application
│
├── Max2D-GameEngine.csproj # The project file for Visual Studio, defines project settings and references
│    
└── README.md               # Documentation file (you are here!)
```

### Key Components
1. **`DemoGame.cs`**:
   - **Description**: The main class for the game, handling game setup, loading, drawing, and updating the game state.
   - **Constructor (`DemoGame`)**:
     - Initializes the game window size and title.
   - **`OnLoad()`**:
     - Sets up the background color, camera zoom, and loads all the necessary sprites and sounds.
     - Creates the game map from the `Map` array, setting up non-interactive and interactive elements.
     - Initializes the player and sets its initial position.
   - **`OnDraw()`**:
     - Placeholder for drawing game elements each frame.
   - **`OnUpdate()`**:
     - Updates the player's position based on input.
     - Checks for collisions with obstacles and collectible items.
     - Plays sound effects for collisions and item collection.
     - Handles the display of the winner screen when all collectible items are collected.
   - **`GetKeyDown(KeyEventArgs e)`**:
     - Handles input when keys are pressed.
     - Moves the player in the respective direction based on the key pressed.
   - **`GetKeyUp(KeyEventArgs e)`**:
     - Handles input when keys are released.
     - Stops the player movement when the key is released.
   - **`DisplayWinnerImage()`**:
     - Displays the winner screen when all items are collected.
     - Plays a winner sound.
     - Auto-disappears the winner screen after 5 seconds.

2. **`Sprite2D.cs`**:
   - **Purpose**: Represents a 2D sprite on the game screen. Used for background, obstacles, collectibles, and the player.
   - **Usage**:
     - To create a new sprite, you need to specify its position, scale, and reference to the asset image.
     - Tags are used to identify what the sprite represents (e.g., "Obstacle", "Collectible", "Player").
   - **Example Usage**:
     ```csharp
     new Sprite2D(new Vector2(i * 60, j * 40), new Vector2(100, 20), new Sprite2D("Sprites/Tiles/sand"), "Obstacle");
     ```

3. **`AudioManager.cs`**:
   - **Purpose**: Manages all audio for the game, including background music and sound effects.
   - **Usage**:
     - To load a sound effect or background music, specify the file path, volume, and a unique name.
     - To play a sound, use its name to reference and play it.
   - **Example Usage**:
     ```csharp
     AudioManager.LoadSound("Media/collectSound", 0.5f, "CollectSound");
     AudioManager.PlaySound("CollectSound");
     ```

4. **Game Map**:
   - The game map is defined in a 2D array. Each cell represents a tile or an element on the game screen.
   - `b` - Background
   - `r` - Rock
   - `t` - Stalactite
   - `l` - Cliff
   - `f` - Cliff Corner
   - `o` - Horizontal Rock
   - `s` - Sand
   - `h` - Shell
   - `c` - Chest (Collectible)
   - `g` - Gold (Collectible)
   - `p` - Player start position
   - `w` - Seaweed
   You can use other signs to draw the **Map**.

5. **Player Movement**:
   - The player can move up, down, left, or right using the W, S, A, and D keys.
   - Collision checks ensure that the player cannot move through obstacles.
   - Animated movement plays an animation when moving and stops when not moving.

6. **Collision and Interaction**:
   - The game checks for collisions with obstacles and collectible items.
   - When the player collects an item (chest or gold), it triggers a sound and removes the item from the screen.
   - If the player hits an obstacle, the player is moved back to the last valid position.

7. **Winner Screen**:
   - Once all collectible items are collected, a winner screen is displayed.
   - It plays a winning sound and auto-dismisses after 5 seconds.

### Controls
- **W**: Move Up
- **S**: Move Down
- **A**: Move Left
- **D**: Move Right

### Improvements
- Add more levels to extend gameplay.
- Implement additional enemy mechanics.
- Add more collectible items and challenges.
- Improve the visual and audio experience with more refined assets.


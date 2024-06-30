# Swing-It Shroomies

## Description

**Swing-It Shroomies** is an engaging and adventurous platformer game where players control cute mushroom characters, the Shroomies, navigating through various levels filled with obstacles and challenges. This game leverages fun and engaging swing mechanics for an immersive user experience. The main objective is to help the Shroomies swing from one platform to another, avoid obstacles, and collect items along the way to reach their destination.

### Background
Swing-It Shroomies is my latest significant project, completed during the third term of my first year in college. I participated in this project as a Game Engineer, utilizing C# to build the game. This project allowed me to refine my skills in game logic and UI design, making the gameplay both exciting and rewarding.

### Gameplay
The game revolves around helping the Shroomies swing through various levels. Players control the Shroomies by swinging them from one platform to another, requiring precise timing and strategic use of the swing mechanics to navigate through increasingly challenging levels. The primary gameplay involves:
- **Swing Mechanics**: Players use swing mechanics to propel the Shroomies across gaps and obstacles.
- **Obstacle Avoidance**: Timing and strategy are essential to avoid various obstacles that can hinder progress.
- **Item Collection**: Collect items scattered throughout the levels to gain points and unlock new challenges.

### Development Contributions
In this project, I worked extensively on the game logic and user interface (UI), ensuring smooth and intuitive gameplay. My responsibilities included:
- **Game Logic**: Developing the conditions for successful swings, collision detection, and item collection.
- **UI Design**: Designing a user-friendly and visually appealing interface to enhance the overall player experience.

### Levels and Challenges
The game features various levels, each presenting unique challenges and obstacles. As players progress, they encounter:
- **Increasing Difficulty**: Levels become more challenging with complex obstacles and narrower timing windows.
- **Unique Obstacles**: Each level introduces new obstacles, requiring players to adapt and strategize.
- **Rewarding Gameplay**: Successfully navigating through levels and collecting items provides a sense of accomplishment and encourages continued play.

### Reception
Swing-It Shroomies was well-received by my peers and instructors, earning praise for its innovative mechanics and engaging gameplay. The project was a valuable learning opportunity, and I am proud of the contributions I made to its development.

## Project Structure

### Root Directory
- **Project-4/**: Contains the main project directory.
  - **.git/**: Git repository containing version control information.
  - **.gitignore**: Specifies files and directories to be ignored by git.
  - **changelog.txt**: Contains the changelog of the project.
  - **GXPEngine.sln**: Visual Studio solution file for the project.
  - **GXPEngine/**: Contains the source code and related files for the game engine.
    - **.DS_Store**: macOS file for folder attributes (can be ignored or deleted).
    - **.vs/**: Visual Studio specific files.
    - **app.config**: Configuration file for the application.
    - **bin/**: Directory for binary files and compiled executables.
    - **Game/**: Contains game-specific code and assets.
    - **GXPEngine.csproj**: C# project file for the game engine.
    - **GXPEngine/**: Source code for the game engine.
    - **MyGame.cs**: Main game file.
    - **obj/**: Directory for object files and intermediate outputs.
    - **Vec2.cs**: Source file for vector mathematics.

## Getting Started

### Prerequisites
- Visual Studio or a compatible C# development environment.

### Building the Project
1. Clone the repository or download the project files.
2. Open the `GXPEngine.sln` solution file in Visual Studio.
3. Build the solution using Visual Studio (Build > Build Solution).

### Running the Game
- After building the solution, navigate to the `bin` directory and run the executable.

### Game Details
- **MyGame.cs**: The main game logic is implemented here. This file contains the core mechanics of the game, including the swinging motion and character controls.
- **Vec2.cs**: Contains vector math functionalities used in the game to calculate movement and physics interactions.
- **Game/**: This directory contains additional game-specific code and assets, such as levels, graphics, and sound effects.

## Controls
- Use the arrow keys or WASD to move the shroomie character.
- Press the space bar to initiate the swing motion.
- Navigate through levels by swinging across gaps, avoiding obstacles, and collecting items.

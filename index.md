# INF633 - Advanced 3D Graphics (2021 - 2022)

------

[Github Repository](https://github.com/edualvarado/inf633-2021-2022) | [Github Page](https://edualvarado.github.io/inf633-2021-2022/) | [Moodle](https://moodle.polytechnique.fr/enrol/index.php?id=13007)

------

- [First steps](#Firststeps)
	- [Introduction](#Introduction)
	- [Installing Unity](#InstallingUnity)
	- [First setup in Unity](#FirstsetupinUnity)
	- [Unity interface and controls](#Unityinterfaceandcontrols)
	- [3D controls](#3Dcontrols)
	- [Interface and interaction](#Interfaceandinteraction)
- [Starting our project](#Startingourproject)
	- [Opening the project](#Openingtheproject)
	- [Running the scene](#Runningthescene)


------

<a name="Firststeps"></a>
## First Steps

<a name="Introduction"></a>
### Introduction

Welcome to the official page for the lab sessions of INF633! 

The goal of this project is to create, starting from a flat empty terrain, one with interesting features, objects and dynamic, living creatures all interacting together. For this, multiple edition tools will be created and basic principles of animation and crowd simulation used to give life to the virtual world.

In this page, you will find the same introduction to install and setup Unity that you can find also in the README.md of the repository at [https://github.com/edualvarado/inf633-2021-2022](https://github.com/edualvarado/inf633-2021-2022), and also the description and content for each lab session.

<a name="InstallingUnity"></a>
### Installing Unity

Link to download: [https://unity.com/download](https://unity.com/download)

- Download Unity Hub. It's a explorer tool for your projects where you can have simultaneously different Unity builds.
- Create an Unity account: It will be required when you launch the program. You can select the free option for personal use.
- Choose your Unity build. For this lab, we will be using **2020.3.21f1 LTS**
- Make sure you have a text editor installed and configured for C#. [Visual Studio Code](https://code.visualstudio.com/Download) is a popular option, but you can use any other code editor or IDE.
- Now you should be able to open Unity. Make sure that you can successfully launch it, that your graphics drivers are working or that you do not have any particular problem.

<a name="FirstsetupinUnity"></a>
### First Setup in Unity

Before even importing the lab project, let's have a general overview of Unity. First, we first need to associate Unity with our chosen C# editor.

- In Unity, go to `Edit > Preferences > External Tools`
- Change `External Script Editor` to your preferred text editor.
- Make sure that your text editor is configured to handle C#.
- From now on, when you are writing your scripts, self-completion for the Unity API among other tools will be enable while you program your scripts in Unity.

<a name="Unityinterfaceandcontrols"></a>
### Unity Interface and Controls

Unity uses a modular window system. That means, that each part of the interface can be reorganized and placed as you like. By default, Unity will show a similar appearance to this one:

<img src="https://docs.unity3d.com/uploads/Main/Editor-Breakdown.png" alt="Image" style="zoom: 80%;" />

- **A - Top bar**. Here you can find some common tools such as Move, Rotate or Scale. Also, you can find the **Play** button to start your application, or **Pause** button, to stop it in the current frame. When it is paused, the **Step** button is available to go to the next frame.
- **B - Hierarchy**. Here are all the elements in your *Scene*. Those elements (commonly named as *Game Objects* in Unity) can be anything: 3D models, cameras, illumination... On top of that, you find the *Scene*. A `Scene` is the parent element that serves as a environment where you place all your *Game Object*. 
- **C** - **Scene view**. This window is the editor where you edit all your *Game Objects* before clicking **Play** in the normal Unity workflow.
- **D - Game view.** This window shows the rendered, running application when you click in **Play**. It will show whatever *Display* you have decided to show. Each of these displays is associated to a *Camera Game Object*. Keep in mind that you could still interact with the Game view during the **Play** mode by scripting, as you might do in any game.
- **E - Inspector**. One of the most important parts. Each *Game Object* has an Inspector associated that describes it. These descriptions are based on *Components* (for example, a *Transform Component* would describe the position, rotation and scale of the *Game Object* which has been associated to). You can attach the built-in components that Unity brings, or your own scripts in C#.
- **F - Project**. Here you can find all your assets of Unity, such as your 3D models, scripts, textures, materials...
- **G - Console**. The last line provided by the console, that displays errors, warnings or any other message that you have programmed to show during run-time. Next to the Project window, you can find the Console window where the entire space is dedicated to debugging.

<a name="3Dcontrols"></a>
### 3D Controls

In the Scene view:

- **Left click** to select a *Game Object*.
- **Right click** to rotate the camera on itself.
  - **Right click + WASD** to move the camera in the environment.
- **Middle click** to pan the camera.
- **Scroll wheel** to zoom in/out.
- **F** to focus the camera on the selected *Game Object*.
- **Alt + Left click** to rate the camera around the focus point.

<a name="Interfaceandinteraction"></a>
### Interface and Interaction

Some fast tips regarding the overall workflow in Unity:

- If you have errors in your scripts, they will show up in red in the bottom left corner. Otherwise, you can use the Console window.
- Any variable that you set as `public` will be visible in the interface and editable in real-time. Although during this lab we will not focus on the code syntax and efficiency, normally it is a good practice to declared the variable as `private`  and serialized it to make it visible in the Unity inspector using `[SerializeField]`.

------

<a name="Startingourproject"></a>
## Starting our project

<a name="Openingtheproject"></a>
### Opening the project

- Clone this repository.
- Make sure you have Unity Hub and an Unity built installed (as we describe in *Introduction*)
- In Unity Hub, click in `Open > Add project from disk`
- Select the parent folder of the repository, containing the Unity project. You will differentiate it because it contains folders such as `Assets` or `Packages`.
- Open the project by selecting the installed built of Unity. That's all!

<a name="Runningthescene"></a>
### Running the scene

- To run the project, just click on the **Play** button at the top menu. The windows will be colored when the project is running.
- For the lab session project:
  - Add new brushes before running the project, by selecting the terrain and drag-and-dropping the script in its inspector.
  - Select the terrain and click on the **Use** button of one of the brushes
  - You can then paint on the terrain (in the game view) with your brush.
  - Click the **Play** button again to stop. Changes will be saved.

------

## SESSION 01 - Terrain edition

### Getting started

In this session, you will be designing and implementing brushes, allowing you to dynamically edit the terrain. Your brushes will extend the base class
TerrainBrush and control terrain modifications in a restricted area around the mouse, by implementing the method draw.

The base code for this session can be found in the following file, and shows how to set all cells in the current region to a defined height.

```csharp
public class SimpleBrush : TerrainBrush {

    public float height = 5;

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                terrain.set(x + xi, z + zi, height);
            }
        }
    }
```
<p align=center>Code Snippet 1: "Scripts/01Terrain Brushes/SimpleBrush.cs"</p>

Copy this file to use it as a base for your own brushes, by changing the name of the copied file and class name.

### Brush ideas

- Simple brush (square shape - already done!)
- Flat brush (to fix terrain always to "0")
- Incremental increase/decrease
- Gaussian increase/decrease
- Smooth/healing brush
- Random brush
- Different brush shapes (square, circle, etc)
- Volume-preserving brush (move matter around instead of removing/adding it)
- Erosion brush (simulates water droplets falling in the region, eroding the terrain)
- Coherent noise brush (Perlin Noise or other approach, using Mathf.PerlinNoise or your own implementation)

### Examples

<p align="center">
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/simple-brush.gif" width="200">
&nbsp; &nbsp;
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/fixed-brush.gif" width="200">
&nbsp; &nbsp;
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/gauss-brush.gif" width="200">
</p>

<p align="center">
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/smooth-brush.gif" width="200">
&nbsp; &nbsp;
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/random-brush.gif" width="200">
&nbsp; &nbsp;
    <img src="https://edualvarado.github.io/inf633-2021-2022/01-TerrainBrushGifs/variablegauss-brush.gif" width="200">
</p>

### Useful functions and variables

```csharp
// Get or set the height of the terrain at (x, z)
  float terrain.get(int x, int z);
  void terrain.set(int x, int z, float height);
  // Get the terrain normal at (x, z)
  Vector3 terrain.getNormal(float x, float z);

  // Get the terrain size
  Vector3 terrain.gridSize();

  // Reset the whole terrain to height=0
  void terrain.reset();

  // Print to the Unity console (next to game tab)
  print("message");
  // Print to game viewport (top left)
  terrain.debug.text = "message";
```
<p align=center>Code Snippet 2: Useful functions</p>

```csharp
  // Access the underlying terrain
  CustomTerrain terrain;

  // Radius of the brush
  int radius;
  int terrain.brush_radius;
```
<p align=center>Code Snippet 3: Useful variables</p>

## SESSION 02 - Object Placement

In this second session, you will design brushes that place objects on the terrain. Here it will be trees, but you can search for or provide other objects.

The base code for this session shows how to instantiate one object where the user clicked, and four at the corners of the drawing region. Note that these types of brushes extend the \texttt{InstanceBrush} class instead of the TerrainBrush one used in the previous session.

```csharp
public class SimpleInstanceBrush : InstanceBrush {

    public override void draw(float x, float z) {
        spawnObject(x, z);
        spawnObject(x - radius, z - radius);
        spawnObject(x - radius, z + radius);
        spawnObject(x + radius, z - radius);
        spawnObject(x + radius, z + radius);
    }
}
```
<p align=center>Code Snippet 4: "Scripts/02\_Instance Brushes/SimpleInstanceBrush.cs"</p>

Like in the previous session, you can use this file as a base for your own brushes by copying it and changing the file and class names.

_IMAGE_

You can set the object that will be instantiated by drag-and-dropping a model in the Object\_prefab parameter of the terrain at run-time. A few models of trees are already in the project, in Standard Assets > Environment > SpeedTree and then the file with a tree icon in one of the three sub-folders.

To remove objects, you can use the default tools provided in the original terrain editor by Unity. For this, select the terrain and go to the \texttt{terrain > Paint trees}
tab (in the inspector). You can then shift-click on the terrain to remove objects around your cursor. Note that this works outside of play mode, as opposed to the brushes you implement.

### Brush ideas

- Random placement in a square
- Random placement in a circle
- Placement on an grid, optionally rotated (crops)
- Placement in small clusters of objects (bushes, groves)
- Minimal distance brush, that makes sure that every placed object has $>$\texttt{x} free space
- Terrain-related placement:
-- Prevent placement if the terrain is too steep
-- Prevent placement depending on the altitude

### Object ideas

- Trees
- Bushes
- Rocks
- Buildings

### Useful functions and variables

```csharp
  // Spawns an object at coordinates (x, z)
  void spawnObject(float x, float z);

  // Returns the interpolated height of the terrain
  float terrain.getInterp(float x, float z);
  // Returns the steepness of the terrain
  float terrain.getSteepness(float x, float z);

  // Return the number of objects instantiated
  int terrain.getObjectCount();
  // Return the location of an object
  Vector3 terrain.getObjectLoc(int index);
  // ADVANCED - Return an instantiated object
  // See Unity manual of TreeInstance for more details
  TreeInstance terrain.getObject(int index);
```
<p align=center>Code Snippet 5: Useful functions</p>

```csharp
  // Object to instantiate by default
  GameObject terrain.object_prefab;
  // Min and max size of the instantiated objects
  float terrain.min_scale;
  float terrain.max_scale;
```
<p align=center>Code Snippet 6: Useful variables</p>


## SESSION 03 - Character Animation

Work in progress...

## SESSION 03 - Crowds and Evolution

Work in progress...


---

Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [GitHub Flavored Markdown](https://guides.github.com/features/mastering-markdown/).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/edualvarado/inf633-2021-2022/settings/pages). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://support.github.com/contact) and we’ll help you sort it out.

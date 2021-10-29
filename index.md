# INF633 - Advanced 3D Graphics (2021 - 2022)

Welcome to the lab sessions of INF633! The goal of this project is to create, starting from a flat empty terrain, one with interesting features, objects and dynamic, living creatures all interacting together. For this, multiple edition tools will be created and basic principles of animation and crowd simulation used to give life to the virtual world.

## Setup and Global Info

### Unity Launch

- Download and extract the project archive from Moodle
- Launch Unity
- Create/log into your Unity account
- Open the project by clicking the "Add" button and selecting the extracted folder
- Once the project is opened, in the project file explorer, go to Assets/Scenes and open SampleScene.unity

### Text editor setup

- In Unity, go to Edit > Preferences > External Tools
- Change External Script Editor to your preferred text editor
- Make sure that your text editor is configured to handle C#

### 3D controls
- Left click to select an object
- Right click to rotate the camera on itself
- Right click+WASD to move the camera in the environment
- Middle click to pan the camera
- Scroll wheel to zoom/unzoom
- Alt+Left click to rotate the camera around the focus point

### Interface and interaction
- If you have errors in your scripts, they will show up in \textcolor{red}{red} in the bottom left corner
- You can also see errors in the console tab, next to the game tab
- Any variable that you set as \texttt{public} will be visible in the interface and editable in real-time


### Running the project
- Add new brushes before running the project, by selecting the terrain and drag-and-dropping the script in its inspector
- To run the project, click on the play button at the top. The button will turn blue when the project is running
- Select the terrain and click on the use button of one of the brushes
- You can then paint on the terrain (in the game view) with your brush
- Click the play button again to stop

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

- Incremental increase/decrease
- Gaussian increase/decrease
- Smooth/healing brush
- Random brush (add details)
- Different brush shapes (square, circle, etc)
- Volume-preserving brush (move matter around instead of removing/adding it)
- Erosion brush (simulates water droplets falling in the region, eroding the terrain)
- Coherent noise brush (Perlin Noise or other approach, using Mathf.PerlinNoise or your own implementation)

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

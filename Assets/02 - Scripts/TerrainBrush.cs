using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerrainBrush : Brush {

    public override void callDraw(float x, float z) {
        Vector3 grid = terrain.world2grid(x, z);
        draw((int)grid.x, (int)grid.z);
        terrain.save();
    }

    public override void draw(float x, float z) {
        draw((int)x, (int)z);
    }
}

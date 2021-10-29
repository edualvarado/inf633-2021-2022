using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInstanceBrush : InstanceBrush {

    public override void draw(float x, float z) {
        spawnObject(x, z);
        spawnObject(x - radius, z - radius);
        spawnObject(x - radius, z + radius);
        spawnObject(x + radius, z - radius);
        spawnObject(x + radius, z + radius);
    }
}

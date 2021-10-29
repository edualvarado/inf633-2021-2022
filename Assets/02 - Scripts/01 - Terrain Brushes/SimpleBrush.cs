using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBrush : TerrainBrush {

    public float height = 5;

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                terrain.set(x + xi, z + zi, height);
            }
        }
    }
}

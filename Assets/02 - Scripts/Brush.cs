using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Brush : MonoBehaviour {

    protected CustomTerrain terrain;
    private bool active = false;

    public int radius {
        get {
            return terrain.brush_radius;
        }
    }

    void Start() {
        terrain = GetComponent<CustomTerrain>();
    }

    public void deactivate() {
        if (active)
            terrain.setBrush(null);
        active = false;
    }
    public void activate() {
        Brush active_brush = terrain.getBrush();
        if (active_brush)
            active_brush.deactivate();
        terrain.setBrush(this);
        active = true;
    }
    public void toggle() {
        if (isActive())
            deactivate();
        else
            activate();
    }
    public bool isActive() {
        return active;
    }

    public virtual void callDraw(float x, float z) {
        draw(x, z);
    }
    public abstract void draw(float x, float z);
    public abstract void draw(int x, int z);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneticAlgo : MonoBehaviour {

    [Header("Genetic algorithm parameters")]
    public int pop_size = 100;
    public GameObject prefab;

    [Header("Dynamic elements")]
    public float vegetation_growth_rate = 1.0f;
    private float curr_growth;

    private List<GameObject> animals;

    protected Terrain terrain;
    protected CustomTerrain cterrain;
    protected float width, height;

    void Start() {
        terrain = Terrain.activeTerrain;
        cterrain = GetComponent<CustomTerrain>();

        curr_growth = 0.0f;

        animals = new List<GameObject>();
        width = terrain.terrainData.size.x;
        height = terrain.terrainData.size.z;
        for (int i = 0; i < pop_size; i++) {
            GameObject animal = makeAnimal();
            animals.Add(animal);
        }
    }

    void Update() {
        while (animals.Count < pop_size / 2) {
            animals.Add(makeAnimal());
        }

        updateResources();
        cterrain.debug.text = animals.Count.ToString() + " animals";
    }

    public void updateResources() {
        Vector2 detail_sz = cterrain.detailSize();
        int[,] details = cterrain.getDetails();
        curr_growth += vegetation_growth_rate;
        while (curr_growth > 1.0f) {
            int x = (int)(UnityEngine.Random.value * detail_sz.x);
            int y = (int)(UnityEngine.Random.value * detail_sz.y);
            details[y, x] = 1;
            curr_growth -= 1.0f;
        }
        cterrain.saveDetails();
    }

    public GameObject makeAnimal(Vector3 position) {
        GameObject animal = Instantiate(prefab, transform);
        animal.GetComponent<Animal>().setup(cterrain, this);
        animal.transform.position = position;
        animal.transform.Rotate(0.0f, UnityEngine.Random.value * 360.0f, 0.0f);
        return animal;
    }
    public GameObject makeAnimal() {
        Vector3 scale = terrain.terrainData.heightmapScale;
        float x = UnityEngine.Random.value * width;
        float z = UnityEngine.Random.value * height;
        float y = cterrain.getInterp(x/scale.x, z/scale.z);
        return makeAnimal(new Vector3(x, y, z));
    }

    public void addOffspring(Animal parent) {
        GameObject animal = makeAnimal(parent.transform.position);
        animal.GetComponent<Animal>().inheritBrain(parent.getBrain(), true);
        animals.Add(animal);
    }

    public void removeAnimal(Animal animal) {
        animals.Remove(animal.transform.gameObject);
        Destroy(animal.transform.gameObject);
    }

}

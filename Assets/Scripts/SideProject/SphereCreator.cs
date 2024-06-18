using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCreator : MonoBehaviour
{
    [SerializeField] public int numberOfSpheres;
    public GameObject spherePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Create initial spheres
        for (int i = 0; i < numberOfSpheres; i++) {
            CreateSphere();
        }

    }

    public void CreateSphere() {
        // Duplicate spheres at random positions
        for (int i = 0; i < numberOfSpheres; i++) {
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
            GameObject newSphere = Instantiate(spherePrefab, randomPosition, Quaternion.identity);

            // Color the instantiated sphere's material
            Renderer renderer = newSphere.GetComponent<Renderer>();
            Material material = renderer.material;
            material.color = GetRandomColor();
        }
    }

    Color GetRandomColor() {
        int randomColorIndex = Random.Range(0, 3);
        switch (randomColorIndex) {
            case 0:
                return Color.red;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            default:
                return Color.white; // Fallback to white color
        }
    }
}

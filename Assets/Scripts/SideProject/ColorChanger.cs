using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject cube; // Reference to the cube

    void OnCollisionEnter(Collision collision) {
        // Check if the collider is a sphere
        if (collision.collider.CompareTag("Sphere")) {
            // Get the sphere's material color
            Color sphereColor = collision.collider.GetComponent<Renderer>().material.color;

            // Change the cube's material color to match the sphere's color
            GetComponent<Renderer>().material.color = sphereColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform arCamera;
    [SerializeField] private GameObject ballPrefab;

    private GameObject currentBall;

    private void BallInitialSetup() {
        currentBall = Instantiate(ballPrefab, new Vector3(0, 1000, 0), Quaternion.identity);
    }
}

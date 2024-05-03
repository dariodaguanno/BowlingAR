using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PinDeckController : MonoBehaviour {

    [SerializeField] private Transform arCamera;
    [SerializeField] private GameObject bowlingLanePrefab;
    [SerializeField] private GameObject pinDeckPrefab;
    [SerializeField] private PlaneFinderBehaviour planeFinder;

    [SerializeField] private GameState gameState;

    private GameObject pinDeckClone;
    private Transform pinDeckSpawnPoint;

    private bool pinDeckCreated;

    private void Awake() {
        // set ar camera position for editor testing
#if UNITY_EDITOR
        arCamera.transform.position = new Vector3(0, 1.4f, 4);
        arCamera.transform.eulerAngles = new Vector3(
            arCamera.transform.eulerAngles.x,
            arCamera.transform.eulerAngles.y + 180,
            arCamera.transform.eulerAngles.z
        );
#endif
    }

    private void Update() {
        // For testing purposes, start and create pin deck on mouse click

#if UNITY_EDITOR
        if (!pinDeckCreated) {
            if (Input.GetMouseButtonDown(0)) {
                pinDeckCreated = true;
                CreatePinDeck();
                Debug.Log("Left button clicked, CreatePinDeck() called");
            }
        }
#endif
    }

    void OnEnable() {
        gameState.OnBallPlayEnd.AddListener(StartBallPlayEnded);
    }

    void OnDisable() {
        gameState.OnBallPlayEnd.RemoveListener(StartBallPlayEnded);    
    }

    public void CreatePinDeck() {
        StartCoroutine(SetupBowlingLaneRoutine());
    }

    private IEnumerator SetupBowlingLaneRoutine() {

        Transform defaultPlaneIndicator = planeFinder.PlaneIndicator.transform;

        Vector3 directionTowardsCamera = defaultPlaneIndicator.position + arCamera.position;

#if UNITY_EDITOR
        directionTowardsCamera = defaultPlaneIndicator.position - arCamera.position;
#endif

        directionTowardsCamera.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(-directionTowardsCamera, Vector3.up);

        GameObject bowlingLaneClone = Instantiate(bowlingLanePrefab, defaultPlaneIndicator.position, lookRotation);

        pinDeckSpawnPoint = bowlingLaneClone.transform.Find("PinDeckSpawnPoint");

        pinDeckClone = Instantiate(pinDeckPrefab, pinDeckSpawnPoint.position, pinDeckSpawnPoint.rotation);

        yield return new WaitForSeconds(1);

        gameState.CurrentGameState = GameState.GameStateEnum.SetupBalls;
    }

    void StartBallPlayEnded() {
        Debug.Log("BallPlayEnded()");

        StartCoroutine(BallPlayEnded());
    }

    IEnumerator BallPlayEnded() {
        yield return new WaitForSeconds(2);

        gameState.CurrentGameState = GameState.GameStateEnum.TurnEnd;

    }
}

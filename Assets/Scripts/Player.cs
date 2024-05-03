using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform arCamera;
    [SerializeField] private GameObject ballPrefab;

    private GameObject currentBall;

    private Vector2 touchInitialPosition;
    private Vector2 touchFinalPosition;

    private float ySwipeDelta;

    [SerializeField] GameState gameState;

    private void Awake() {

        gameState.OnEnterBallSetup.AddListener(EnableSelf);

        gameState.ResetState();
        gameState.CurrentGameState = GameState.GameStateEnum.PlacingPinDeckAndLane;
    }

    private void Start() {
        BallInitialSetup();
    }

    private void Update() {

        switch (gameState.CurrentGameState) {
            case GameState.GameStateEnum.ReadyToThrow:
                // track touch to throw, device only
                DetectScreenSwipe();

#if UNITY_EDITOR
                // desktop editor only, track mouse button to throw
                if (Input.GetMouseButtonDown(1)) ThrowBall();
#endif
                break;

            case GameState.GameStateEnum.BallInPlay:
                // if the ball falls below -20, you have ended the play
                if (currentBall.transform.position.y < -20) {
                    Debug.Log("PLAYER PLAY END!");

                    // reset ball
                    currentBall.transform.position = new Vector3(0, 1000, 0);
                    currentBall.transform.rotation = Quaternion.identity;

                    // reset rigidbody force
                    Rigidbody rb = currentBall.GetComponent<Rigidbody>();
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.useGravity = false;

                    // ball has been thrown, set ball play end state
                    gameState.CurrentGameState = GameState.GameStateEnum.BallPlayEnd;
                }
                break;
        }
    }

    private void OnEnable() {
        BallInitialSetup();
    }

    private void OnDisable() {
        gameState.OnEnterBallSetup.RemoveListener(EnableSelf);
    }

    void EnableSelf() {
        enabled = true;
    }

    private void BallInitialSetup() {
        currentBall = Instantiate(ballPrefab, new Vector3(0, 1000, 0), Quaternion.identity);
        gameState.CurrentGameState = GameState.GameStateEnum.ReadyToThrow;
    }

    private void DetectScreenSwipe() {
        foreach (var touch in Input.touches) {

            if (touch.phase == TouchPhase.Began) {

                touchInitialPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended) {
                
                touchFinalPosition = touch.position;

                if (touchFinalPosition.y > touchInitialPosition.y) {

                    ySwipeDelta = touchFinalPosition.y - touchInitialPosition.y;
                }

                ThrowBall();

            }
        }
    }

    private void ThrowBall() {
        
        currentBall.GetComponent<Rigidbody>().useGravity = true;

        float throwPowerMultiplier = 60.0f;

        Quaternion lookRotation = arCamera.rotation;

#if UNITY_EDITOR
        // store camera and mouse position and convert to a world direction
        Camera cam = arCamera.GetComponent<Camera>();
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseDir = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.farClipPlane));

        // store rotation direction
        lookRotation = Quaternion.LookRotation(mouseDir, Vector3.up);

        // override swipe and power for editor only
        ySwipeDelta = 1.5f;
        throwPowerMultiplier = 60.00f;
#endif

        currentBall.transform.position = arCamera.position;
        currentBall.transform.rotation = lookRotation;

        Vector3 forceVector = currentBall.transform.forward * (ySwipeDelta * throwPowerMultiplier);

        currentBall.GetComponent<Rigidbody>().AddForce(forceVector, ForceMode.Impulse);

        gameState.RemainingBalls--;
        gameState.CurrentGameState = GameState.GameStateEnum.BallInPlay;
    }
}

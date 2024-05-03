using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/CreateGameStateAsset")]
public class GameState : ScriptableObject {

    public enum GameStateEnum {
        TitleScreen,
        PlacingPinDeckAndLane,
        SetupBalls,
        ReadyToThrow,
        BallInPlay,
        BallPlayEnd,
        StrikeAchieved,
        TurnEnd,
        ResettingDeck,
        GameEnded
    }

    [SerializeField] private int score = 0;
    [SerializeField] private int remainingBalls = 0;
    [SerializeField] private int currentTurn = 0;
    [SerializeField] private int maxTurns = 5;
    [SerializeField] private int strikeCounter = 0;
    [SerializeField] private int strikeExtraPoints = 10;
    [SerializeField] private float throwPowerMultiplier = 0.05f;

    [HideInInspector] public UnityEvent<int> OnScoreChanged;
    [HideInInspector] public UnityEvent OnEnterBallSetup;
    [HideInInspector] public UnityEvent OnReadyToThrow;
    [HideInInspector] public UnityEvent OnBallInPlay;
    [HideInInspector] public UnityEvent OnBallPlayEnd;
    [HideInInspector] public UnityEvent OnStrikeAchieved;
    [HideInInspector] public UnityEvent OnResettingDeck;
    [HideInInspector] public UnityEvent OnGameEnded;

    [SerializeField] private GameStateEnum currentGameState;

    public GameStateEnum CurrentGameState {
        get => currentGameState;
        set {
            currentGameState = value;

            switch (value) {
                case GameStateEnum.SetupBalls:
                    OnEnterBallSetup?.Invoke();
                    break;
                case GameStateEnum.ReadyToThrow:
                    OnReadyToThrow?.Invoke();
                    break;
                case GameStateEnum.BallInPlay:
                    OnBallInPlay?.Invoke();
                    break;
                case GameStateEnum.BallPlayEnd:
                    OnBallPlayEnd?.Invoke();
                    break;
                case GameStateEnum.StrikeAchieved:
                    OnStrikeAchieved?.Invoke();
                    break;
                case GameStateEnum.ResettingDeck:
                    OnResettingDeck?.Invoke();
                    break;
                case GameStateEnum.GameEnded:
                    OnGameEnded?.Invoke();
                    break;
            }
        }
    }

    public int Score {
        get => score;
        set {
            score = value;
            OnScoreChanged?.Invoke(score);
        }
    }

    public int RemainingBalls {
        get => remainingBalls;
        set => remainingBalls = value;
    }
    public int CurrentTurn {
        get => currentTurn;
        set => currentTurn = value;
    }
    public int StrikeCounter {
        get => strikeCounter;
        set => strikeCounter = value;
    }
    public int MaxTurns {
        get => maxTurns;
        set => maxTurns = value;
    }
    public int StrikeExtraPoints {
        get => strikeExtraPoints;
        set => strikeExtraPoints = value;
    }
    public float ThrowPowerMultiplier {
        get => throwPowerMultiplier;
        set => throwPowerMultiplier = value;
    }

    public void ResetState() {
        currentTurn = 1;
        score = 0;
        remainingBalls = MaxTurns;
    }

    public void LoadNewScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameStateEnum currentGameState;

    public GameStateEnum CurrentGameState {
        get => currentGameState;
        set => currentGameState = value;
    }

    [SerializeField] private int score = 0;

    public int Score {
        get => score;
        set => score = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private TMP_Text scoreUI;

    [SerializeField] private GameObject nextTurnPanel;
    [SerializeField] private float turnWaitTime = 3;

    [SerializeField] private GameObject placePinDeckPanel;
    [SerializeField] private GameObject controlsPanel_1;
    [SerializeField] private GameObject controlsPanel_2;

    [SerializeField] private TMP_Text remainingBallsUI;

    [SerializeField] private GameObject strikePanel;
    [SerializeField] private GameObject gameOverScreen;

    void UpdateScoreUI(int newScore) {
        scoreUI.text = $"{newScore}";
    }

    void UpdateAmountOfBallsUI() {
        remainingBallsUI.text = $"{gameState.RemainingBalls}";
    }

    public void ShowNextTurnUI() {
        
        strikePanel.SetActive(false);

        StartCoroutine(ShowNextTurnRoutine());
    }

    IEnumerator ShowNextTurnRoutine() {

        Debug.Log("Show next turn");
        
        gameState.CurrentTurn++;

        if (gameState.CurrentTurn <= gameState.MaxTurns) {
            nextTurnPanel.SetActive(true);
            nextTurnPanel.GetComponentInChildren<TMP_Text>().text = $"Turn {gameState.CurrentTurn}";

            yield return new WaitForSeconds(turnWaitTime);

            nextTurnPanel.SetActive(false);
            gameState.CurrentGameState = GameState.GameStateEnum.ResettingDeck;
        }
        else {
            gameState.CurrentGameState = GameState.GameStateEnum.GameEnded;
        }
        
        yield return new WaitForSeconds(turnWaitTime);

    }

    public void ShowStrikeUI() {
        strikePanel.SetActive(true);
    }

    public void ShowGameOverScreen() {
        strikePanel.SetActive(false);
        gameOverScreen.SetActive(true);
    }

}

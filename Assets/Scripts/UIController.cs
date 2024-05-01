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

    void UpdateScoreUI(int newScore) {
        scoreUI.text = $"{newScore}";
    }

    public void ShowNextTurnUI() {
        //UNCOMMENT
        //strikePanel.SetActive(false);

        StartCoroutine(ShowNextTurnRoutine());
    }

    IEnumerator ShowNextTurnRoutine() {

        Debug.Log("Show next turn");

        //UNCOMMENT
        /*
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
        */
        yield return new WaitForSeconds(turnWaitTime);

    }
}

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

    [SerializeField] private bool throwInstructionShowed = false;


    void UpdateScoreUI(int newScore) {
        scoreUI.text = $"{newScore}";
    }

    void UpdateAmountOfBallsUI() {
        remainingBallsUI.text = $"{gameState.RemainingBalls}";
    }

    private void OnEnable() {
        gameState.OnScoreChanged.AddListener(UpdateScoreUI);
        gameState.OnEnterBallSetup.AddListener(HidePlaceInDeckPanel);
        gameState.OnBallInPlay.AddListener(UpdateAmountOfBallsUI);
    }

    private void OnDisable() {
        gameState.OnScoreChanged.RemoveListener(UpdateScoreUI);
        gameState.OnEnterBallSetup.RemoveListener(HidePlaceInDeckPanel);
        gameState.OnBallInPlay.RemoveListener(UpdateAmountOfBallsUI);
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

    void HidePlaceInDeckPanel() {
        // hide place pin panel and show control instructions
        placePinDeckPanel.SetActive(false);

        ShowControls();
    }

    private void ShowControls() {
        if (throwInstructionShowed) return;

        throwInstructionShowed = true;
        controlsPanel_1.SetActive(true);

        Invoke("HideControls_1", 3);
    }

    void HideControls_1() {
        controlsPanel_1.SetActive(false);
        controlsPanel_2.SetActive(true);

        Invoke("HideControls_2", 3);
    }

    void HideControls_2() {
        controlsPanel_2.SetActive(false);
    }

}

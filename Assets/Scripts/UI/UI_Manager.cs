using DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour , IDependencyProvider
{
    [SerializeField] InstructionsBehaviour instructionsMenu;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] WinMenu winMenu;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;

    [SerializeField] Button pauseButton;
    [SerializeField] Button verifyAnswerButton;

    private void Start()
    {
        CloseInstructionsPanel();
        ClosePausePanel();
        winPanel.SetActive(false);
    }
    private void OpenPausePanel()
    {
        pausePanel.SetActive(true);
    }
    private void ClosePausePanel()
    {
        pausePanel.SetActive(false);
    }
    public void OnGameWinned()
    {
        winPanel.SetActive(true);
        pauseMenu.ClosePauseMenu();
        instructionsMenu.gameObject.SetActive(false);
    }

    private void OpenInstructionsPanel()
    {
        pauseMenu.ClosePauseMenu();
        instructionsMenu.gameObject.SetActive(true);
    }
    private void CloseInstructionsPanel()
    {
        pauseMenu.OpenPauseMenu();
        instructionsMenu.gameObject.SetActive(false);
    }

    public void AssignButtonsActions(UnityAction pauseAction , UnityAction verifyAnswerAction , UnityAction unPauseAction , UnityAction quitGameAction)
    {
        pauseButton.onClick.AddListener(OpenPausePanel);

        pauseButton.onClick.AddListener(pauseAction);
        verifyAnswerButton.onClick.AddListener(verifyAnswerAction);

        pauseMenu.AssignPauseActions2AllButtons(ClosePausePanel, null, null);
        pauseMenu.AssignPauseActions2AllButtons(unPauseAction, OpenInstructionsPanel, quitGameAction);

        instructionsMenu.AddListeners2Button(CloseInstructionsPanel);

        winMenu.AddListener2Buttons(null, quitGameAction);
    }

    private void OnDestroy()
    {
        pauseButton.onClick.RemoveAllListeners();
        verifyAnswerButton.onClick.RemoveAllListeners();
    }

    [Provide]
    public UI_Manager ProvideUI_Manager()
    {
        return this;
    }
}

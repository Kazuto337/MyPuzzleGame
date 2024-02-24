using DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [Inject]
    GameManager gameManager;

    [SerializeField] InstructionsController instructions;
    [SerializeField] PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu.OnInstructionsOpen.AddListener(OpenInstructions);
        pauseMenu.OnExitGame.AddListener(CloseGameOption);
        pauseMenu.OnResumeGame.AddListener(ClosePauseMenu);
    }

    public void OpenPauseMenu()
    {
        gameManager.PauseGame();
    }
    public void ClosePauseMenu()
    {
        gameManager.ResumeGame();
    }

    public void OpenInstructions()
    {
        instructions.ShowInstructions();
    }
    public void CloseInstructions()
    {
        instructions.HideInstructions();
    }

    public void CloseGameOption()
    {
        gameManager.CloseGame();
    }
}

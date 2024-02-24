using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button resumeGameBttn, ExitGameBttn, openInstructionsBttn;

    public UnityEvent OnResumeGame, OnExitGame, OnInstructionsOpen;

    private void Start()
    {
        resumeGameBttn.onClick.AddListener(OnResumeButtonClicked);
        ExitGameBttn.onClick.AddListener(OnExtiButtonClicked);
        openInstructionsBttn.onClick.AddListener(OnInstruccitionsButtonOpen);
    }
    public void OnResumeButtonClicked()
    {
        OnResumeGame.Invoke();
    }
    public void OnExtiButtonClicked()
    {
        OnExitGame.Invoke();
    }
    public void OnInstruccitionsButtonOpen()
    {
        OnInstructionsOpen.Invoke();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button unPauseGameButton;
    [SerializeField] Button openInstructionsButton;
    [SerializeField] Button quitGameButton;

    public void OpenPauseMenu()
    {
        gameObject.SetActive(true);
    }
    public void ClosePauseMenu()
    {
        gameObject.SetActive(false);
    }
    public void AssignPauseActions2AllButtons(UnityAction unPauseAction = null, UnityAction openInstructionsAction = null, UnityAction quitGameAction = null)
    {
        if (unPauseAction != null)
        {
            unPauseGameButton.onClick.AddListener(unPauseAction); 
        }
        if (openInstructionsAction != null)
        {
            openInstructionsButton.onClick.AddListener(openInstructionsAction); 
        }
        if (quitGameAction != null)
        {
            quitGameButton.onClick.AddListener(quitGameAction); 
        }
    }

    private void OnDestroy()
    {
        unPauseGameButton.onClick?.RemoveAllListeners();
        openInstructionsButton.onClick?.RemoveAllListeners();
        quitGameButton.onClick?.RemoveAllListeners();
    }
}

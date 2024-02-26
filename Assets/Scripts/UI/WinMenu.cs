using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] Button repeatGameButton , quitGameButton;

    public void AddListener2Buttons(UnityAction repeatAction = null, UnityAction quitGameAction = null)
    {
        if (repeatAction != null)
        {
            repeatGameButton.onClick.AddListener(repeatAction); 
        }
        if (quitGameButton != null)
        {
            quitGameButton.onClick.AddListener(quitGameAction);
        } 
    }
}

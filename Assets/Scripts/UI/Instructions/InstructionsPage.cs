using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsPage : MonoBehaviour
{
    [SerializeField] TMP_Text message;
    [SerializeField] Image pageImage;

    public void ConvertData(string dataMessage , Sprite pageSprite)
    {
        message.text = dataMessage;
        pageImage.sprite = pageSprite;
    }
}

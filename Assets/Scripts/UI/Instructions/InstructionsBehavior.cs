using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsBehavior : MonoBehaviour
{
    How2PlayData data;
    [SerializeField] Button nextPageBttn, previousPageBttn, closeBttn;

    [SerializeField] List<InstructionPage> pages; 

    public void LoadData(How2PlayData newData)
    {
        data = newData;
        foreach (InstructionPage item in pages)
        {
            item.pageImage.sprite = data.PagesImages[item.name];//The Page GameObject must have the same name as the ones in the JSON
            item.pageText.text = data.PagesText[item.name];
        }
    }
}

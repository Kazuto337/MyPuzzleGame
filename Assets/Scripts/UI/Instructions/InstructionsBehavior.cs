using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsBehavior : MonoBehaviour
{
    How2PlayData data;
    [SerializeField] Button nextPageBttn, previousPageBttn, closeBttn;
    [SerializeField] TMP_Text pagesIndexTXT;
    int currentPage;

    [SerializeField] List<InstructionPage> pages;

    private void Start()
    {
        currentPage = 0;
    }
    public void LoadData(How2PlayData newData)
    {
        data = newData;
        foreach (InstructionPage item in pages)
        {
            item.pageImage.sprite = data.PagesImages[item.name];//The Page GameObject must have the same name as the ones in the JSON
            item.pageText.text = data.PagesText[item.name];
        }

        PrintCurrentPage();
    }

    public void ChangePage(int direction)
    {
        pages[currentPage].gameObject.SetActive(false);
        int newIndex = (currentPage + direction) % pages.Count;
        if (newIndex < 0)
        {
            newIndex = pages.Count - 1;
        }

        currentPage = newIndex;
        PrintCurrentPage();
    }

    private void PrintCurrentPage()
    {
        pagesIndexTXT.text = currentPage.ToString() + " / " + pages.Count;
        pages[currentPage].gameObject.SetActive(true);
    }
}

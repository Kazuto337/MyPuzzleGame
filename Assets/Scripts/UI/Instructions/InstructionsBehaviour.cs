using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InstructionsBehaviour : MonoBehaviour
{
    List<InstructionsPage> uiPages = new List<InstructionsPage>();
    Instructions_DTO instructionsData;

    [Header("Pages Config")]
    [SerializeField] GameObject pagesPrefab;
    [SerializeField] GameObject pagesOrigin;

    [Header("Menu Option")]
    [SerializeField] Button closeInstrucionsButton;

    [Header("Pages Controls"), Space(20f)]
    [SerializeField] Button previousPageButton;
    [SerializeField] Button nextPageButton;
    int currentPage = 0;
    [SerializeField] TMP_Text currentPageTxt;

    private void Awake()
    {
        CreatePages();
        previousPageButton.onClick.AddListener(() => { ChangePage(-1); });
        nextPageButton.onClick.AddListener(() => { ChangePage(1); });
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void AddListeners2Button(UnityAction action)
    {
        closeInstrucionsButton.onClick.AddListener(action);
    }

    private string[] ReadDataFromFiles()
    {
        string jsonFilePath = Application.streamingAssetsPath + "/HowToPlay.txt";
        string json = File.ReadAllText(jsonFilePath);

        try
        {
            instructionsData = JsonUtility.FromJson<Instructions_DTO>(json);
        }
        catch (System.Exception error)
        {
            Debug.LogException(error);
        }

        string folderPath = Application.streamingAssetsPath + "/Sprites/Instructions";
        string[] pngFiles = Directory.GetFiles(folderPath, "*.png");

        return pngFiles;
    }

    private Texture2D LoadTextureFromFile(string dataPath)
    {
        byte[] fileData = File.ReadAllBytes(dataPath);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        return texture;
    }
    private void CreatePages()
    {
        string[] spritesFound = ReadDataFromFiles();
        int i = 0;
        foreach (var item in instructionsData.pages)
        {
            GameObject newPage = Instantiate(pagesPrefab, pagesOrigin.transform);
            newPage.name = item.imageName;

            Texture2D texture = LoadTextureFromFile(spritesFound[i]);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            newPage.GetComponent<InstructionsPage>().ConvertData(item.message, sprite);

            uiPages.Add(newPage.GetComponent<InstructionsPage>());
            newPage.SetActive(false);
            i++;
        }

        uiPages[0].gameObject.SetActive(true);
        currentPageTxt.text = (currentPage + 1) + " / " + uiPages.Count;
    }

    private void ChangePage(int pageDirection)
    {
        uiPages[currentPage].gameObject.SetActive(false);

        int newIndex = (currentPage + pageDirection) % uiPages.Count;
        if (newIndex < 0)
        {
            newIndex = uiPages.Count - 1;
        }
        currentPage = newIndex;
        currentPageTxt.text = (currentPage + 1) + " / " + uiPages.Count;

        uiPages[currentPage].gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        closeInstrucionsButton.onClick.RemoveAllListeners();
    }
}

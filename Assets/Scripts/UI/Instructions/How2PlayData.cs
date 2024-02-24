using DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class How2PlayData : IDependencyProvider
{
    private Instructions _instructionsDTO;
    private Dictionary<string, Sprite> pagesImages;
    private Dictionary<string, string> pagesText;

    public Instructions Instructions { get => _instructionsDTO; }
    public Dictionary<string, Sprite> PagesImages { get => pagesImages;}
    public Dictionary<string, string> PagesText { get => pagesText;}

    public How2PlayData()
    {
        ReadInstruccionsFromFile();
        LoadPages();
    }

    private void ReadInstruccionsFromFile()
    {
        string jsonFilePath = Application.streamingAssetsPath + "/HowToPlay.txt";
        string json = File.ReadAllText(jsonFilePath);

        _instructionsDTO = new Instructions();
        try
        {
            _instructionsDTO = JsonUtility.FromJson<Instructions>(json);
        }
        catch (System.Exception error)
        {
            Debug.LogException(error);
        }
    }

    private void LoadPages()
    {
        pagesImages = new Dictionary<string, Sprite>();
        try
        {
            string[] files = Directory.GetFiles(Application.dataPath + "/Sprites/Instructions", "*.png");
            foreach (var item in _instructionsDTO.pages)
            {
                Texture2D texture = LoadTextureFromFile(files[i]);
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                pagesImages[item.imageName] = newSprite;
                pagesText[item.imageName] = item.message;
            }
        }
        catch (System.Exception error)
        {
            Debug.LogException(error);
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(1920, 1080);
        texture.LoadImage(fileData);
        return texture;
    }

    [Provide]
    public How2PlayData ProvideInstructionsData()
    {
        return this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DependencyInjection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameManager : MonoBehaviour, IDependencyProvider, IMovementVerifier
{
    public static GameManager Instance;

    [Inject] private PuzzleEngine _puzzleEngine;

    [SerializeField] TMP_Text feedbackTxt;
    [SerializeField] GameObject winnerPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        _puzzleEngine.VerifyMovement(cube, movementVector);
    }
    public void CheckAnswer()
    {
        string filePath = Application.streamingAssetsPath + "/FinalMatrix.txt";

        if (File.Exists(filePath))
        {
            LoadAnswerFromJson(filePath);
        }
        else
        {
            Debug.LogError($"File FinalMatrix.txt not found on Folder.");
        }
    }

    private void LoadAnswerFromJson(string jsonFilePath)
    {

        string json = File.ReadAllText(jsonFilePath);
        Matrix_DTO mapData = JsonUtility.FromJson<Matrix_DTO>(json);

        Dictionary<int, Vector3Int> mapDictionary = CreateMapDictionary(mapData.map);
        Dictionary<int, Vector3Int> gameMap = _puzzleEngine.ExportMapDictionary();

        IsAnswerCorrect(mapDictionary , gameMap);
    }
    private Dictionary<int, Vector3Int> CreateMapDictionary(List<MapItem> mapItems)
    {
        Dictionary<int, Vector3Int> mapDictionary = new Dictionary<int, Vector3Int>();

        foreach (MapItem item in mapItems)
        {
            string[] coordinatesArray = item.coordinates.Split(',');
            int x = int.Parse(coordinatesArray[0]);
            int y = int.Parse(coordinatesArray[1]);
            int z = int.Parse(coordinatesArray[2]);

            mapDictionary[item.value] = new Vector3Int(x, y, z);
        }

        return mapDictionary;
    }

    private bool IsAnswerCorrect(Dictionary<int, Vector3Int> map1, Dictionary<int, Vector3Int> map2)
    {
        if (map1.Count != map2.Count)
        {
            return false;
        }

        foreach (var key in map1.Keys)
        {
            if (!map2.ContainsKey(key))
            {
                return false;
            }

            if (map1[key] != map2[key])
            {
                return false;
            }
        }

        return true;
    }

}


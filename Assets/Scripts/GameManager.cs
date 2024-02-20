using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DependencyInjection;
using System.IO;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, IDependencyProvider, IMovementVerifier
{
    public static GameManager Instance;

    [Inject] private PuzzleEngine _puzzleEngine;

    [SerializeField] TMP_Text feedbackTxt;
    [SerializeField] TMP_Text answerTxt;
    [SerializeField] GameObject winnerPanel;

    Dictionary<int, Vector3Int> answersMap;

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

    private void Start()
    {
        LoadAnswerFromJson();
        foreach (var key in answersMap.Keys)
        {
            answerTxt.text += key + " = " + answersMap[key] + "\n";
        }
    }
    public void CheckAnswer()
    {
        Dictionary<int, Vector3Int> gameMap = _puzzleEngine.ExportCubesMap();

        if (IsAnswerCorrect(answersMap, gameMap))
        {
            feedbackTxt.color = Color.green;
            feedbackTxt.text = "Correct Answer";
            winnerPanel.SetActive(true);
        }
    }
    private void LoadAnswerFromJson()
    {
        string jsonFilePath = Application.streamingAssetsPath + "/FinalMatrix.txt";
        string json = File.ReadAllText(jsonFilePath);

        Matrix_DTO mapData = new Matrix_DTO();
        try
        {
            mapData = JsonUtility.FromJson<Matrix_DTO>(json);
        }
        catch (System.Exception error)
        {
            Debug.LogException(error);
            feedbackTxt.text = error.Message;
        }

        answersMap = ConvertMapItem2Dictionary(mapData.map);
    }
    private Dictionary<int, Vector3Int> ConvertMapItem2Dictionary(List<MapItem> mapItems)
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
                feedbackTxt.text = "Incorrect Answer";
                return false;
            }
        }
        return true;
    }
    public void VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        _puzzleEngine.VerifyMovement(cube, movementVector);
    }

}


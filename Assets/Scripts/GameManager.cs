using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DependencyInjection;
using System.IO;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Inject]
    UI_Manager uiManager;

    [Inject]
    private PuzzleEngine _puzzleEngine;

    [SerializeField] TMP_Text feedbackTxt;
    [SerializeField] TMP_Text answerTxt;

    Dictionary<int, Vector3Int> answersMap;

    [SerializeField] PlayerInput inputManager;
    private void Start()
    {
        uiManager.AssignButtonsActions(PauseGame, CheckAnswer, ResumeGame, CloseGame);
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

            uiManager.OnGameWinned();
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

    public void PauseGame()
    {
        inputManager.enabled = false;
    }

    public void ResumeGame()
    {
        inputManager.enabled = true;
    }
    public void CloseGame()
    {
        Application.Quit();
    }

}


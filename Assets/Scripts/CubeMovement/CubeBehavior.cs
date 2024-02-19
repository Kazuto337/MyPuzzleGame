using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    int _ID; //Helps Identify Cubes by JSON

    [SerializeField] GameObject controls;
    bool isSelected = false;
    bool isMovable;
    [SerializeField] List<TMP_Text> textBoxes;

    public bool IsMovable { get => isMovable; }

    public void Construct(int _ID, bool _isMovable)
    {
        isMovable = _isMovable;
        this._ID = _ID;

        if (_isMovable == false)
        {
            Destroy(controls);
        }

        foreach (TMP_Text item in textBoxes)
        {
            item.text = _ID.ToString();
        }
    }
    public void OnCubeSelected()
    {
        if (isMovable == false)
        {
            return;
        }

        isSelected = true;
        controls.SetActive(true);
    }
    public void OnCubeDeselected()
    {
        isSelected = false;
        controls.SetActive(false);
    }
    public void Try2Move(Vector3Int movementVector)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.VerifyMovement(this , movementVector);
    }
}

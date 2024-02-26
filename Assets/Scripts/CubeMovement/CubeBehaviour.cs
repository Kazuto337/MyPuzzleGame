using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CubeBehaviour : MonoBehaviour
{
    UnityEvent<CubeBehaviour, Vector3Int> OnArrowPressed;

    int _ID; //Helps Identify Cubes by JSON

    [SerializeField] GameObject controls;
    bool isSelected = false;
    bool isMovable;

    [SerializeField] List<TMP_Text> textBoxes;
    [SerializeField] List<TMP_Text> coordBoxes;

    public bool IsMovable { get => isMovable; }
    public int ID { get => _ID; }

    private void Start()
    {
        OnCubeDeselected();
    }

    public void Construct(int _ID, bool _isMovable)
    {
        isMovable = _isMovable;
        this._ID = _ID;

        if (_isMovable == false)
        {
            Destroy(controls);
            GetComponent<Renderer>().material.color = Color.blue;
        }

        foreach (TMP_Text item in textBoxes)
        {
            item.text = _ID.ToString();
        }
    }
    public void Construct(int _ID, bool _isMovable , UnityAction<CubeBehaviour, Vector3Int> action)
    {
        isMovable = _isMovable;
        this._ID = _ID;
        OnArrowPressed = new UnityEvent<CubeBehaviour, Vector3Int>();
        if (_isMovable == false)
        {
            Destroy(controls);
            GetComponent<Renderer>().material.color = Color.blue;
        }

        foreach (TMP_Text item in textBoxes)
        {
            item.text = _ID.ToString();
        }

        OnArrowPressed.AddListener(action);
    }
    public void OnCubeSelected()
    {
        if (isMovable == false)
        {
            return;
        }

        isSelected = true;
        controls.SetActive(isSelected);
    }
    public void OnCubeDeselected()
    {
        if (isMovable == false)
        {
            return;
        }
        isSelected = false;
        controls.SetActive(isSelected);
    }
    public void PrintCoord(Vector3Int coord)
    {
        foreach (TMP_Text item in coordBoxes)
        {
            item.text = "<" + coord.x + " , " + coord.y + " , " + +coord.z + ">";
        }
    }
    public void InvokeMovement(Vector3Int direction)
    {
        OnArrowPressed.Invoke(this, direction);
    }

    private void OnDestroy()
    {
        OnArrowPressed?.RemoveAllListeners();
    }
}

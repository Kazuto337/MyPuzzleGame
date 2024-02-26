using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    CubeBehaviour cube;
    private Vector3Int matrixPosition;
    bool isEmpty = true;

    public CubeBehaviour Cube { get => cube; }
    public bool IsEmpty { get => isEmpty; }

    public void Construct(Vector3Int _matrixPosition, CubeBehaviour _cube = null)
    {
        matrixPosition = _matrixPosition;
        cube = _cube;

        if (cube != null)
        {
            isEmpty = false;
            return;
        }
        isEmpty = true;
    }

    public void AddCube(CubeBehaviour newCube)
    {
        isEmpty = false;
        cube = newCube;
        cube.PrintCoord(matrixPosition);
    }

    public void RemoveCube()
    {
        isEmpty = true;
        cube = null;
    }
}

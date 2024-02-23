using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
    CubeBehavior cube;
    private Vector3Int matrixPosition;
    bool isEmpty = true;

    public CubeBehavior Cube { get => cube; }
    public bool IsEmpty { get => isEmpty; }

    public void Construct(Vector3Int _matrixPosition, CubeBehavior _cube = null)
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

    public void AddCube(CubeBehavior newCube)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    CubeBehavior cube;
    public Vector3Int matrixPosition;
    bool isEmpty = true;

    public CubeBehavior Cube { get => cube; }
    public bool IsEmpty { get => isEmpty;}

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

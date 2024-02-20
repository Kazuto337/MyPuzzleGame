using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    CubeBehavior cube;
    public Vector3Int matrixPosition;
    bool isEmpy = true;

    public CubeBehavior Cube { get => cube; }
    public bool IsEmpty { get => isEmpy;}

    public void AddCube(CubeBehavior newCube)
    {
        isEmpy = false;
        cube = newCube;
        cube.PrintCoord(matrixPosition);
    }

    public void RemoveCube() 
    {
        isEmpy = true;
        cube = null;
    }
}

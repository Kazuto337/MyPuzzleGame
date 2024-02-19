using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    CubeBehavior cube;
    public Vector3 matrixPosition;
    bool isEmpy = true;

    public CubeBehavior Cube { get => cube; }
    public bool IsEmpy { get => isEmpy;}

    public void AddCube(CubeBehavior newCube)
    {
        isEmpy = false;
        cube = newCube;
    }

    public void RemoveCube() 
    {
        isEmpy = true;
        cube = null;
    }
}

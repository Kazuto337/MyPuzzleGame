using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    CubeBehavior cube;

    public CubeBehavior Cube { get => cube; }

    public bool IsEmpty()
    {
        return cube == null;
    }
    public void AddCube(CubeBehavior newCube)
    {
        cube = newCube;
    }

    public void RemoveCube() 
    { 
        cube = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    [SerializeField] Arrow_SO arrowData;
    [SerializeField] CubeBehavior cubeBehavior;

    public void MoveCubve()
    {
        Debug.Log("ArrowPressed: " + gameObject.name);
        cubeBehavior.Try2Move(arrowData.MovementFactor);
    }
}

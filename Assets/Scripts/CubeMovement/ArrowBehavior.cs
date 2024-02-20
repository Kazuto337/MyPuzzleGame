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
        Try2Move(arrowData.MovementFactor);
    }
    public void Try2Move(Vector3Int movementVector)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.VerifyMovement(cubeBehavior, movementVector);
    }
}

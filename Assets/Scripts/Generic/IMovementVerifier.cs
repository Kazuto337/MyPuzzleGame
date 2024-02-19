using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementVerifier
{
    public Vector3 VerifyMovement(CubeBehavior cube, Vector3Int movementVector);
}

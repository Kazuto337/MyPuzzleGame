using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementVerifier
{
    public void VerifyMovement(CubeBehaviour cube, Vector3Int movementVector);
}

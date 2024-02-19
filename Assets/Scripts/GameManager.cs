using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DependencyInjection;

public class GameManager : MonoBehaviour, IDependencyProvider, IMovementVerifier
{
    public static GameManager Instance;

    [Inject] private PuzzleEngine _puzzleEngine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        return _puzzleEngine.VerifyMovement(cube, movementVector);
    }
}

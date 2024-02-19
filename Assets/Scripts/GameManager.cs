using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour , IMovementVerifier
{
    public static GameManager Instance;

    private PuzzleEngine _puzzleEngine;

    public void Construct(PuzzleEngine puzzleEngine)
    {
        _puzzleEngine = puzzleEngine;
    }

    private void Awake()
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

    public Vector3 VerifyMovement(CubeBehavior cube , Vector3Int movementVector)
    {
        return _puzzleEngine.VerifyMovement(cube, movementVector);
    }
}

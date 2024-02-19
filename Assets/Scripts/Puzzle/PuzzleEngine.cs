using DependencyInjection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleEngine : MonoBehaviour, IMovementVerifier, IDependencyProvider
{
    Slot[,,] puzzleMatrix;
    [SerializeField] GameObject matrixOrigin;
    [SerializeField] GameObject cubePrefab;

    private void Start()
    {
        puzzleMatrix = new Slot[3,3,3];
        CreatePuzzle();
    }

    #region CreatePuzzle
    public void CreatePuzzle()
    {
        FillSlotMatrix();

        GameObject newCube = Instantiate(cubePrefab, puzzleMatrix[1, 0, 0].transform);
        CubeBehavior indexCubeBehavior = newCube.GetComponent<CubeBehavior>();
        indexCubeBehavior.Construct(1, false);

        puzzleMatrix[0, 0, 1].AddCube(indexCubeBehavior);

        //FemalePosition
        GameObject newCube1 = Instantiate(cubePrefab, puzzleMatrix[1, 0, 2].transform);
        indexCubeBehavior = newCube.GetComponent<CubeBehavior>();
        indexCubeBehavior.Construct(8, false);

        puzzleMatrix[2, 0, 1].AddCube(indexCubeBehavior);

        //Final Position
        GameObject newCube2 = Instantiate(cubePrefab, puzzleMatrix[1, 2, 1].transform);
        CubeBehavior newCubeBehavior = newCube.GetComponent<CubeBehavior>();
        newCubeBehavior.Construct(4, false);

        puzzleMatrix[1, 2, 1].AddCube(newCubeBehavior);

        //AddRandomCubes();
    }

    private void FillSlotMatrix()
    {
        Vector3 slotPosition = Vector3.zero;
        Vector3 slotScale = Vector3.one;
        for (int i = 0; i < puzzleMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleMatrix.GetLength(1); j++)
            {
                for (int k = 0; k < puzzleMatrix.GetLength(2); k++)
                {
                    GameObject slot = new GameObject();
                    slot.name = "slot";
                    slot.AddComponent<Slot>();
                    slot.transform.localScale = slotScale;

                    slot.transform.SetParent(matrixOrigin.transform);
                    slotScale = slot.transform.localScale;

                    slot.transform.position = slotPosition;

                    slotPosition.z -= slotScale.z; //Each Iteration moves the slot one unit Based on the gameObject scale

                    puzzleMatrix[i, j, k] = slot.GetComponent<Slot>();
                }

                slotPosition.y -= slotScale.y;
                slotPosition.z = 0;
            }

            slotPosition.x -= slotScale.x;
            slotPosition.y = 0;
        }
    }

    private void AddRandomCubes()
    {
        for (int i = 4; i < 11; i++)
        {
            int x = Random.Range(1, 3), y = Random.Range(1, 3), z = Random.Range(1, 3);

            if (puzzleMatrix[x, y, z].Cube == null)
            {
                GameObject indexCube = Instantiate(cubePrefab, puzzleMatrix[0, 1, 0].transform);
                CubeBehavior indexCubeBehavior = indexCube.GetComponent<CubeBehavior>();
                indexCubeBehavior.Construct(i, true);
            }
        }
    } 
    #endregion

    public Vector3 VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        Vector3Int selectedCubePos = Look4CubePositionInArray(cube);
        Vector3Int newPosition = selectedCubePos + movementVector;

        if (puzzleMatrix[newPosition.x, newPosition.y, newPosition.y].Cube == null)
        {
            puzzleMatrix[newPosition.x, newPosition.y, newPosition.z].AddCube(cube);
            puzzleMatrix[selectedCubePos.x, selectedCubePos.y, selectedCubePos.z].RemoveCube();

            return puzzleMatrix[newPosition.x, newPosition.y, newPosition.y].transform.position;
        }

        return cube.transform.position;
    }

    /// <summary>
    /// Looks for the position of the passed CubeBehavior on the Matrix
    /// </summary>
    /// <param name="cube"></param>
    /// <returns></returns>
    private Vector3Int Look4CubePositionInArray(CubeBehavior cube)
    {
        Vector3Int foundedSlot = Vector3Int.zero;

        bool founded = false;

        for (int i = 0; i < puzzleMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleMatrix.GetLength(1); j++)
            {
                for (int k = 0; k < puzzleMatrix.GetLength(2); k++)
                {
                    if (cube == puzzleMatrix[i, j, k].Cube)
                    {
                        foundedSlot = new Vector3Int(i, j, k);
                        founded = true;
                        break;
                    }
                }

                if (founded)
                {
                    break;
                }
            }

            if (founded)
            {
                break;
            }
        }

        return foundedSlot;
    }

    [Provide]
    public PuzzleEngine ProvidePuzzleEngine()
    {
        return this;
    }
}

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
        puzzleMatrix = new Slot[3, 3, 3];
        CreatePuzzle();
    }

    #region CreatePuzzle
    public void CreatePuzzle()
    {
        FillSlotMatrix();

        //MalePosition
        GameObject newCube = Instantiate(cubePrefab, puzzleMatrix[1, 0, 0].transform);
        CubeBehavior indexCubeBehavior = newCube.GetComponent<CubeBehavior>();
        indexCubeBehavior.Construct(1, false);

        puzzleMatrix[1, 0, 0].AddCube(indexCubeBehavior);

        //FemalePosition
        GameObject newCube1 = Instantiate(cubePrefab, puzzleMatrix[1, 0, 2].transform);
        indexCubeBehavior = newCube1.GetComponent<CubeBehavior>();
        indexCubeBehavior.Construct(2, false);

        puzzleMatrix[1, 0, 2].AddCube(indexCubeBehavior);

        //Final Position
        GameObject newCube2 = Instantiate(cubePrefab, puzzleMatrix[1, 2, 1].transform);
        indexCubeBehavior = newCube2.GetComponent<CubeBehavior>();
        indexCubeBehavior.Construct(3, false);

        puzzleMatrix[1, 2, 1].AddCube(indexCubeBehavior);

        AddRandomCubes();
    }

    private void FillSlotMatrix()
    {
        float slotDistance = 1.0f;

        for (int i = 0; i < puzzleMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleMatrix.GetLength(1); j++)
            {
                for (int k = 0; k < puzzleMatrix.GetLength(2); k++)
                {
                    // Calculate distance between Slots
                    Vector3 slotPosition = new Vector3(i * slotDistance, j * slotDistance, k * slotDistance);

                    GameObject slot = new GameObject("Slot");
                    slot.transform.SetParent(matrixOrigin.transform);
                    slot.transform.localPosition = slotPosition;
                    slot.AddComponent<Slot>().Construct(new Vector3Int(i, j, k));

                    puzzleMatrix[i, j, k] = slot.GetComponent<Slot>();
                }
            }
        }
    }
    private void AddRandomCubes()
    {
        for (int i = 4; i < 12; i++)
        {
            int x = Random.Range(1, 3), y = Random.Range(1, 3), z = Random.Range(1, 3);

            if (puzzleMatrix[x, y, z].Cube == null)
            {
                GameObject indexCube = Instantiate(cubePrefab, puzzleMatrix[x, y, z].transform);
                CubeBehavior indexCubeBehavior = indexCube.GetComponent<CubeBehavior>();

                indexCubeBehavior.Construct(i, true , VerifyMovement);
                puzzleMatrix[x, y, z].AddCube(indexCubeBehavior);
                continue;
            }
            else
            {
                Vector3Int indexSlot = FindEmptySlot();
                int j = indexSlot.x;
                int k = indexSlot.y;
                int w = indexSlot.z;


                GameObject indexCube = Instantiate(cubePrefab, puzzleMatrix[j, k, w].transform);
                CubeBehavior indexCubeBehavior = indexCube.GetComponent<CubeBehavior>();

                indexCubeBehavior.Construct(i, true , VerifyMovement);
                puzzleMatrix[j, k, w].AddCube(indexCubeBehavior);
                continue;
            }
        }
    }
    private Vector3Int FindEmptySlot()
    {
        Vector3Int foundedSlot = new Vector3Int(0, 0, 0);
        bool founded = false;

        for (int i = 0; i < puzzleMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleMatrix.GetLength(1); j++)
            {
                for (int k = 0; k < puzzleMatrix.GetLength(2); k++)
                {
                    if (puzzleMatrix[i, j, k].IsEmpty)
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
    #endregion

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

    /// <summary>
    /// Return a map of the slots in puzzle matrix with IsEmpty propertie equal false
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Vector3Int> ExportCubesMap()
    {
        Dictionary<int, Vector3Int> mapDictionary = new Dictionary<int, Vector3Int>();

        for (int i = 0; i < puzzleMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < puzzleMatrix.GetLength(1); j++)
            {
                for (int k = 0; k < puzzleMatrix.GetLength(2); k++)
                {
                    if (!puzzleMatrix[i, j, k].IsEmpty)
                    {
                        Vector3Int position = new Vector3Int(i, j, k);
                        int cubeID = puzzleMatrix[i, j, k].Cube.ID;
                        mapDictionary.Add(cubeID , position);
                    }
                }
            }
        }

        return mapDictionary;
    }
    public void VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        Vector3Int selectedCubePos = Look4CubePositionInArray(cube);
        Vector3Int newPosition = selectedCubePos + movementVector;
        bool validationA = puzzleMatrix[newPosition.x, newPosition.y, newPosition.z].IsEmpty;
        if (validationA)
        {
            puzzleMatrix[newPosition.x, newPosition.y, newPosition.z].AddCube(cube);
            puzzleMatrix[selectedCubePos.x, selectedCubePos.y, selectedCubePos.z].RemoveCube();

            cube.transform.parent = puzzleMatrix[newPosition.x, newPosition.y, newPosition.z].transform;
            cube.transform.localPosition = Vector3.zero;

            Debug.LogWarning("Cube Moved Successfully");
            return;
        }

        Debug.LogWarning("Cube Failed 2 Move");
        return;
    }

    [Provide]
    public PuzzleEngine ProvidePuzzleEngine()
    {
        return this;
    }
}

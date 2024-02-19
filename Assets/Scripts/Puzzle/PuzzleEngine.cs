using UnityEngine;
using UnityEngine.Events;

public class PuzzleEngine : MonoBehaviour , IMovementVerifier
{
    Slot[,,] puzzleMatrix;

    [SerializeField] UnityEvent OnPuzzleReady;

    public void CreatePuzzle()
    {

    }

    public Vector3 VerifyMovement(CubeBehavior cube, Vector3Int movementVector)
    {
        Vector3Int selectedCubePos = Look4CubePositionInArray(cube);
        Vector3Int newPosition = selectedCubePos + movementVector;

        if (puzzleMatrix[newPosition.x , newPosition.y , newPosition.y].Cube == null)
        {
            puzzleMatrix[newPosition.x , newPosition.y , newPosition.z].AddCube(cube);
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
                    if (cube == puzzleMatrix[i,j,k].Cube)
                    {
                        foundedSlot = new Vector3Int(i,j,k);
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
}

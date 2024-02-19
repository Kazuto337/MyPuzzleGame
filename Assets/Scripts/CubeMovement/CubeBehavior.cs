using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    int _ID;

    GameObject controls;
    bool isSelected = false;
    [SerializeField] bool isMovable;

    public void OnCubeSelected()
    {
        if (isMovable == false)
        {
            return;
        }

        isSelected = true;
        controls.SetActive(true);
    }
    public void OnCubeDeselected()
    {
        isSelected = false;
        controls.SetActive(false);
    }
    public void Try2Move(Vector3Int movementVector)
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.VerifyMovement(this, movementVector) == transform.position)
        {
            return;
        }

        Move(gameManager.VerifyMovement(this, movementVector));
    }

    public void Move(Vector3 newPosition)
    {
        StartCoroutine(MovementBehavior(newPosition));
    }

    IEnumerator MovementBehavior(Vector3 newPosition)
    {
        float t = 0;

        while (newPosition.magnitude - transform.position.magnitude <= 0.2f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPosition, t);

            float distance = newPosition.magnitude - transform.position.magnitude;

            if (distance == 0 || distance < 0.2f || transform.position == newPosition)
            {
                transform.position = newPosition;
                break;
            }

            yield return null;
        }
    }
}

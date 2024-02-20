using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] GameObject target;
    bool isDragging;
    Vector2 startingMousePosition;

    public void OnDrag(InputAction.CallbackContext action)
    {
        if (action.performed || action.started)
        {
            startingMousePosition = Mouse.current.position.ReadValue();
            isDragging = true;
        }
        else if (action.canceled)
        {
            isDragging = false;
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        Vector2 delta = currentMousePosition - startingMousePosition;

        float rotationSpeed = 0.1f;
        Vector3 rotationAngles = new Vector3(delta.y, -delta.x, 0) * rotationSpeed;

        target.transform.Rotate(rotationAngles, Space.World);

        startingMousePosition = currentMousePosition;
    }
}

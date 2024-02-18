using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Camera _camera;
    Vector3 startingPosition;
    [SerializeField] GameObject target;
    bool isDragging;

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Move();
        }
    }

    public void OnDrag(InputAction.CallbackContext action)
    {
        startingPosition = _camera.ScreenToViewportPoint(Input.mousePosition);

        isDragging = action.performed || action.started;
        Debug.Log(isDragging);
    }

    public void Move()
    {
        
        Vector3 angle = startingPosition - _camera.ScreenToViewportPoint(Input.mousePosition);

        _camera.transform.RotateAround(target.transform.position , Vector3.right , angle.y * 180);
        _camera.transform.RotateAround(target.transform.position , Vector3.down , angle.x * 180);

        startingPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
    }

}

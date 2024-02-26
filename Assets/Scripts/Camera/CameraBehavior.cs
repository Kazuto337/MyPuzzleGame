using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] GameObject target;
    bool isDragging;
    Vector2 startingMousePosition;

    [SerializeField] private float _minDistance = 1f;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _zoomSpeed = 0.01f;
    private Vector3 _originalCameraPosition;

    private void Start()
    {
        _originalCameraPosition = _camera.transform.localPosition;
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            RotateObject();
        }
    }
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
    public void OnScroll(InputAction.CallbackContext action)
    {
        float scrollDelta = action.ReadValue<Vector2>().y;
        float zoomAmount = scrollDelta * _zoomSpeed;

        float newDistance = Mathf.Clamp(_camera.transform.localPosition.z + zoomAmount, -_maxDistance, -_minDistance);

        _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, newDistance);
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

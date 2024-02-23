using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RaycastButtonInteraction : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rayLength;
    CubeBehavior cube;
    public void OnTap(InputAction.CallbackContext action)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.blue);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayLength))
        {
            Button button = hit.collider.GetComponent<Button>();
            if (button != null)
            {
                Debug.LogWarning("Button Founded = " + button.name);
                button.onClick.Invoke();
                return;
            }

            CubeBehavior cube1 = hit.collider.GetComponent<CubeBehavior>();
            if (cube != null && cube != cube1)
            {
                cube.OnCubeDeselected();
                cube = cube1;
                return;
            }

            cube = cube1;
            cube.OnCubeSelected();
        }
    }
}

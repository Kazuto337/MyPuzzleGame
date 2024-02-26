using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] Arrow_SO arrowData;
    [SerializeField] CubeBehaviour cubeBehavior;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnArrowPressed);
    }
    private void OnArrowPressed()
    {
        cubeBehavior.InvokeMovement(arrowData.MovementFactor);
    }
}

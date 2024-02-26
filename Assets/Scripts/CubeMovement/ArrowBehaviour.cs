using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] Arrow_SO arrowData;
    [SerializeField] CubeBehaviour cubeBehavior;

    float t = 0;
    bool isPerforming = false;


    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnArrowPressed);
    }
    private void Update()
    {
        if (isPerforming)
        {
            t += Time.deltaTime;
            if (t >= 1)
            {
                isPerforming = false;
            }
        }
    }
    public void OnArrowPressed()
    {
        if (isPerforming)
        {
            return;
        }

        cubeBehavior.InvokeMovement(arrowData.MovementFactor);
        isPerforming = true;
    }
}

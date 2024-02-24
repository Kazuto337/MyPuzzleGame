using DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    [Inject]
    [SerializeField] How2PlayData data;

    [SerializeField] InstructionsBehavior view;

    public void ShowInstructions()
    {
        view.LoadData(data);
        view.gameObject.SetActive(true);
    }
    public void HideInstructions()
    {
        view.gameObject.SetActive(false);
    }
}

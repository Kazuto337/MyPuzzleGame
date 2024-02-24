using DependencyInjection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    [Inject]
    [SerializeField] How2PlayData data;

    [SerializeField] InstructionsBehavior view;

    private void Start()
    {
        view.LoadData(data);
    }

    public void ShowInstructions()
    {
        view.gameObject.SetActive(true);
    }
    public void HideInstructions()
    {
        view.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arrow" , menuName = "ScriptableObject/ArrowValue" , order = 0)]
public class Arrow_SO : ScriptableObject
{
    [SerializeField] Vector3Int movementFactor;

    public Vector3Int MovementFactor { get => movementFactor;}
}

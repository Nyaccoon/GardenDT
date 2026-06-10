using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class BuildingData : ScriptableObject
{
    [field: SerializeField] public BuildingModel model { get; private set; }
    [field: SerializeField] public Enums.FloorType floorType { get; private set; }
}
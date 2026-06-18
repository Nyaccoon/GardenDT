using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FloorBuilding : MonoBehaviour
{
    private BuildingModel model;
    private FloorData data;
    public void Setup(FloorData data, float rotation)
    {
        this.data = data;
        model = Instantiate(data.floorModel, transform.position, Quaternion.identity, transform);
        model.Rotate(rotation);
    }

    public FloorData GetData()
    {
        return data;
    }
} 
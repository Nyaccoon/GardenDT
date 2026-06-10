using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
    [SerializeField] private Transform wrapper;
    public float rotation => wrapper.transform.eulerAngles.y;
    private BuildingShapeUnit[] shapeUnits;
    
    private void Awake()
    {
        shapeUnits = GetComponentsInChildren<BuildingShapeUnit>();
    }

    public void Rotate(float rotationStep)
    {
        wrapper.Rotate(new(0, rotationStep, 0));
    }

    public List<Vector3> GetAllBuildingPositions()
    {
        return shapeUnits.Select(unit => unit.transform.position).ToList();
    }
}
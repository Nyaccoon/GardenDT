using System.Collections.Generic;
using UnityEngine;

public class PillarMath : MonoBehaviour
{
    private BuildingSystem bSystem;
    private List<Building> allBuildings;
    private int nrOfPaved;
    private int nrOfLeakThrough;
    private int nrOfUnpaved;
    private int nrOfGrass;
    private int nrOfFlower;
    private int nrOfBush;
    private int nrOfTree;
    private int totalArea;

    public void Start()
    {
        allBuildings = bSystem.GetAllBuildings();
    }

    private int GetTotalArea()
    {
        totalArea = Support.GridHeight * Support.GridWidth;

        return totalArea;
    }

    private 

    public void CalculatePillar1()
    {
        StaticFormulas.P1Water();
    }
}
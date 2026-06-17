using System.Collections.Generic;
using UnityEngine;

public class PillarMath : MonoBehaviour
{
    private BuildingSystem bSystem;
    private List<Building> allBuildings = new();
    private List<FloorBuilding> allFloors = new();

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
        allFloors = bSystem.GetAllFloors();
        GetAllNumbers();
    }

    private int GetTotalArea()
    {
        totalArea = Support.GridHeight * Support.GridWidth;

        return totalArea;
    }

    public void CalculatePillar1()
    {
        StaticFormulas.P1Water(GetTotalArea(), nrOfPaved, nrOfLeakThrough, nrOfUnpaved, nrOfFlower, nrOfGrass, nrOfBush, nrOfTree);
    }

    private void GetAllNumbers()
    {
        foreach (Building building in allBuildings)
        {
            if (building.GetData().floorType == Support.FloorType.Paved)
            {
                nrOfPaved++;
            }
            else if (building.GetData().floorType == Support.FloorType.LeakThrough)
            {
                nrOfLeakThrough++;
            }
            else if (building.GetData().floorType == Support.FloorType.Unpaved)
            {
                nrOfUnpaved++;
            }
            else if (building.GetData().floorType == Support.FloorType.Grass)
            {
                nrOfGrass++;
            }
            else if (building.GetData().floorType == Support.FloorType.Flower)
            {
                nrOfFlower++;
            }
            else if (building.GetData().floorType == Support.FloorType.Bush)
            {
                nrOfBush++;
            }
            else if (building.GetData().floorType == Support.FloorType.Tree)
            {
                nrOfTree++;
            }
        }

        foreach (FloorBuilding floorBuilding in allFloors)
        {
            if (floorBuilding.GetData().floorType == Support.FloorType.Paved)
            {
                nrOfPaved++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.LeakThrough)
            {
                nrOfLeakThrough++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.Unpaved)
            {
                nrOfUnpaved++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.Grass)
            {
                nrOfGrass++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.Flower)
            {
                nrOfFlower++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.Bush)
            {
                nrOfBush++;
            }
            else if (floorBuilding.GetData().floorType == Support.FloorType.Tree)
            {
                nrOfTree++;
            }
        }

        Debug.Log("All numbers (paved, leak through, unpaved, grass, flower, bush, tree " + nrOfPaved + ", " + nrOfLeakThrough + ", " + nrOfUnpaved + ", " + nrOfGrass + ", " + nrOfFlower + ", " + nrOfBush + ", " + nrOfTree);
    }
}
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

    private int allPlants;

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        allBuildings = bSystem.GetAllBuildings();
        allFloors = bSystem.GetAllFloors();
    }

    public void CalculatePillar1()
    {
        GetAllNumbers();
        StaticFormulas.P1Water(GetTotalArea(), nrOfPaved, nrOfLeakThrough, nrOfUnpaved, nrOfFlower, nrOfGrass, nrOfBush, nrOfTree);
    }

    private int GetTotalArea()
    {
        totalArea = Support.GridHeight * Support.GridWidth;

        return totalArea;
    }

    private void GetAllPlants()
    {
        List<FloorBuilding> allFloorsCopy = GetCopyAllFloors();

        foreach (Building building in allBuildings)
        {
            foreach (FloorBuilding floor in allFloors)
            {
                if ((building.transform.position - new Vector3(0, 0.5f, 0)) == floor.transform.position)
                {
                    allFloorsCopy.Remove(floor);
                }
            }
        }
    }

    private List<FloorBuilding> GetCopyAllFloors()
    {
        List<FloorBuilding> builings = new();
        foreach (FloorBuilding b in allFloors)
        {
            builings.Add(b);
        }

        return builings;
    }

    private void GetAllNumbers()
    {
        Setup();
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
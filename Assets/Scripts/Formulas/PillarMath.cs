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

    public fertilizerCleanupType fertilizer;
    public fertilizerCleanupType cleanupStyle;

    public int nrOfInsects;
    public int nrOfBirds;
    public int nrOfSpiders;
    public int nrOfOtherAnimals;
    private int totalPlants;

    public int nrOfPlantTypes;

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        allBuildings = bSystem.GetAllBuildings();
        allFloors = bSystem.GetAllFloors();

        List<FloorBuilding> allFloorsCopy = FilterGrassWithoutPlants();
        allFloors = allFloorsCopy;

        GetAllNumbers(allBuildings, allFloors);
    }

    public void CalculatePillar1()
    {
        StaticFormulas.P1Water(GetTotalArea(), nrOfPaved, nrOfLeakThrough, nrOfUnpaved, nrOfFlower, nrOfGrass, nrOfBush, nrOfTree);
    }

    public void CalculatePillar2()
    {
        StaticFormulas.P2Soil(fertilizer, cleanupStyle);
    }

    public void CalculatePillar3()
    {
        StaticFormulas.P3Environment(nrOfInsects, nrOfBirds, nrOfSpiders, nrOfOtherAnimals, totalPlants, totalArea);
    }

    public void CalculatePillar4()
    {
        StaticFormulas.P4PlantDiversity(nrOfFlower, nrOfGrass, nrOfBush, nrOfTree, totalArea, nrOfPlantTypes);
    }

    private int GetTotalArea()
    {
        totalArea = Support.GridHeight * Support.GridWidth;

        return totalArea;
    }

    private List<FloorBuilding> FilterGrassWithoutPlants()
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

        return allFloorsCopy;
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

    private void GetAllNumbers(List<Building> allBuildings, List<FloorBuilding> allFloorsCopy)
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
                totalPlants++;
            }
            else if (building.GetData().floorType == Support.FloorType.Flower)
            {
                nrOfFlower++;
                totalPlants++;
            }
            else if (building.GetData().floorType == Support.FloorType.Bush)
            {
                nrOfBush++;
                totalPlants++;
            }
            else if (building.GetData().floorType == Support.FloorType.Tree)
            {
                nrOfTree++;
                totalPlants++;
            }
        }

        foreach (FloorBuilding floorBuilding in allFloorsCopy)
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
                totalPlants++;
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
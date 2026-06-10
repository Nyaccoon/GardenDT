using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    private BuildingGridCell[,] grid;
    
    private void Start()
    {
        grid = new BuildingGridCell[width, height];
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = new(); 
            }
        }
    }

    public void SetBuilding(Building building, List<Vector3> allBuildingPositions)
    {
        foreach (var p in allBuildingPositions)
        {
            (int x, int y) = WorldToGridPosition(p);
            grid[x, y].SetBuilding(building);
        }
    }

    public bool CanBuild(List<Vector3> allBuildingPositions)
    {
        foreach (var p in allBuildingPositions)
        {
            (int x, int y) = WorldToGridPosition(p);
            if (x < 0 || x >= width || y < 0 || y >= height) return false;
            if (!grid[x, y].IsEmpty()) return false;

        }
        return true;
    }

    private (int x, int y) WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - transform.position).x / BuildingSystem.cellSize);
        int y = Mathf.FloorToInt((worldPosition - transform.position).z / BuildingSystem.cellSize);
        return (x, y);
    }
}

public class BuildingGridCell
{
    private Building building;
    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public bool IsEmpty()
    {
        return building == null;
    }
}
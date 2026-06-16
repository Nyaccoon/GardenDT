using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public const float cellSize = 1f;

    [SerializeField] private BuildingData treeData;
    [SerializeField] private BuildingData bushData;
    [SerializeField] private BuildingData flowerData;
    [SerializeField] private BuildingData vegGarData;
    [SerializeField] private BuildingData trampData;
    [SerializeField] private BuildingPreview previewPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private BuildingGrid grid;
    private BuildingPreview preview;

    private List<Building> allBuildings;

    private void Update()
    {
        Vector3 mousePos = GetWorldMousePosition();

        if(preview != null)
        {
            HandlePreview(mousePos);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                preview = CreatePreview(treeData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                preview = CreatePreview(bushData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                preview = CreatePreview(flowerData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                preview = CreatePreview(vegGarData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                preview = CreatePreview(trampData, mousePos);
            }
        }
    }

    private void HandlePreview(Vector3 mouseWorldPos)
    {
        preview.transform.position = mouseWorldPos;
        List<Vector3> buildPositions = preview.model.GetAllBuildingPositions();
        bool canBuild = grid.CanBuild(buildPositions);
        if (canBuild)
        {
            preview.transform.position = GetSnappedCentrePosition(buildPositions);
            preview.ChangeState(Support.PreviewState.Positive);
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(buildPositions);
            }
        }
        else
        {
            preview.ChangeState(Support.PreviewState.Negative);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            preview.Rotate(90);
        }
    }

    private void PlaceBuilding(List<Vector3> buildingPositions)
    {
        Building building = Instantiate(buildingPrefab, preview.transform.position, Quaternion.identity);
        building.Setup(preview._data, preview.model.rotation);
        grid.SetBuilding(building, buildingPositions);
        Destroy(preview.gameObject);
        preview = null;

        allBuildings.Add(building);
    }

    private Vector3 GetSnappedCentrePosition(List<Vector3> allBuildingPositions)
    {
        List<int> xs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.x)).ToList();
        List<int> zs = allBuildingPositions.Select(p => Mathf.FloorToInt(p.z)).ToList();
        float centreX = (xs.Min() + xs.Max()) / 2f + cellSize / 2f;
        float centreZ = (zs.Min() + zs.Max()) / 2f + cellSize / 2f;
        return new(centreX, 0, centreZ);
    }

    private Vector3 GetWorldMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new(Vector3.up, Vector3.zero);
        if(groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    private BuildingPreview CreatePreview(BuildingData data, Vector3 position)
    {
        BuildingPreview buildingPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        buildingPreview.Setup(data);
        return buildingPreview;
    }

    public List<Building> GetAllBuildings()
    {
        return allBuildings;
    }
}
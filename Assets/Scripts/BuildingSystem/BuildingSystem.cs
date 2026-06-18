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

    [SerializeField] private FloorData dirtData;
    [SerializeField] private FloorData grassData;
    [SerializeField] private FloorData gravelData;
    //[SerializeField] private FloorData leakThroughTileData;
    //[SerializeField] private FloorData sandData;
    //[SerializeField] private FloorData tileData;
    //[SerializeField] private FloorData waterData;

    [SerializeField] private BuildingPreview previewPrefab;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private FloorBuilding floorBuildingPrefab;
    [SerializeField] private BuildingGrid grid;
    [SerializeField] private FloorBuilding standardFloor;

    private BuildingPreview buildingPreview;
    private BuildingPreview floorPreview;

    private List<Building> allBuildings = new();
    private List<FloorBuilding> allFloors = new();

    private void Start()
    {
        InstantiateFloor();
    }

    private void Update()
    {
        Vector3 mousePos = GetWorldMousePosition();

        if(buildingPreview != null)
        {
            HandleBuildingPreview(mousePos);
        }
        else if(floorPreview != null)
        {
            HandleFloorPreview(mousePos);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                buildingPreview = CreateBuildingPreview(treeData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                buildingPreview = CreateBuildingPreview(bushData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                buildingPreview = CreateBuildingPreview(flowerData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                buildingPreview = CreateBuildingPreview(vegGarData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                buildingPreview = CreateBuildingPreview(trampData, mousePos);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                floorPreview = CreateFloorPreview(dirtData, mousePos);
            }
        }
    }

    private void HandleBuildingPreview(Vector3 mouseWorldPos)
    {
        buildingPreview.transform.position = mouseWorldPos;
        List<Vector3> buildPositions = buildingPreview.buildingModel.GetAllBuildingPositions();
        bool canBuild = grid.CanBuildBuilding(buildPositions);
        if (canBuild)
        {
            buildingPreview.transform.position = GetSnappedCentrePosition(buildPositions);
            buildingPreview.ChangeState(Support.PreviewState.Positive);
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(buildPositions);
            }
        }
        else
        {
            buildingPreview.ChangeState(Support.PreviewState.Negative);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            buildingPreview.Rotate(90);
        }
    }

    private void HandleFloorPreview(Vector3 mouseWorldPos)
    {
        floorPreview.transform.position = mouseWorldPos;
        List<Vector3> buildPositions = floorPreview.buildingModel.GetAllBuildingPositions();
        bool canBuild = grid.CanBuildFloor(buildPositions);
        if (canBuild)
        {
            floorPreview.transform.position = GetSnappedCentrePosition(buildPositions);
            floorPreview.ChangeState(Support.PreviewState.Positive);
            if (Input.GetMouseButtonDown(0))
            {
                PlaceFloor(buildPositions);
            }
        }
        else
        {
            floorPreview.ChangeState(Support.PreviewState.Negative);
        }
    }

    private void PlaceBuilding(List<Vector3> buildingPositions)
    {
        Building building = Instantiate(buildingPrefab, buildingPreview.transform.position, Quaternion.identity);
        building.Setup(buildingPreview.buildingData, buildingPreview.buildingModel.rotation);
        building.transform.position -= new Vector3(0, 0.5f, 0);
        grid.SetBuilding(building, buildingPositions);
        Destroy(buildingPreview.gameObject);
        buildingPreview = null;

        allBuildings.Add(building);
    }

    private void PlaceFloor(List<Vector3> buildingPositions)
    {
        DestroyObject();

        FloorBuilding floorBuilding = Instantiate(floorBuildingPrefab, floorPreview.transform.position, Quaternion.identity);
        floorBuilding.Setup(floorPreview.floorData, floorPreview.buildingModel.rotation);
        floorBuilding.transform.position -= new Vector3(0, 0.5f, 0);
        grid.SetFloor(floorBuilding, buildingPositions);
        Destroy(floorPreview.gameObject);
        floorPreview = null;

        allFloors.Add(floorBuilding);
    }

    private GameObject GetHitObject()
    {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            var hitTransform = hit.transform;
            int count = 0;
            while (hitTransform != null && hitTransform.gameObject.GetComponent<FloorBuilding>() == null && count < 10)
            {
                Debug.Log(hitTransform);
                hitTransform = hitTransform.parent;
                count++;
            }
            hitObject = hitTransform?.gameObject;
        }
        Debug.Log("Hit Object: " +  hitObject.name);
        return hitObject;
    }

    private void DestroyObject()
    {
        GameObject objectToDestroy = GetHitObject();
        Debug.Log(objectToDestroy);
        if(objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
        else
        {
            Debug.Log("No object to destroy");
        }
    }


    private void InstantiateFloor()
    {
        for (int x = 0; x < Support.GridWidth; x++)
        {
            for (int y = 0; y < Support.GridHeight; y++)
            {
                FloorBuilding newObject = Instantiate(standardFloor, new Vector3(x * BuildingSystem.cellSize + 0.5f, -0.5f, y * BuildingSystem.cellSize + 0.5f), Quaternion.identity);
                newObject.transform.SetParent(transform);
                newObject.Setup(grassData, 0f);

                allFloors.Add(newObject);
            }
        }

        Debug.Log(allFloors);
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

    private BuildingPreview CreateBuildingPreview(BuildingData data, Vector3 position)
    {
        BuildingPreview buildingPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        buildingPreview.Setup(data);
        return buildingPreview;
    }
    private BuildingPreview CreateFloorPreview(FloorData data, Vector3 position)
    {
        BuildingPreview floorPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        floorPreview.Setup(data);
        return floorPreview;
    }

    public List<Building> GetAllBuildings()
    {
        return allBuildings;
    }

    public void SpawnTree()
    {
        Vector3 mousePos = GetWorldMousePosition();
        buildingPreview = CreateBuildingPreview(treeData, mousePos);
    }

    public void SpawnBush()
    {
        Vector3 mousePos = GetWorldMousePosition();
        buildingPreview = CreateBuildingPreview(bushData, mousePos);
    }

    public void SpawnVegGar()
    {
        Vector3 mousePos = GetWorldMousePosition();
        buildingPreview = CreateBuildingPreview(vegGarData, mousePos);
    }

    public void SpawnFlower()
    {
        Vector3 mousePos = GetWorldMousePosition();
        buildingPreview = CreateBuildingPreview(flowerData, mousePos);
    }

    public void SpawnTramp()
    {
        Vector3 mousePos = GetWorldMousePosition();
        buildingPreview = CreateBuildingPreview(trampData, mousePos);
    }

    public void SpawnFloor()
    {
        Vector3 mousePos = GetWorldMousePosition();
        floorPreview = CreateFloorPreview(dirtData, mousePos);
    }




    public List<FloorBuilding> GetAllFloors()
    {
        return allFloors;
    }
}
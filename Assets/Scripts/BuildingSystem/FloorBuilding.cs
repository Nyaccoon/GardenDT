using UnityEngine;

public class FloorBuilding : MonoBehaviour
{
    private int width = Support.GridWidth;
    private int height = Support.GridHeight;
    [SerializeField] private Transform _cube;

    public void Start()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var newObject = Instantiate(_cube, new Vector3(x * BuildingSystem.cellSize + 0.5f, -0.5f, y * BuildingSystem.cellSize + 0.5f), Quaternion.identity);
                newObject.SetParent(transform);
            }
        }
    }
}

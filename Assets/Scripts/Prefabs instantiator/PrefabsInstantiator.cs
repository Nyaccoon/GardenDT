using UnityEngine;
using UnityEngine.InputSystem;

public class PrefabsInstantiator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public GameObject Tree;

    public void SpawnTree()
    {
        if (Tree == null)
        {
            Debug.LogWarning("No Tree assigned!");
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("No Main Camera found!");
            return;
        }

        // Get mouse position on the screen
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;
        Instantiate(Tree, worldPosition, Quaternion.identity);
    }
}

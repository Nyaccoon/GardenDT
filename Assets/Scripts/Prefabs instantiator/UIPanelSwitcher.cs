using UnityEngine;

public class UIPanelSwitcher : MonoBehaviour
{
    public GameObject panelFloor;
    public GameObject panelPlants;

    void Start()
    {
        ShowPanelFloor(); 
    }

    public void ShowPanelFloor()
    {
        panelFloor.SetActive(true);
        panelPlants.SetActive(false);
    }

    public void ShowPanelPlants()
    {
        panelFloor.SetActive(false);
        panelPlants.SetActive(true);
    }
}
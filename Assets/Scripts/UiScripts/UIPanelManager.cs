using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject panelFloor;
    public GameObject panelPlants;
    public GameObject settingsPanel;

    void Start()
    {
        // Disable all panels at the beginning of the scene
        panelFloor.SetActive(false);
        panelPlants.SetActive(false);
        settingsPanel.SetActive(false);
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

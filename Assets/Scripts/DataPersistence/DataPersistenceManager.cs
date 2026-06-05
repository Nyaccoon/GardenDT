using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager instance {  get; private set; }

    private GameData gameData;

    private void Initialise()
    {
        if(instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene.");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void SaveGame()
    {
        //TODO: pass data to other scripts so they can update it

        //TODO: save data to file using the data handler
    }

    public void LoadGame()
    {
        //TODO: Load any saved data from a file using the data handler

        //if no data can be loaded, initialise to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initialising data to defaults.");
            NewGame();
        }

        //TODO: push the loaded data to all other scripts that need it
    }
}

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static DataPersistenceManager instance {  get; private set; }

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDPO();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDPO()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
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
        //pass data to other scripts so they can update it

        foreach (IDataPersistence dPO in dataPersistenceObjects)
        {
            dPO.SaveData(ref gameData);
        }

        //save data to file using the data handler
        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, initialise to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initialising data to defaults.");
            NewGame();
        }

        //push the loaded data to all other scripts that need it

        foreach(IDataPersistence dPO in dataPersistenceObjects)
        {
            dPO.LoadData(gameData);
        }
    }
}
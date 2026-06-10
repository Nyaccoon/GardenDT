using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;


    public class DataPersistenceManager : MonoBehaviour
    {
        [Header("File Storage Config")]
        [SerializeField] private string fileName;
        [SerializeField] private bool useEncryption;

        public static DataPersistenceManager instance { get; private set; }

        private GameData gameData;

        private List<IDataPersistence> dataPersistenceObjects;

        private FileDataHandler dataHandler;

        private void Initialise()
        {
            if (instance != null)
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
        
            this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
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

        public bool SaveGame()
        {
            DPMLogic.SaveGameLogic(dataPersistenceObjects, dataHandler, gameData);
            return true;
        }

        public bool LoadGame()
        {
            DPMLogic.LoadGameLogic(dataPersistenceObjects, dataHandler, gameData);
            return true;
        }
    }

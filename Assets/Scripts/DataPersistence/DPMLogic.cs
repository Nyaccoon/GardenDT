using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class DPMLogic
{
    public static bool SaveGameLogic(List<IDataPersistence> dataPersistenceObjects, FileDataHandler dataHandler, GameData gameData)
    {
        try
        {
            //pass data to other scripts so they can update it

            foreach (IDataPersistence dPO in dataPersistenceObjects)
            {
                dPO.SaveData(ref gameData);
            }

            //save data to file using the data handler
            dataHandler.Save(gameData);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to save the game: " + e.Message);
            return false;
        }

        return true;
    }

    public static bool LoadGameLogic(List<IDataPersistence> dataPersistenceObjects, FileDataHandler dataHandler, GameData gameData)
    {
        try
        {
            //Load any saved data from a file using the data handler
            gameData = dataHandler.Load();

            //if no data can be loaded, initialise to a new game
            if (gameData == null) 
            {
                Debug.Log("No data was found. Initialising data to defaults.");
                NewGame();
            }

            //push the loaded data to all other scripts that need it

            foreach (IDataPersistence dPO in dataPersistenceObjects)
            {
                dPO.LoadData(gameData);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Failed to load the game: " + e.Message);
            return false;
        }

        return true;
    }
}

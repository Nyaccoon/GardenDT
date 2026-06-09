using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class DPMLogic
{
    public bool SaveGameLogic(List<IDataPersistence> dataPersistenceObjects)
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
}

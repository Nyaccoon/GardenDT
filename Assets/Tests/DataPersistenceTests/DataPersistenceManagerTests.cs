using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Linq;
using System.Collections.Generic;



    public class DataPersistenceManagerTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ThrowException_OnSaveGame()
        {
            // Setup
            var go = new GameObject();

            go.AddComponent<DataPersistenceManager>();

            bool boolCaught = true;
            DataPersistenceManager manager = go.GetComponent<DataPersistenceManager>();
            Debug.Log(manager);
            // Action
            var ex = Assert.Throws<Exception>(
                ()=> { boolCaught = manager.SaveGame(); });
        Debug.Log(boolCaught);
            // Assert
            Assert.IsFalse(boolCaught);
        }

        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator DataPersistenceManagerTestsWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }

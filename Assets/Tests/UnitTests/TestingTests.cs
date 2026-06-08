using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestingTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestingTestsSimplePasses()
    {
        // Setup
        int a = 2;

        // Action
        a *= 2;

        // Assert
        Assert.AreEqual(a, 4);
    }

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator TestingTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}
}

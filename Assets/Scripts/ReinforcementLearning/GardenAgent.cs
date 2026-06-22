using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Collections.Generic;
using System;

public class GardenAgent : Agent
{

    private PillarMath mathComponent;
    private List<Vector3> allBuildings = new();
    private List<FloorBuilding> allFloors = new();

    public override void CollectObservations(VectorSensor sensor)
    {
        // Benodigde observaties:
        //  pillar1
        //  pillar2
        //  pillar3
        //  pillar4
        //  list<Building> allBuildings
        //  list<FloorBuilding> allFloors
        sensor.AddObservation(mathComponent.GetPillar1Score());
        sensor.AddObservation(mathComponent.GetPillar2Score());
        sensor.AddObservation(mathComponent.GetPillar3Score());
        sensor.AddObservation(mathComponent.GetPillar4Score());
        CheckLists();
        //sensor.AddObservation(allBuildings);

    }

    private void CheckLists()
    {
        if(mathComponent.GetAllBuildings() != null)
        {
            List<Building> tempList = mathComponent.GetAllBuildings();
            foreach(Building building in tempList)
            {
                allBuildings.Add(building.gameObject.transform.position);
            }
        }
        else
        {
            throw new NullReferenceException("Error: list allBuildings is void.");
        }

        if (mathComponent.GetAllFloors() != null)
        {
            allFloors = mathComponent.GetAllFloors();
        }
        else
        {
            throw new NullReferenceException("Error: list allFloors is void.");
        }
    }
}
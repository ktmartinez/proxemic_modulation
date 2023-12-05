using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EntityProximityModulation : MonoBehaviour
{
    public Transform[] entities; // Array to hold the three entities

    private Vector3[] initialPositions; // Array to store initial positions of the entities

    void Start()
    {
        // Initialize array to store initial positions of entities
        initialPositions = new Vector3[entities.Length];

        // Store initial positions of the entities
        for (int i = 0; i < entities.Length; i++)
        {
            initialPositions[i] = entities[i].position;
        }
    }

    void Update()
    {
        // Calculate the total movement of all entities in x and z axes
        Vector3 totalMovement = Vector3.zero;
        foreach (var entity in entities)
        {
            Vector3 currentPos = entity.position;
            Vector3 initialPos = initialPositions[System.Array.IndexOf(entities, entity)];

            // Calculate only x and z axis movement
            Vector3 movement = new Vector3(currentPos.x - initialPos.x, 0f, currentPos.z - initialPos.z);
            totalMovement += movement;
        }

        // Calculate the average movement
        Vector3 averageMovement = totalMovement / entities.Length;

        // Update positions of all entities based on the average movement in x and z axes
        foreach (var entity in entities)
        {
            Vector3 currentPos = entity.position;

            // Calculate new position only for x and z axes
            Vector3 newPos = new Vector3(currentPos.x - (averageMovement.x * 0.25f), currentPos.y, currentPos.z - (averageMovement.z * 0.25f));
            entity.position = newPos;
        }
    }
}
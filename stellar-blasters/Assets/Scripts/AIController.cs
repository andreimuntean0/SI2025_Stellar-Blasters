using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [SerializeField]
    float rayCastOffset = 2.5f;

    float movementSpeed = 5f;       // Base movement speed (overridden in Start)
    float rotationalDump = .5f;     // Smoothing factor for rotation
    float detectionDistance = 20f;  // How far ahead obstacles are detected
    
    [SerializeField]
    Transform target;               // Reference to the player's transform

    public AIController_GameSettings gameSettings; // Game mode and difficulty settings

    // Start is called before the first frame update
    void Start()
    {
        // get game settings
        gameSettings = FindObjectOfType<AIController_GameSettings>();
        if (gameSettings == null)
        {
            Debug.LogError("AIController_GameSettings script not found in the scene!");
        }
        // Sets movementSpeed based on game mode/difficulty.
        movementSpeed = gameSettings.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindTarget()) // Exit if no player found
            return;
        if (gameSettings.solo)
        {   
            // Flee in solo mode
            RunAwayMode();
        }
        else
        {
            // Chase & avoid obstacles in multiplayer
            Pathfinding();
        }
        // Always move forward
        Move();
        
    }

    void Turn()
    {
        // Smooth Player Tracking
        Vector3 pos = target.position - transform.position;                                                     // Direction to player
        Quaternion rotation = Quaternion.LookRotation(pos);                                                     // Target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDump * Time.deltaTime);   // Apply interpolated rotation to enemy's transform
    }

    void Move()
    {
        // Forward Movement
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }   

    void Pathfinding()
    {
        // Uses 4 raycasts (left, right, up, down) for obstacle detection. Adjusts rotation to avoid collisions. Falls back to Turn() if no obstacles are detected.

        RaycastHit hit;
        Vector3 raycastNewDir = Vector3.zero;

        // Define raycast origins (left, right, up, down)
        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        // Debug visualization (cyan colored rays)
        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        // Obstacle detection logic
        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir += Vector3.right; // Turn right if left ray hits
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir -= Vector3.right; // Turn left if right ray hits
        }

        if(Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir -= Vector3.up;     // Turn down if upper ray hits
        }
        else if(Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir += Vector3.down;  // Turn up if lower ray hits
        }

        // Apply rotation if obstacle detected, else turn toward player
        if (raycastNewDir != Vector3.zero)
        {
            transform.Rotate(raycastNewDir * 5f * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }

    void RunAwayMode() 
    {
        // Fleeing Behavior - Makes the enemy turn and run away from the player.
        Vector3 runDirection = transform.position - target.position;  // Direction away from player
        Quaternion rotation = Quaternion.LookRotation(runDirection);  // Target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDump * Time.deltaTime); // Apply interpolated rotation to enemy's transform
    }

    bool FindTarget()
    {
        if(target == null)
        {
            // If target is lost, searches for the player by tag.
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            if(temp != null)
                target = temp.transform;
        }

        if(target == null)
            return false;
        return true;
    } 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows enemies to locate the target (player), check attack conditions (facing player + line of sight) and to fire the lasers when conditions are fulfilled.
public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Laser laser;

    Vector3 hitPosition;

    // Stores the game mode and difficulty settings.
    public AIController_GameSettings gameSettings;


    // Start is called before the first frame update
    void Start()
    {
        gameSettings = FindObjectOfType<AIController_GameSettings>();
        if (gameSettings == null)
        {
            Debug.LogError("AIController_GameSettings script not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if there is a valid target
        if (!FindTarget())
            return;

        // Checks if this is NOT a solo game mode - ensures enemies only attack in Enemy Engage mode.
        // Checks if player is within a 130°-170° in front of the enemy.
        // Checks if the performed raycast has unobstructed view to player (it can hit the player directly)
        if ((!gameSettings.solo) && inFront() && haveLineOfSight())
        {
            // if all conditions are fulfilled -> fire the laser
            FireLaser();
        }
    }

    // Calculates angle between enemy's forward vector and player direction
    bool inFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if (Mathf.Abs(angle) > 130 && Mathf.Abs(angle) < 170)
        {
            // Player is within 130°-170°
            // Debug.DrawLine(transform.position, target.position, Color.green); // used to visualize the line of sight of the enemy during debug - when player is in front
            return true;
        }

        // Debug.DrawLine(transform.position, target.position, Color.yellow); // -||- - but when player is NOT in front
        return false;
    }

    // Shoots raycast from laser position toward player
    bool haveLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;

        if (Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance))
        {
            // Debug.DrawRay(laser.transform.position, direction, Color.red);
            if (hit.transform.CompareTag("Player"))
            {
                // Confirms hit object has "Player" tag
                Debug.DrawRay(laser.transform.position, direction, Color.red);
                hitPosition = hit.transform.position;
                // Stores hit position for accurate aiming
                return true;
            }
        }

        return false;
    }

    // Triggers the laser's attack with precise target position
    void FireLaser()
    {
        // Debug.Log("Enemy Firing!!");
        // Passes both hit location and target reference
        laser.FireLaser(hitPosition, target);
    }

    // Locate player GameObject by tag and store it inside target.
    bool FindTarget()
    {
        if (target == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            if (temp != null)
                target = temp.transform;
        }

        if (target == null)
            return false;
        return true;
    }
}

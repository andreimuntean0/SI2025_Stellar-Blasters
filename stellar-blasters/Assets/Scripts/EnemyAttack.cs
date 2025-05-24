using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Laser laser;

    Vector3 hitPosition;

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
        if(!FindTarget())
            return;
        inFront();
        haveLineOfSight();
        if ( (!gameSettings.solo) && inFront() && haveLineOfSight())
        {   
            FireLaser();
        } 
    }

    bool inFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        if(Mathf.Abs(angle) > 130 && Mathf.Abs(angle) < 170)
        {
            // Debug.DrawLine(transform.position, target.position, Color.green);
            return true;
        }
        
        // Debug.DrawLine(transform.position, target.position, Color.yellow);
        return false;
    }

    bool haveLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;

        if(Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance))
        {
            // Debug.DrawRay(laser.transform.position, direction, Color.red);
            if(hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(laser.transform.position, direction, Color.red);
                hitPosition = hit.transform.position;
                return true;
            }
        }

        return false;
    }

    void FireLaser()
    {
        // Debug.Log("Enemy Firing!!");
        laser.FireLaser(hitPosition, target);
    }

    bool FindTarget()
    {
        if(target == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            if(temp != null)
                target = temp.transform;
        }

        if(target == null)
            return false;
        return true;
    } 
}

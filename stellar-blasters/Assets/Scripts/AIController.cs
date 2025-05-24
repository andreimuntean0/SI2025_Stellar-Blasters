using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [SerializeField]
    float rayCastOffset = 2.5f;

    float movementSpeed = 5f;
    float rotationalDump = .5f;
    float detectionDistance = 20f;
    
    [SerializeField]
    Transform target;

    public AIController_GameSettings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = FindObjectOfType<AIController_GameSettings>();
        if (gameSettings == null)
        {
            Debug.LogError("AIController_GameSettings script not found in the scene!");
        }
        movementSpeed = gameSettings.movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!FindTarget())
            return;
        if (gameSettings.solo)
        {   
            RunAwayMode();
        }
        else
        {
            Pathfinding();
        }
        Move();
        
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDump * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }   

    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastNewDir = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        if(Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir += Vector3.right;    
        }
        else if(Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir -= Vector3.right;
        }

        if(Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir -= Vector3.up;    
        }
        else if(Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            raycastNewDir += Vector3.down;
        }

        if(raycastNewDir != Vector3.zero)
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
        Vector3 runDirection = transform.position - target.position;
        Quaternion rotation = Quaternion.LookRotation(runDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDump * Time.deltaTime);
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

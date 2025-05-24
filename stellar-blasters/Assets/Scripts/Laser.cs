using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    [SerializeField]
    float laserOffTime = .5f;
    [SerializeField]
    float maxdist = 300f;
    [SerializeField]
    float fireDelay = 2f;
    LineRenderer lr;
    Light laserLight;
    bool canFire;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lr.enabled = false;
        laserLight.enabled = false;
        canFire = true;
        transform.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // used for laser in debug mode
        // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxdist, Color.yellow);
    }

    Vector3 castRay()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxdist;

        if(Physics.Raycast(transform.position, transform.forward * maxdist, out hit))
        {
            // Debug.Log("We hit: " + hit.transform);

            SpawnExplosion(hit.point, hit.transform);

            return hit.point;
        }
        
        // Debug.Log("We missed!");
        return transform.position + (transform.forward * maxdist);

    }

    void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        Explosion temp = target.GetComponent<Explosion>();
        if(temp != null)
        {
            if(target.gameObject.tag == "enemy")
            {
                temp.EnemyHit(hitPosition, target.gameObject);
            }
            else
            {
                // temp.EnemyHit(hitPosition, target.gameObject);
                temp.IveBeenHit(hitPosition);
            }
        }
    }

    public void FireLaser()
    {
        FireLaser(castRay());
    }

    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if(canFire)
        {
            if(target != null)
                SpawnExplosion(targetPosition, target);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPosition);
            lr.enabled = true;
            laserLight.enabled = true;
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
        }
    }

    void TurnOffLaser()
    {
        lr.enabled = false;
        laserLight.enabled = false;
    }

    public float Distance
    {
        get { return maxdist; }
    }

    void CanFire()
    {
        canFire = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Handles firing the laser, hitting targets via raycasting, and managing visuals like a line renderer and light to simulate the laser beam.
public class Laser : MonoBehaviour
{
    [SerializeField]
    float laserOffTime = .5f;
    [SerializeField]
    float maxdist = 300f;
    [SerializeField]
    float fireDelay = 2f;  // cooldown
    LineRenderer lr; // draws the laser beam.
    Light laserLight; // a visual light effect for realism.
    bool canFire; // flag to control whether the laser is ready to fire.

    void Awake()
    {
        // Retrieves the LineRenderer and Light components from the same GameObject.
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hides the laser initially and sets it to a ready state. 
        // Also rotates the laser downward by 90 degrees — this might depend on the object’s orientation in the scene.
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

        // Fires a ray from the laser’s position forward.
        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxdist;

        if(Physics.Raycast(transform.position, transform.forward * maxdist, out hit))
        {
            // Debug.Log("We hit: " + hit.transform);

            // The ray hit something and calls SpawnExplosion() at the hit point.
            SpawnExplosion(hit.point, hit.transform);

            return hit.point;
        }
        // If it misses, it returns a point maxdist units ahead.
        // Debug.Log("We missed!");
        return transform.position + (transform.forward * maxdist);

    }

    void SpawnExplosion(Vector3 hitPosition, Transform target)
    {
        // Checks if the hit target has an Explosion script.
        Explosion temp = target.GetComponent<Explosion>();
        if(temp != null)
        {
            if(target.gameObject.tag == "enemy")
            {
                // If the target is an enemy, it calls EnemyHit().
                temp.EnemyHit(hitPosition, target.gameObject);
            }
            else
            {
                // temp.EnemyHit(hitPosition, target.gameObject);
                // otherwise instantiate an explosion at the hitPosition
                temp.IveBeenHit(hitPosition);
            }
        }
    }

    public void FireLaser()
    {
        // A public method to trigger a laser shot. Internally, it calculates the hit point using castRay() and fires the laser visually.
        FireLaser(castRay());
    }

    public void FireLaser(Vector3 targetPosition, Transform target = null)
    {
        if (canFire)
        {
            // Fires a laser to a specified position.
            if (target != null)
                SpawnExplosion(targetPosition, target); // Triggers an explosion if a target is provided.
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, targetPosition);
            lr.enabled = true;
            laserLight.enabled = true;
            // Activates visual effects (beam and light).
            // Starts cooldown using Invoke.
            canFire = false;
            Invoke("TurnOffLaser", laserOffTime);
            Invoke("CanFire", fireDelay);
        }
    }

    void TurnOffLaser()
    {
        // Disables laser visuals after a short delay.
        lr.enabled = false;
        laserLight.enabled = false;
    }

    public float Distance
    {
        // Public getter for the laser's maximum range.
        get { return maxdist; }
    }

    void CanFire()
    {
        // Re-enables firing after the cooldown ends.
        canFire = true;
    }

}

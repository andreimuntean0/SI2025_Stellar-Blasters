using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the movement, rotation, lasers and collisions for the player's ship.
public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcc = 2.5f, strafeAcc = 2f, hoverAcc = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcc = 3.5f;

    [SerializeField]
    Laser[] laser;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        // get the mouse cursor's current screen position in pixels.
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        // mouse to center calculation
        // Calculates how far the mouse is from screen center
        // (horizontal offset) / screenCenter.y -> to normalize to (-1, 1)
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        // Ensure the mouse distance vector never exceeds length 1 (create a circular deadzone)
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        // Roll input smoothing using Mathf.Lerp.
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcc * Time.deltaTime);

        //-----------------------------------------------------------------------------------------------------------------------------
        // Ship rotation
        // Pitch: Negative mouseDistance.y so mouse-up = nose-up
        // Yaw:   Positive mouseDistance.x so mouse-right = turn-right
        // Roll:  Uses smoothed keyboard input
        transform.Rotate(
            -mouseDistance.y * lookRateSpeed * Time.deltaTime,  // pitch (up/down)
            mouseDistance.x * lookRateSpeed * Time.deltaTime,   // yaw (left/right)
            rollInput * rollSpeed * Time.deltaTime,             // roll (barrel roll)
            Space.Self);

        //-----------------------------------------------------------------------------------------------------------------------------
        // Movement speed smoothing 
        // Smoothly interpolates current speed toward target speed:
        // Vertical (W/S): Forward/backward
        // Horizontal (A/D): Left/right strafe
        // Hover (likely R/F): Up/down
        // *Acc values control acceleration/deceleration rates
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcc * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcc * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcc * Time.deltaTime);

        //-----------------------------------------------------------------------------------------------------------------------------
        // Applies movement relative to the ship's local axes:
        // - Forward (Z-axis): move forward/backward
        // - Right (X-axis): strafe left/right
        // - Up (Y-axis): ascend/descend (hover)
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right * activeStrafeSpeed * Time.deltaTime;
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;

        //-----------------------------------------------------------------------------------------------------------------------------
        // Warp Speed (Boost) toggle – multiplies movement speed while holding Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Warp speed
            forwardSpeed *= 4f;
            strafeSpeed *= 4f;
            hoverSpeed *= 4f;
        }
        // Reset speed to normal value
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Reset to normal speed
            forwardSpeed /= 4f;
            strafeSpeed /= 4f;
            hoverSpeed /= 4f;
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        // Firing Lasers
        // this will call method FireLaser() from Laser.cs
        if (Input.GetMouseButtonDown(0))
        {
            foreach (Laser l in laser)
            {
                l.FireLaser();
            }
        }

    }

    public Vector3 GetMouseAimPosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    // Player Ship Collision Handling
    // If collides with an enemy or asteroid, apply damage and trigger explosion effects
    // Damage application is handled via Explosion.cs logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "asteroid")
        {
            // hit an asteroid
            Explosion temp = transform.GetComponent<Explosion>();                       // Get explosion component
            temp.IveBeenHit(transform.position);                                        // Trigger ship impact effect (particle effect) 
            temp.BlowUpEnemy(other.gameObject.transform.position, other.gameObject);    // Call blow up enemy - to destroy that asteroid
        }
        else if (other.gameObject.tag == "enemy")
        {
            // hit an enemy ship 
            Explosion temp = transform.GetComponent<Explosion>();
            temp.IveBeenHitHard(transform.position);
            temp.BlowUpEnemy(other.gameObject.transform.position, other.gameObject);    // Call blow up enemy - to destroy that enemy ship
        }
        else
        {
            // unknown collision - for special cases
            Debug.Log("you hit something unknown");
        }

    }

}

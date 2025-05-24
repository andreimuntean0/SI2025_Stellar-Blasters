using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        screenCenter.x = Screen.width  * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcc * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical")   * forwardSpeed, forwardAcc * Time.deltaTime);
        activeStrafeSpeed  = Mathf.Lerp(activeStrafeSpeed,  Input.GetAxisRaw("Horizontal") * strafeSpeed,  strafeAcc  * Time.deltaTime);
        activeHoverSpeed   = Mathf.Lerp(activeHoverSpeed,   Input.GetAxisRaw("Hover")      * hoverSpeed,   hoverAcc   * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += transform.right   * activeStrafeSpeed  * Time.deltaTime;
        transform.position += transform.up      * activeHoverSpeed   * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Warp speed
            forwardSpeed *= 4f;
            strafeSpeed *= 4f;
            hoverSpeed *= 4f;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Reset to normal speed
            forwardSpeed /= 4f;
            strafeSpeed /= 4f;
            hoverSpeed /= 4f;
        }

        if (Input.GetMouseButtonDown(0))
        {
             foreach(Laser l in laser)
            {
                // Vector3 pos = transform.position + (transform.forward * l.Distance);
                l.FireLaser();
            }
        }

    }

    public Vector3 GetMouseAimPosition ()
    {
        return new Vector3 (Input.mousePosition.x, Input.mousePosition.y , 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "asteroid")
        {
            Explosion temp = transform.GetComponent<Explosion>();
            temp.IveBeenHit(transform.position);
            temp.BlowUpEnemy(other.gameObject.transform.position, other.gameObject);
        }
        else if(other.gameObject.tag == "enemy")
        {
            Explosion temp = transform.GetComponent<Explosion>();
            temp.IveBeenHitHard(transform.position);
            temp.BlowUpEnemy(other.gameObject.transform.position, other.gameObject);
        }
        else
        {
            Debug.Log("you hit something unknown");
        }

    }

}

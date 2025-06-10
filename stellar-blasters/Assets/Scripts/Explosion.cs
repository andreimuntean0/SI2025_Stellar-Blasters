using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    UnityEngine.GameObject explosion; // prefab for the explosion

    [SerializeField]
    UnityEngine.GameObject blowUp;  // prefab for the blow-up of the ship

    [SerializeField]
    Shield shield;  // reference to the Shield component of the player

    // Called when an object receives a light hit (contact with enemy laser or asteroids)
    public void IveBeenHit(Vector3 pos)
    {
        // Instantiates the explosion effect at position pos, with no rotation, as a child of the current object.
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);  // Destroys the explosion effect after 1 second.

        if (shield == null)
            return;

        shield.TakeDamage();            // Applies standard damage to the shield.
        EventManager.ScorePoints(-5);   // Deducts 5 points from the score.
    }

    // Called when an object receives a hard hit (contact with enemy ship)
    public void IveBeenHitHard(Vector3 pos)
    {
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);

        if (shield == null)
            return;

        shield.TakeDamage(30);          // Applies 30 damage points to the shield.
        EventManager.ScorePoints(-10);  // Deducts 10 points from the score.
    }


    // Called when player ships hits an enemy
    public void EnemyHit(Vector3 pos, GameObject enemy)
    {
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);
        // Spawns a standard explosion effect and destroys the effect after 1 second.
        BlowUpEnemy(pos, enemy);  // Calls BlowUpEnemy() to destroy the enemy both visually and logically.
        EventManager.ScorePoints(10);  // Awards 10 points to the player.
    }

    public void BlowUp()
    {   
        // Triggers the blow-up effect for the player ship when the shield reaches 0.
        Instantiate(blowUp);
        Destroy(gameObject);
    }

    public void BlowUpEnemy(Vector3 pos, GameObject enemy)
    {
        // Triggers the blow-up effect for the enemy ship when the player hits it with the lasers.
        GameObject container = new GameObject("BlowUpContainer");  // Creates an empty GameObject to hold the explosion effect.
        container.transform.position = pos;                        // Places it at the explosion position.
        container.transform.localScale = Vector3.one;
        
        GameObject go = Instantiate(blowUp, container.transform);  // Instantiates the explosion inside the container.
        go.transform.localPosition = Vector3.zero;                 // Centers it locally within the container.
        Destroy(go, 0.5f);                                         // Destroys the explosion effect after 0.5 seconds.
        Destroy(enemy);                                            // Removes the enemy GameObject from the scene.
    }


}

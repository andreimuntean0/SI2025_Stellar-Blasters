using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    UnityEngine.GameObject explosion;

    [SerializeField]
    UnityEngine.GameObject blowUp;

    [SerializeField]
    Shield shield;

    public void IveBeenHit(Vector3 pos)
    {
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);

        if(shield == null)
            return;
        
        shield.TakeDamage();
        EventManager.ScorePoints(-5);
    }

    public void IveBeenHitHard(Vector3 pos)
    {
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);

        if(shield == null)
            return;
        
        shield.TakeDamage(30);
        EventManager.ScorePoints(-10);
    }


    public void EnemyHit(Vector3 pos, GameObject enemy)
    {
        UnityEngine.GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as UnityEngine.GameObject;
        Destroy(go, 1f);
        BlowUpEnemy(pos, enemy);
        EventManager.ScorePoints(10);
    }

    public void BlowUp()
    {
        Instantiate(blowUp);
        Destroy(gameObject);
    }

    public void BlowUpEnemy(Vector3 pos, GameObject enemy)
    {
        GameObject container = new GameObject("BlowUpContainer");
        container.transform.position = pos;
        container.transform.localScale = Vector3.one;
        
        GameObject go = Instantiate(blowUp, container.transform);
        go.transform.localPosition = Vector3.zero;
        Destroy(go, 0.5f);
        Destroy(enemy);
    }


}

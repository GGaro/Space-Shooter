using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PickUps : MonoBehaviour
{
    static int points = 100;
    bool gotHit = false;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.transform.CompareTag("Player"))
        {   
                PickUpHit();
            Debug.Log("Hit");
        }
    }

    public void PickUpHit()
    {
        if (!gotHit)
        {
            gotHit = true;
            EventManager.ScorePoints(points);
            EventManager.ReSpawnPick();
            Destroy(gameObject);
        }
    }
}

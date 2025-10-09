using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinbehavior : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        // test if other object is player
        //find an object which has levelmanager behavior attached to it and call from it
        //find level manager and call coin function
        FindObjectOfType<LevelManager>().CollectCoin();

        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 20;
    void Start()
    {
        //add base to game manager
    }
    void OnCollisionEnter(Collision collision)
    {
       
        
            
            if (health <= 0)
            {
                //create destruction effect
                //report to GM
                //destroy raft LAST
                Destroy(gameObject);
            }
        

    }
}

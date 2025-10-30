using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftScript : MonoBehaviour
{
    public int health = 1;
    public int score = 50;
    void Start()
    {
       //add raft to game manager
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            health--;
            if (health <= 0)
            {
                //create destruction effect
                //report to GM
                //report to score manager
                //destroy raft LAST
                Destroy(gameObject);
            }
        }
    }
    
}

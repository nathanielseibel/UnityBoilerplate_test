using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public int health = 1;
    public int damage = 2;

    public BaseManager Manager;

    void Start()
    {
        Manager = FindObjectOfType<BaseManager>();
        //add bomb to game manager
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health--;
            if (health <= 0)
            {
                //create destruction effect
                //report to GM
                //destroy raft LAST
                Destroy(gameObject);
            }
        }

    }
}

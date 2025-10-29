using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    
    void Update()
    {
        var rb = GetComponent<Rigidbody>();
        var vel = rb.velocity;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(10f, vel.y, vel.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-10f, vel.y, vel.z);
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody>();
        var vel = rb.velocity;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(3f, vel.y, vel.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-3f, vel.y, vel.z);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 5f, 0);
        }
    }
}

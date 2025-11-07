using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static PlayerBehavior Instance;
    Rigidbody rb;
    BoxCollider col;
    float speed = 30f;
    public float rightScreenEdge;
    public float leftScreenEdge;


    void Awake()
    {
        Instance = this;

    }

    void Start()
    {
       var  rb = GetComponent<Rigidbody>();
       var  col = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*moveInput*speed*Time.deltaTime);
        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector3(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector3(rightScreenEdge, transform.position.y);
        }
    }
}


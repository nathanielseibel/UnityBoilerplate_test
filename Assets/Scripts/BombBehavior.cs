using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombBehavior : MonoBehaviour
{
    public int health = 1;
    public int damage = 2;
    [SerializeField] private float initialSpeed = 300f;
    Rigidbody rb;
    [SerializeField] private float ballSpeed = 35f;



    [SerializeField] private BaseManager Manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, initialSpeed, 0));

    }
    void OnCollisionEnter(Collision collision)
    {

        //ignore collision with bricks
        if (collision.gameObject.tag == "Brick")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }




        if (collision.gameObject.tag == "Base")
        {
            health--;

            if (health <= 0)
            { 
                Destroy(gameObject);
            }
        }

        //if collides with wall, bounce off
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 normal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, normal);
        }


        // Add a small random vector to the ball's velocity to prevent straight lines
        Vector2 random2D = UnityEngine.Random.insideUnitCircle.normalized;
        rb.velocity += new Vector3(random2D.x, random2D.y, 0);

    }

    private void Update()
    {
        

        //apply a downward force to the ball constantly
        rb.AddForce(new Vector3(0, -5f, 0));

        //overtime decrease the ball speed to a minimum of 0
        if (ballSpeed > 0f)
        {
            ballSpeed -= Time.deltaTime * 10f;
        }
    }


}

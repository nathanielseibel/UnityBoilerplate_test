
using UnityEngine;


public class BaseManager : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int maxHealth = 20;
    public GameOverManager gameOverManager;

    //get current health
    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage()
    {
        health--;
    }

    void Start()
    {
        // Update health UI at start
        ScoreManager.Instance.UpdateDamHealth(health);
    }


    //  Handle collisions with bombs
    //  I gave the base a rigidbody and a collider set to isTrigger to detect bomb hits
    //  The base is set to kinematic so it doesn't move and the ball will pass through it
    //  When a bomb collides with the base, OnTriggerEnter is called
    //  The base's health is reduced and the bomb is destroyed
    //  If health reaches zero, the base is destroyed
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that hit the base is a bomb
        if (other.CompareTag("Bomb"))
        {
            // Reduce health
            TakeDamage();
            //update health UI
            ScoreManager.Instance.UpdateDamHealth(health);

            // Destroy the bomb as well
            BombBehavior bombScript = gameObject.GetComponent<BombBehavior>();
            bombScript.TakeDamage();


            // Check if base should be destroyed
            if (health <= 0)
            {
                
                //Destroy this dam
                Destroy(gameObject);
                gameOverManager.ShowGameOverUI();
                
            }

            

        }
    }
}

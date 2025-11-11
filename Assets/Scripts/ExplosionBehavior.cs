using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    // References to explosion effect and sound
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;

    // variable for explosion duration
    [SerializeField] private float explosionDuration = 5.0f;
    // variable for explosion expansion speed
    [SerializeField] private float expansionSpeed = 4.5f;

    [SerializeField] private float maxExplosionSize = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, explosionDuration);
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        //expand the explosion over time and then destroy it
        StartCoroutine(ExpandAndDestroy());


    }

    private IEnumerator ExpandAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = initialScale * maxExplosionSize;
        // Play explosion sound
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        while (elapsedTime < explosionDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / explosionDuration));
            elapsedTime += Time.deltaTime * expansionSpeed;
            yield return null;
        }
        // Ensure final scale is set
        transform.localScale = targetScale;
        // Destroy the explosion object after the duration
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the explosion is a brick
        if (other.CompareTag("Brick") || other.CompareTag("SpeedBrick") || other.CompareTag("TankyBrick") || other.CompareTag("SuperTankyBrick"))
        {
            // Damage the brick by 1hp
            RaftScript brickScript = other.GetComponent<RaftScript>();
            if (brickScript != null)
            {
                brickScript.TakeDamage();
                //Check if the brick is out of hp and destroy it if so
                if (brickScript.GetHealth() <= 0)
                {
                    Destroy(other.gameObject);
                }
            }

        }

        // Check if the object that entered the explosion is a bomb
        if (other.CompareTag("Bomb"))
        {
            // Destroy the bomb
            Destroy(other.gameObject);
        }
    }
}

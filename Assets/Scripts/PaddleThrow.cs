
using UnityEngine;

public class PaddleThrow : MonoBehaviour
{
    public static PaddleThrow Instance;
    Rigidbody rb;
    BoxCollider col;
    float speed = 30f;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public Transform mousePressPosition;
    public float maxForce = 10f; // Maximum force you want to apply

    
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

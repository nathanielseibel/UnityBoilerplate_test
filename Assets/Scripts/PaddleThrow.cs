
using UnityEngine;

public class PaddleThrow : MonoBehaviour
{
    Rigidbody rb;
    BoxCollider col;
    float speed = 30f;
    public float rightScreenEdge;
    public float leftScreenEdge;
    private Vector3 mousePressPosition;
    private Vector3 mouseReleasePosition;
    private float dragDistance;
    public float maxForce = 10f; // Maximum force you want to apply
    public LineRenderer lineRenderer; // Optional: For visual feedback of aiming line


    
    void OnMouseDown()
    {
        // Record the initial mouse position in world space when the mouse button is pressed on the paddle
        mousePressPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePressPosition.z = 0; // Ensure Z is zero for 2D games
        
        // Optional: Start drawing the aiming line
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
        }
    }
    void OnMouseDrag()
    {
        // Track the current mouse position as it is being dragged
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = 0;

        // Calculate the direction and distance of the drag
        Vector3 dragVector = currentMousePosition - mousePressPosition;
        dragDistance = Mathf.Clamp(dragVector.magnitude, 0, maxForce);
        Vector3 aimingDirection = dragVector.normalized;
        
        // Optional: Update the aiming line end point (e.g., in the opposite direction for "pull back" aiming)
        if (lineRenderer != null)
        {
            Vector3 lineEndPoint = transform.position - aimingDirection * dragDistance;
            lineRenderer.SetPosition(1, lineEndPoint);
        }
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
    void OnMouseUp()
    {
        // Record the final mouse position and calculate the force
        mouseReleasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseReleasePosition.z = 0;

        // Calculate the force vector (opposite direction of drag, scaled by distance)
        Vector3 forceDirection = (mousePressPosition - mouseReleasePosition).normalized;
        float forceMagnitude = Mathf.Clamp(Vector3.Distance(mousePressPosition, mouseReleasePosition), 0, maxForce);
        Vector3 finalForce = forceDirection * forceMagnitude;

        // **Apply the force to a projectile or the paddle's Rigidbody here**
        // Example (assuming you have a Rigidbody2D component on a *separate* ball/projectile):
        Rigidbody projectile = GetComponent<Rigidbody>();
        projectile.GetComponent<Rigidbody>().AddForce(finalForce, ForceMode.Impulse);

        // Optional: Hide the aiming line
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }
    
}

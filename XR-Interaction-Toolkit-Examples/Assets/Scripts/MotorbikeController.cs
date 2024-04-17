using UnityEngine;

public class MotorbikeController : MonoBehaviour
{
    public float force = 500.0f;
    public float maxRotationSpeed = 100.0f;
    public float lateralDragFactor = 0.99f;   // Drag factor for lateral movement reduction


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Forward movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forceDirection = transform.up * moveVertical * force;
        rb.AddForce(forceDirection);

        float currentSpeed = rb.velocity.magnitude;
        var localVel = transform.InverseTransformDirection(rb.velocity);
        
        localVel.y *= lateralDragFactor;
        rb.velocity = transform.TransformDirection(localVel);


        // Turning
        if (currentSpeed > 0.05) {
            float rotationFactor = 1;
            if(localVel.y < 0){
                rotationFactor = -1;
            }
            float rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, currentSpeed / 10.0f); // Assuming 10.0f is the speed at which maximum rotation rate is achieved
            float rotation = moveHorizontal * rotationSpeed * Time.deltaTime * rotationFactor;
            Quaternion turn = Quaternion.Euler(0f, 0f, rotation);
            rb.MoveRotation(rb.rotation * turn);
        }
    }
}

using UnityEngine;
using Unity.Netcode;

public class MotorbikeController : NetworkBehaviour
{
    public float force = 500.0f;
    public float maxRotationSpeed = 100.0f;
    public float lateralFriction = 0.95f;  // High friction to minimize unwanted lateral movement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!IsOwner) return;

        // Input for acceleration and steering
        // Applying forward force
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forceDirection = transform.up * moveVertical * force;


        if (moveVertical > 0) {
            // Forward movement
            rb.AddForce(forceDirection);
        } else if (moveVertical < 0) {
            // Braking - apply a force in the opposite direction of current movement
            Vector3 brakingForceDirection = -rb.velocity.normalized * Mathf.Abs(moveVertical) * force;
            rb.AddForce(brakingForceDirection);
        }



        // Calculate the current speed and local velocity
        float currentSpeed = rb.velocity.magnitude;
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);

        // Apply high friction to lateral movement (x-axis for your model's orientation)
        localVel.x *= lateralFriction;
        rb.velocity = transform.TransformDirection(localVel);

        // Apply turning
        if (currentSpeed > 0.05) {  // Ensures there's some movement for realistic turning
            float rotationFactor = localVel.y >= 0 ? 1 : -1;
            float rotationSpeed = Mathf.Lerp(0, maxRotationSpeed, currentSpeed / 10.0f); // Assuming 10.0f is the speed at which maximum rotation rate is achieved
            float rotation = moveHorizontal * rotationSpeed * Time.deltaTime * rotationFactor;
            Quaternion turn = Quaternion.Euler(0f, 0f, rotation);  // Rotation around the z-axis
            rb.MoveRotation(rb.rotation * turn);
        }
    }
}

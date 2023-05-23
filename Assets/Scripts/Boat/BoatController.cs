using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BoatController : MonoBehaviour
{

    [SerializeField]
    float thrustForce = 10f;

    [SerializeField]
    float torqueForce = 10f;

    [SerializeField]
    Transform leftFloater;

    [SerializeField]
    Transform rightFloater;

    [SerializeField]
    InputAction noseForce = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction tailForce = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction rightAngleTorque = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction leftAngleTorque = new InputAction(type: InputActionType.Button);

    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        noseForce.Enable();
        tailForce.Enable();
        rightAngleTorque.Enable();
        leftAngleTorque.Enable();
    }

    void OnDisable()
    {
        noseForce.Disable();
        tailForce.Disable();
        rightAngleTorque.Disable();
        leftAngleTorque.Disable();
    }

    private void Update()
    {
        float waveHeight = WaveManager.instance.GetWaveHeight(_x: transform.position.x);
        if (transform.position.y < waveHeight)
        {

            // // Get the values of the right and left angle torque inputs.
            float rightAngle = rightAngleTorque.ReadValue<float>();
            float leftAngle = leftAngleTorque.ReadValue<float>();
            // if (rightAngle == 1)
            // {
            //     rb.AddForceAtPosition(Vector3.left * torqueForce, rightFloater.transform.position, ForceMode.Acceleration);
            //     rb.velocity = new Vector3(torqueForce, rb.velocity.y, rb.velocity.z);
            // }
            // if (leftAngle == 1)
            // {
            //     rb.AddForceAtPosition(Vector3.right * torqueForce, leftFloater.transform.position, ForceMode.Acceleration);
            //     rb.velocity = new Vector3(torqueForce, rb.velocity.y, rb.velocity.z);
            // }
            float nose = noseForce.ReadValue<float>();
            float tail = tailForce.ReadValue<float>();
            // Calculate the force applied to the boat.
            // The force is calculated by multiplying the thrust force by the nose and tail values.
            // The tail value is 1 if the player is pressing the arrow down, and 0 if he is not.
            // Apply the force to the boat.
            // if (nose == 1)
            // {
            //     rb.AddRelativeForce(Vector3.forward * thrustForce, ForceMode.Acceleration);
            //     Vector3 rot = rb.transform.rotation.eulerAngles;
            //     rot = new Vector3(rot.x, rot.y - 90, rot.z);
            //     Quaternion rotation = Quaternion.Euler(rot);
            //     Vector3 movement = rotation * Vector3.forward * thrustForce;
            //     movement.y = rb.velocity.y;
            //     rb.velocity = movement;
            // }
            // if (tail == 1)
            // {
            //     rb.AddRelativeForce(Vector3.back * thrustForce, ForceMode.Acceleration);
            //     Vector3 rot = rb.transform.rotation.eulerAngles;
            //     rot = new Vector3(rot.x, rot.y - 90, rot.z);
            //     Quaternion rotation = Quaternion.Euler(rot);
            //     Vector3 movement = rotation * Vector3.forward * thrustForce;
            //     movement.y = rb.velocity.y;
            //     rb.velocity = movement;
            // }
        }
    }
}

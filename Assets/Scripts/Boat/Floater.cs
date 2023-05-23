using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// made from this youtube video https://www.youtube.com/watch?v=eL_zHQEju8s&ab_channel=TomWeiland
public class Floater : MonoBehaviour
{
    public int floaterCount;
    public Rigidbody rigidbody;
    public float depth = 1;
    public float displacement = 3;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    private void FixedUpdate()
    {
        rigidbody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.instance.GetWaveHeight(_x: transform.position.x);
        // Under water
        if (transform.position.y <= waveHeight)
        {
            // limits the values between 0 and 1.
            float mul = Mathf.Clamp01((waveHeight - transform.position.y) / depth) * displacement;
            rigidbody.AddForceAtPosition(new Vector3(0, Mathf.Abs(Physics.gravity.y) * mul, -1), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(mul * -rigidbody.velocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(mul * -rigidbody.velocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}

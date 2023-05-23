using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BoatAgent : Agent
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
    Transform boatTop;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    Transform targetTransform;

    [SerializeField]
    Vector3 startPosition = new Vector3(80.0f, -0.5f, 0f);

    [SerializeField]
    Vector3 rotatePosition = new Vector3(0.0f, 180.0f, 0.0f);

    public override void OnEpisodeBegin()
    {
        rb.transform.localPosition = startPosition;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int moveZ = actions.DiscreteActions[0];
        int moveX = actions.DiscreteActions[1];
        float waveHeight = WaveManager.instance.GetWaveHeight(_x: transform.position.x);
        if (transform.position.y < waveHeight)
        {
            if (moveZ == 2)
            {
                rb.AddForceAtPosition(Vector3.left * torqueForce, rightFloater.transform.position, ForceMode.Acceleration);
                UpdateVelocity();
            }
            if (moveZ == 1)
            {
                rb.AddForceAtPosition(Vector3.right * torqueForce, leftFloater.transform.position, ForceMode.Acceleration);
                UpdateVelocity();
            }
            if (moveX == 2)
            {
                rb.AddRelativeForce(Vector3.forward * thrustForce, ForceMode.Acceleration);
                MoveInDirection();
            }
            else if (moveX == 1)
            {
                rb.AddRelativeForce(Vector3.back * thrustForce, ForceMode.Acceleration);
                MoveInDirection();
            }
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteActions[0] = 1; break;
            case 0: discreteActions[0] = 0; break;
            case +1: discreteActions[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteActions[1] = 1; break;
            case 0: discreteActions[1] = 0; break;
            case +1: discreteActions[1] = 2; break;
        }
    }
    public void RewardUpdate(float reward)
    {
        AddReward(reward);
        EndEpisode();
    }
    private void MoveInDirection()
    {
        Vector3 rot = rb.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - 90, rot.z);
        Quaternion rotation = Quaternion.Euler(rot);
        Vector3 movement = rotation * Vector3.forward * thrustForce;
        movement.y = rb.velocity.y;
        rb.velocity = movement;
    }
    private void UpdateVelocity()
    {
        rb.velocity = new Vector3(torqueForce, rb.velocity.y, rb.velocity.z);
    }
    private void Update()
    {
        float waveHeight = WaveManager.instance.GetWaveHeight(_x: boatTop.transform.position.x);
        if (waveHeight > boatTop.transform.position.y)
        {
            rb.transform.eulerAngles = rotatePosition * Time.deltaTime;
            RewardUpdate(-0.1f);
        }
    }
}
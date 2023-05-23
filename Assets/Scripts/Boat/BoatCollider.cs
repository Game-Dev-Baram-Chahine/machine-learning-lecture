using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            BoatAgent boatAgent = GetComponentInChildren<BoatAgent>();
            Debug.Log("Success");
            boatAgent.RewardUpdate(1.0f);
        }
    }
}

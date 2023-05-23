using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// made from this youtube video https://www.youtube.com/watch?v=eL_zHQEju8s&ab_channel=TomWeiland

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public float amplitude = 1;
    public float length = 1;
    public float speed = 1;
    public float offset = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Already exists");
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }
    public float GetWaveHeight(float _x)
    {
        return amplitude * Mathf.Sin(_x / length + offset);
    }

}

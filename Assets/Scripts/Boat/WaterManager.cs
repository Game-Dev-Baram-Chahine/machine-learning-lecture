using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// made from this youtube video https://www.youtube.com/watch?v=eL_zHQEju8s&ab_channel=TomWeiland

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }
        meshFilter.mesh.vertices = vertices;
        // Recalculates the normals of the Mesh from the triangles and vertices af.
        meshFilter.mesh.RecalculateNormals();
    }
}

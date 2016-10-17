using UnityEngine;
using System.Collections;

public class Figure : MonoBehaviour
{
    public Vector3[] newVertices;
    public Vector2[] newUV;
    public int[] newTriangles;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
    }
}
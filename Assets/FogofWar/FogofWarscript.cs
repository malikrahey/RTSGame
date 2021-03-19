using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogofWarscript : MonoBehaviour
{
    public GameObject FogofWarPlane;
    public Transform units;
    public LayerMask FogLayer;
    public float radius = 5f;
    private float radiusSquared { get { return radius * radius; } }

    private Mesh mesh;
    private Vector3[] vertices;
    private Color[] colours;
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        mesh = FogofWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colours = new Color[vertices.Length];
        for(int i = 0; i < colours.Length; i++)
        {
            colours[i] = Color.black;
        }
        UpdateColours();
    }

    void UpdateColours()
    {
        mesh.colors = colours;
    }
}

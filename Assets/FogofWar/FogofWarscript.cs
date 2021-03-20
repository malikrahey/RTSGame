using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogofWarscript : MonoBehaviour
{
    public GameObject FogofWarPlane;
    public Transform units;
    public LayerMask FogLayer;
    public float radius = 5f;
    public Camera View;
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
        Ray r = new Ray(View.transform.position,units.position - View.transform.position);
        RaycastHit hit;
        Debug.DrawRay(View.transform.position, (units.position - View.transform.position) * 1000, Color.red, Mathf.Infinity);
        if (Physics.Raycast(r, out hit, 1000, FogLayer, QueryTriggerInteraction.Collide))
        {
            //Debug.Log("Collision detected");
            for(int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = FogofWarPlane.transform.TransformPoint(vertices[i]);
                float dis = Vector3.SqrMagnitude(v - hit.point);
                if (dis < radiusSquared)
                {
                    float alpha = Mathf.Min(colours[i].a, dis / radiusSquared);
                    colours[i].a = alpha;
                    //Debug.Log("New Alpha");
                }
            }
            UpdateColours();
        }
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

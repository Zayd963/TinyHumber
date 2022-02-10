using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool visited = false;
    public float globalGoal = 0;
    public float localGoal = 0;
    public Node[] connectedNodes;
    public Node parent;
    public Material colour;

    // Start is called before the first frame update
    void Start()
    {
        colour = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //if (visited)
        //    colour.SetColor("_Color", Color.blue);

    }
}

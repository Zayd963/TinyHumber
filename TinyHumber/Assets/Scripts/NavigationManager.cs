using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public List<Node> nodes;
    public Node startingNode;
    public Node endingNode;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(endingNode != null)
            {
                GetPath();
                Node p = endingNode;
                while(p.parent != null)
                {
                    Debug.Log(p.name);
                    p.colour.SetColor("_Color", Color.yellow);
                    p = p.parent;
                }
            }
        }

        startingNode.colour.SetColor("_Color", Color.red);
        endingNode.colour.SetColor("_Color", Color.blue);
    }

    float GetDistance(Node lhs, Node rhs)
    {
        Vector2 temp = lhs.transform.position - rhs.transform.position;
        return temp.magnitude;
    }

    float GetHeuristic(Node lhs, Node rhs)
    {
        return GetDistance(lhs, rhs);
    }

    static int CompareDistances(Node lhs, Node rhs)
    {
        return lhs.globalGoal.CompareTo(rhs.globalGoal);
    }

    bool GetPath()
    {
        foreach(Node node in nodes)
        {
            node.visited = false;
            node.globalGoal = Mathf.Infinity;
            node.localGoal = Mathf.Infinity;
            node.parent = null;
        }

        Node currentNode = startingNode;
        startingNode.localGoal = 0;
        startingNode.globalGoal = GetHeuristic(startingNode, endingNode);

        List<Node> nodesToTest = new List<Node>();
        nodesToTest.Add(startingNode);

        while(nodesToTest.Count != 0 && currentNode != endingNode)
        {
            nodesToTest.Sort(CompareDistances);

            while (nodesToTest.Count != 0 && nodesToTest[0].visited)
                nodesToTest.RemoveAt(0);

            if (nodesToTest.Count <= 0)
                break;

            currentNode = nodesToTest[0];
            currentNode.visited = true;

            foreach(Node connectedNode in currentNode.connectedNodes)
            {
                if (!connectedNode.visited)
                    nodesToTest.Add(connectedNode);

                float possiblyLowerGoal = currentNode.localGoal + GetDistance(currentNode, connectedNode);

                if(possiblyLowerGoal < connectedNode.localGoal)
                {
                    connectedNode.parent = currentNode;
                    connectedNode.localGoal = possiblyLowerGoal;

                    connectedNode.globalGoal = connectedNode.localGoal + GetHeuristic(connectedNode, endingNode);
                }
            }
        }

        startingNode.parent = null;
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator
{
    public struct Node
    {
        public GameObject obj;
        public Dictionary<Node, float> adjacencies;

        public Node(GameObject o, Dictionary<Node, float> a)
        {
            obj = o;
            adjacencies = a;
        }

        public Node(GameObject o)
        {
            obj = o;
            adjacencies = null;
        }
    };

    class NodeCompare : EqualityComparer<Node>
    {
        public override bool Equals(Node a, Node b)
        {
            return (a).obj.name.Equals((b).obj.name);
        }

        public override int GetHashCode(Node a)
        {

            return (a).GetHashCode();
        }

    }


    private Node[] nodes;
    private GameObject[] anchors;
    public Material originalMaterial = null;
    public Material closestMaterial = null;

    public Navigator()
    {

    }

    public void Setup(Material material) {
        //get all anchors
        closestMaterial = material;
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
        originalMaterial = anchors[0].GetComponent<Renderer>().material;
        //create a list of nodes to access the graph in constant time
        nodes = new Node[anchors.Length];
        for (int i = 0; i < anchors.Length; i++)
        {
            nodes[i] = new Node(GameObject.Find(i.ToString()));
        }

        //Fill in adjacencies
        nodes[0].adjacencies = makeDict(new Node[] { nodes[1], nodes[10], nodes[11] });
        nodes[1].adjacencies = makeDict(new Node[] { nodes[0], nodes[2] });
        nodes[2].adjacencies = makeDict(new Node[] { nodes[1], nodes[3] });
        nodes[3].adjacencies = makeDict(new Node[] { nodes[2], nodes[4], nodes[17] });
        nodes[4].adjacencies = makeDict(new Node[] { nodes[3], nodes[5] });
        nodes[5].adjacencies = makeDict(new Node[] { nodes[4], nodes[6] });
        nodes[6].adjacencies = makeDict(new Node[] { nodes[5], nodes[7] });
        nodes[7].adjacencies = makeDict(new Node[] { nodes[6], nodes[8] });
        nodes[8].adjacencies = makeDict(new Node[] { nodes[7], nodes[9] });
        nodes[9].adjacencies = makeDict(new Node[] { nodes[8], nodes[28] });
        nodes[10].adjacencies = makeDict(new Node[] { nodes[0] });
        nodes[11].adjacencies = makeDict(new Node[] { nodes[0], nodes[12] });
        nodes[12].adjacencies = makeDict(new Node[] { nodes[13], nodes[11] });
        nodes[13].adjacencies = makeDict(new Node[] { nodes[12], nodes[14] });
        nodes[14].adjacencies = makeDict(new Node[] { nodes[13], nodes[15], nodes[18] });
        nodes[15].adjacencies = makeDict(new Node[] { nodes[14], nodes[16] });
        nodes[16].adjacencies = makeDict(new Node[] { nodes[15], nodes[17], nodes[20] });
        nodes[17].adjacencies = makeDict(new Node[] { nodes[3], nodes[20] });
        nodes[18].adjacencies = makeDict(new Node[] { nodes[14], nodes[19], nodes[21] });
        nodes[19].adjacencies = makeDict(new Node[] { nodes[18] });

        nodes[20].adjacencies = makeDict(new Node[] { nodes[17], nodes[16] });
        nodes[21].adjacencies = makeDict(new Node[] { nodes[18], nodes[22] });
        nodes[22].adjacencies = makeDict(new Node[] { nodes[21], nodes[23] });
        nodes[23].adjacencies = makeDict(new Node[] { nodes[22], nodes[24] });
        nodes[24].adjacencies = makeDict(new Node[] { nodes[23], nodes[26] });
        nodes[25].adjacencies = makeDict(new Node[] { nodes[27], nodes[31] });
        nodes[26].adjacencies = makeDict(new Node[] { nodes[24], nodes[27] });
        nodes[27].adjacencies = makeDict(new Node[] { nodes[26], nodes[25] });
        nodes[28].adjacencies = makeDict(new Node[] { nodes[9], nodes[29] });
        nodes[29].adjacencies = makeDict(new Node[] { nodes[28], nodes[30] });
        nodes[30].adjacencies = makeDict(new Node[] { nodes[29], nodes[31] });
        nodes[31].adjacencies = makeDict(new Node[] { nodes[30], nodes[25] });


        //calculate the weights for the edges
        //calculateWeights(nodes);
    }

    public GameObject getClosest(Vector3 pos)
    {

        float min = Vector3.Distance(pos, nodes[0].obj.transform.position);
        GameObject ret = nodes[0].obj;
        for (int i = 1; i < nodes.Length; i++)
        {
            if (min > Vector3.Distance(pos, nodes[i].obj.transform.position))
            {
                ret = nodes[i].obj;
                min = Vector3.Distance(pos, nodes[i].obj.transform.position);
            }
        }
        return ret;
    }

    public int getClosestInt(Vector3 start)
    {
        GameObject closest = getClosest(start);
        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i].obj.name.Equals(closest.name))
            {
                return i;
            }
        }
        return -1;
    }

    public void Recolor(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = closestMaterial;
    }

    private Dictionary<Node, float> makeDict(Node[] nodes)
    {
        Dictionary<Node, float> adj = new Dictionary<Node, float>();
        for (int i = 0; i < nodes.Length; i++)
        {
            adj.Add(nodes[i], 1.0f);
        }
        return adj;
    }

    public GameObject[] Dijkstras(Node[] graph, int source, int dest)
    {


        Dictionary<string, float> dist = new Dictionary<string, float>();

        for (int i = 0; i < graph.Length; i++)
        {
            if (i == source)
            {
                dist[graph[i].obj.name] = 0;
            }
            else
            {
                dist[graph[i].obj.name] = float.MaxValue;
            }
        }

        List<Node> Q = new List<Node>(graph);
        List<Node> S = new List<Node>();
        var Parent = new Dictionary<string, Node>();
        Parent[graph[source].obj.name] = new Node(null);
        Node V = graph[source];
        int temp = 0;
        while (Q.Count != 0)
        {

            float minDist = float.MaxValue;
            foreach (var node in Q)
            {
                if (minDist > dist[node.obj.name])
                {
                    minDist = dist[node.obj.name];
                    V = node;
                }
            }
            S.Add(V);
            if (temp > 50)
            {
                return new GameObject[] { };
            }
            temp++;

            foreach (KeyValuePair<Node, float> neighbor in V.adjacencies)
            {

                float alt = dist[V.obj.name] + neighbor.Value;

                if (alt < dist[neighbor.Key.obj.name])
                {
                    dist[neighbor.Key.obj.name] = alt;
                    Parent[neighbor.Key.obj.name] = V;
                }
            }
            Q.Remove(V);
        }

        List<GameObject> ret = new List<GameObject>();
        Node last = graph[dest];
        int j = 0;


        while (Parent[last.obj.name].obj != null)
        {
            j++;
            ret.Add(((Node)last).obj);
            last = Parent[last.obj.name];
        }
        ret.Add(graph[source].obj);

        return ret.ToArray();

    }


    public void PrintDict(Dictionary<string, Node> dict)
    {
        foreach (KeyValuePair<string, Node> pair in dict)
        {
            if (pair.Value.obj == null)
            {
                Debug.Log(pair.Key + " => null");
            }
            else
            {
                Debug.Log(pair.Key + " => " + pair.Value.obj.name);
            }
        }
    }
   
    private void calculateWeights(Node[] nodes)
    {
        foreach (var node in nodes)
        {
            foreach (Node neighbor in node.adjacencies.Keys)
            {
                node.adjacencies[neighbor] = Vector3.Distance(node.obj.transform.position, neighbor.obj.transform.position);
            }
        }
        return;
    }

    public void Closest()
    {
        GameObject closestSphere = getClosest(Camera.main.transform.position);
        Recolor(closestSphere);
    }

    public void Reset()
    {
        foreach (GameObject obj in anchors)
        {
            obj.GetComponent<Renderer>().material = originalMaterial;
        }
    }

    public void ProcessCommand(int sphere)
    {
        GameObject[] path = Dijkstras(nodes, getClosestInt(Camera.main.transform.position), sphere);

        for (int i = 0; i < path.Length; i++)
        {

            Recolor(path[i]);
        }

        Recolor(getClosest(Camera.main.transform.position));
    }

    public void Target()
    {
        Reset();
        GameObject targ = GameObject.FindGameObjectWithTag("Rotating");
        int closestToTargetInt = getClosestInt(targ.transform.position);
        ProcessCommand(closestToTargetInt);
    }
}

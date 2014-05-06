using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Problem
    {
        public GraphView form;
        public string point1;
        public string point2;
        public Queue<List<Edge>> ways1;
        public HashSet<string> checked1;
        public PriorityQueue<double, string> front;
        public bool isSolved()
        {
            return front.Contains(new KeyValuePair<double,string>(0,point2));
        }

    }
    public class Graph
    {
        List<Edge> edges;
        public Microsoft.Glee.Drawing.Graph getDrawable()
        {
            Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("graph");
            for (int i = 0; i < edges.Count; i++)
            {
                graph.AddEdge(edges[i].label1, edges[i].label2);
                graph.Edges.Last().Attr.Id = edges[i].label1 + edges[i].label2;
                graph.Edges.Last().EdgeAttr.Label = edges[i].weight.ToString();
                graph.Directed = false;
            }
            return graph;
        }
        public List<string> findNeighbors(string a)
        {
            List<string> rez = new List<string>();
            foreach (Edge edge in edges)
            {
                if (edge.label1 == a || edge.label2 == a)
                {
                    rez.Add(edge.getOther(a));
                }
            }
            return rez;
        }
        public Graph()
        {
            edges = new List<Edge>();
        }
        public void ReadEdges(string file)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            int t = path.Count();
            int d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            t = path.Count();
            d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            t = path.Count();
            d = path.LastIndexOf("\\");
            path = path.Remove(d, t - d);
            path = Path.Combine(path, file);
            System.IO.StreamReader myFile = new System.IO.StreamReader(path);
            while (!myFile.EndOfStream)
            {
                string edge = myFile.ReadLine();
                Edge e = new Edge(edge.Split(' ')[0], edge.Split(' ')[1], double.Parse(edge.Split(' ')[2]));
                edges.Add(e);
            }
            myFile.Close();
        }
        public Edge edgeExists(string a, string b)
        {
            
            Edge current = new Edge(a, b, 0);
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].isEqual(current))
                {
                    current.weight = edges[i].weight;
                }
            }
            return current;
        }
    }
}
public class Edge
{
    public string label1;
    public string label2;
    public double weight;
    public Edge(string l1, string l2, double w)
    {
        label1 = l1;
        label2 = l2;
        weight = w;
    }
    public Edge(string l1, string l2)
    {
        label1 = l1;
        label2 = l2;
        weight = 0;
    }
    public string getOther(string a)
    {
        if (a != label1)
            return label1;
        else
            return label2;
    }
    public bool isEqual(Edge other)
    {
        if ((label1 == other.label1 && label2 == other.label2) || (label1 == other.label2 && label2 == other.label1))
            return true;
        else return false;
    }
}

 

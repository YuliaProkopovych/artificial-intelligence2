using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class PSA
    {

        class Algorism
        {

            Problem problem;
            Graph graph;
            Dictionary<string, double> heuristics;
            public void setProblem(Problem p)
            {
                problem = p;
            }
            public double searchStrategy(string a)
            {
                try
                {
                    return heuristics[a + problem.point2];
                }
                catch (Exception err)
                {
                    try
                    {
                        return heuristics[problem.point2 + a];
                    }
                    catch
                    {
                        return 0;
                    }
                }
                //return heuristics[a+b];
            }
            public double searchStrategy(string a, string b)
            {
                try
                {
                    return heuristics[a + b];
                }
                catch (Exception err)
                {
                    return heuristics[b + a];
                }
                //return heuristics[a+b];
            }
            public void setHeuriristics(Dictionary<string, double> h)
            {
                heuristics = h;
            }
            public List<Edge> getWay()
            {
                List<Edge> rez = new List<Edge>();
                string node = problem.point2;
                while (node != problem.point1)
                {
                    string nextNode = graph.findNeighbors(node).Intersect(problem.checked1).First();
                    if (!rez.Contains(graph.edgeExists(node,nextNode)))
                    {
                        rez.Add(graph.edgeExists(nextNode, node));
                    }
                    else
                    {
                        nextNode = graph.findNeighbors(node).Intersect(problem.checked1).Last();
                        rez.Add(graph.edgeExists(nextNode, node));
                    }
                    node = nextNode;
                }
                rez.Reverse();
                return rez;
            }
            public Problem Step(GraphView form, Graph g)
            {
                graph = g;
                string current = problem.front.Dequeue().Value;
                List<string> neighbours = graph.findNeighbors(current);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    KeyValuePair<double, string> node = new KeyValuePair<double, string>(searchStrategy(neighbours[i]), neighbours[i]);
                    if (!problem.checked1.Contains(neighbours[i]) && !problem.front.Contains(node))
                    {
                        problem.front.Enqueue(node.Key, node.Value);
                    }
                }
                problem.checked1.Add(current);
                form.graphRedraw(problem);
                return problem;
            }
        }
    }
}

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
        protected Graph graph;
        GraphView form;
        Problem task;
        Algorism algo;
        Dictionary<string, double> heuristics;

        public PSA(GraphView f)
        {
            form = f;
            algo = new Algorism();
            task = new Problem();
        }
        public double getF(string a)
        {
            return algo.searchStrategy(a);
        }
        public void setProblem(string a, string b)
        {
            task.point1 = a;
            task.point2 = b;
            task.ways1 = new Queue<List<Edge>>();
            Edge edge1 = new Edge(task.point1,task.point1);
            List<Edge> list1 = new List<Edge>();
            list1.Add(edge1);
            task.ways1.Enqueue(list1);
            task.checked1 = new HashSet<string>();
            task.front = new PriorityQueue<double, string>();
            task.front.Enqueue(algo.searchStrategy(task.point1,task.point2),task.point1);
            
            algo.setProblem(task);
            form.graphRedraw(task);
            form.drawHeuristics(heuristics);
        }
        public void setGraph(string path)
        {
            graph = new Graph();
            graph.ReadEdges(path);
            form.setGraph(graph.getDrawable());
        }
        public void setHeuristics(string file)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            heuristics = new Dictionary<string, double>();
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
            string[] nodes = myFile.ReadLine().Split(' ');
            while (!myFile.EndOfStream)
            {
                string distance = myFile.ReadLine();
                for (int i = 1; i < distance.Split(' ').Count(); i++)
                {
                    heuristics[distance.Split(' ')[0] + nodes[i-1]] = double.Parse(distance.Split(' ')[i]);
                }
            }
            myFile.Close();
            algo.setHeuriristics(heuristics);

        }
        public void Run()
        {
            if(!task.isSolved())
            {
                task = algo.Step(form, graph);
            }
            else
            //if (task.result != " ")
            {
                List<Edge> way = algo.getWay();
                form.getButton().Enabled = false;
                
                string s = "";
                double d = 0;
                for (int i = 0; i < way.Count; i++)
                {
                    s = s + way[i].label1.ToString() + "-" + way[i].label2.ToString() + ";";
                    d += way[i].weight;
                }
                
                
                form.drawWay(way);
                MessageBox.Show(s + "   " + d.ToString());
        
            }
        }

        //public void setWay(string rez)
        //{
        //    form.getButton().Enabled = false;
        //    List<Edge> way = task.getWays();
        //    way.Reverse();
        //    way.AddRange(findWay(task.point2, rez, task.checked2));
        //    string s = "";
        //    for (int i = 0; i < way.Count; i++)
        //    {
        //        s = s + way[i].label1.ToString() + "-" + way[i].label2.ToString() + ";";
        //    }
        //    form.getTextBox().Text = s;
        //    MessageBox.Show(s);
        //    form.drawWay(way);
        //}
    //    public List<Edge> findWay(string point1, string rez, HashSet<string> checked1)
    //    {
    //        List<Edge> way = new List<Edge>();
    //        for (int i = 0; i < graph.findNeighbors(rez).Count; i++)
    //        {
    //            if (checked1.Contains(graph.findNeighbors(rez)[i]))
    //            {
    //                way.Add(new Edge(rez, graph.findNeighbors(rez)[i], graph.edgeExists(graph.findNeighbors(rez)[i], rez)));
    //                checked1.Remove(way.Last().label1);
    //                checked1.Remove(way.Last().label2);
    //                break;
    //            }
    //        }
    //        while (checked1.Contains(point1))
    //        {
    //            int j = 0;
    //            while(true)
    //            {
    //                if (graph.findNeighbors(way.Last().label2).Contains(point1))
    //                {

    //                    way.Add(new Edge(way.Last().label2, point1, graph.edgeExists(way.Last().label2, point1)));
    //                    checked1.Remove(way.Last().label1);
    //                    checked1.Remove(way.Last().label2);
    //                    break;
    //                }
    //                if (checked1.Contains(graph.findNeighbors(way.Last().label2)[j]))
    //                {
    //                    way.Add(new Edge(way.Last().label2, graph.findNeighbors(way.Last().label2)[j], graph.edgeExists(graph.findNeighbors(way.Last().label2)[j], way.Last().label1)));
    //                    checked1.Remove(way.Last().label1);
    //                    checked1.Remove(way.Last().label2);
    //                    j = 0;
    //                }
    //                else
    //                {
    //                    if (checked1.Contains(point1))
    //                    {
    //                        if (graph.findNeighbors(way.Last().label2).Count == j)
    //                        {
    //                            way.Remove(way.Last());
    //                            j = 0;
    //                        }
    //                        else j++;
    //                        //for (int i = 0; i < graph.findNeighbors(rez).Count; i++)
    //                        //{
    //                        //    if (checked1.Contains(graph.findNeighbors(rez)[i]))
    //                        //    {
    //                        //        way.Add(new Edge(graph.findNeighbors(rez)[i], rez, graph.edgeExists(graph.findNeighbors(rez)[i], rez)));
    //                        //        checked1.Remove(rez);
    //                        //        checked1.Remove(graph.findNeighbors(rez)[i]);
    //                        //        break;
    //                        //    }
    //                        //}
    //                    }
    //                }
    //            }
    //        }
    //        return way;
    //    }
    }

}

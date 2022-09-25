using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

using geneticInformationSystem.models.lexing;
using geneticInformationSystem.models.parsing;
using geneticInformationSystem.modules;

namespace giSystemUtilities {
    public class GenerateTree {

        public GViewer GenerateSampleTree() {
            GViewer viewer = new GViewer();
			//create a graph object
			Microsoft.Msagl.Drawing.Graph graph = new Graph("graph");
			//create the graph content
			graph.AddEdge("A", "B");
			graph.AddEdge("B", "C");
			graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
			graph.FindNode("A").Attr.Color = Microsoft.Msagl.Drawing.Color.Magenta;
			graph.FindNode("B").Attr.Color = Microsoft.Msagl.Drawing.Color.MistyRose;
			Node c =	graph.FindNode("C");
			c.Attr.Color = Microsoft.Msagl.Drawing.Color.PaleGreen;
			c.Attr.Shape =	Shape.Diamond;
			return viewer;
		}

		public Bitmap GenerateMsaglSampleTreeImage(int width, int height) {
			Graph graph = new Graph("Sample");

			graph.AddEdge("A", "B");
			graph.AddEdge("A", "B");
			graph.FindNode("A").Attr.Color =Microsoft.Msagl.Drawing.Color.Red;
			graph.FindNode("B").Attr.Color =Microsoft.Msagl.Drawing.Color.Blue;
			GraphRenderer renderer = new GraphRenderer(graph);

			renderer.CalculateLayout();
			Bitmap bitmap = new Bitmap(width, height);
			renderer.Render(bitmap);
			return bitmap;
		}


		public Bitmap GenerateMsaglTreeFromExpr(Expr root, String name, int w, int h) {
			Bitmap treePic = new Bitmap(w,h);
			Graph graph = new Graph(name);
			graph.Attr.LayerDirection = LayerDirection.BT;
			int i = -1;
			GenerateMsaglTree(root, null, graph,ref i);
			
			GraphRenderer renderer = new GraphRenderer(graph);
			
			renderer.CalculateLayout();
			renderer.Render(treePic);
			return treePic;
		}

		static void GenerateMsaglTree(Expr root, string parent, Graph graph, ref int i) {
			// Base case
			if (root == null) {
				return;
			}
			i++;
			string myname = i + ":" + root.value.ToString();
			// Process left child first
			GenerateMsaglTree(root.left, myname, graph, ref i);

			//current node..
			if (parent != null){
				graph.AddEdge(myname, parent);
			}
            else {
				graph.AddNode(myname);
            }
			Node mynode = graph.FindNode(myname);
			mynode.LabelText = root.value.ToString();

			// Process right child
			GenerateMsaglTree(root.right, myname, graph,ref i);
		}
	}
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeHelpers {
        private static readonly int nodeSize = 1;
        private static readonly float siblingDistance = 0.0F;
        private static readonly float treeDistance = 0.0F;
        internal static void InitializeNodes<T>(ITreeNode<T> node, int depth) {
            if (node != null) {
                node.SetX(0);
                node.SetY(depth);
                node.SetMod(0);

                InitializeNodes(node.GetLeftNode(), depth + 1);
                InitializeNodes(node.GetRightNode(), depth + 1);
            }
        }

        internal static void InitialAssignXMod<T>(ITreeNode<T> node) {
            //POST order traversal: L,R,root

            //null check
            if (node == null) {
                return;
            }

            //only check if L/R nodes exist
            if (node.GetLeftNode() != null) {
                InitialAssignXMod<T>(node.GetLeftNode());
            }
            if (node.GetRightNode() != null) {
                InitialAssignXMod<T>(node.GetRightNode());
            }

            //base conditions

            //is a leaf- has no children- leftnode=rightnode=null
            if (node.IsLeaf()) {
                //it is left node if it is actually left node OR it is the ONLY node!
                if (node.IsLeftNode()) {
                    node.SetX(0);
                }
                else {
                    //if not left node, x=leftnode.X+nodesize+siblingDistance
                    node.SetX(node.GetParentNode().GetLeftNode().GetX() + nodeSize + siblingDistance);
                }
            }
            else {
                //get the children of the node
                List<ITreeNode<T>> children = node.GetChildren();

                //node has only child.
                if (children.Count == 1) {
                    //is it leftNode?
                    if (node.IsLeftNode()) {
                        //It's X needs to equal that of its child
                        node.SetX(children[0].GetX());
                    }
                    else {
                        //it has siblings

                        //get sibling and calculate X and Mod
                        var siblingnode=node.GetParentNode().GetLeftNode();
                        node.SetX(siblingnode.GetX() + nodeSize + siblingDistance);
                        node.SetMod(node.GetX() - children[0].GetX());
                    }
                }
                else {
                    //get both children
                    var leftChild = node.GetLeftNode();
                    var rightChild = node.GetRightNode();

                    //caluclate the midpoint
                    var mid = (leftChild.GetX() + rightChild.GetX()) / 2;

                    if (node.IsLeftNode()) {
                        node.SetX(mid);
                    }
                    else {
                        var siblingnode = node.GetParentNode().GetLeftNode();
                        node.SetX(siblingnode.GetX() + nodeSize + siblingDistance);
                        node.SetMod(node.GetX() - mid);
                    }
                }

                if (children.Count > 0 && !node.IsLeftNode()) {
                    // Since subtrees can overlap, check for conflicts and shift tree right if needed
                    CheckForConflicts(node);
                }
            }
        }

        internal static void CheckForConflicts<T>(ITreeNode<T> node) {
            var minDistance = treeDistance + nodeSize;
            var shiftValue = 0F;

            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(node, 0, ref nodeContour);

            var sibling = node.GetParentNode().GetLeftNode();
            //while (sibling != null && sibling != node) {
                var siblingContour = new Dictionary<int, float>();
                GetRightContour(sibling, 0, ref siblingContour);

                //calculate max level between sibling and node contour
                int sibmax = 0;
                foreach(var k in siblingContour.Keys) {
                    if (k > sibmax) {
                        sibmax = k;
                    }
                }

                int nodemax = 0;
                foreach (var k in nodeContour.Keys) {
                    if (k > nodemax) {
                        nodemax = k;
                    }
                }

                //var nodeKeys = nodeContour.Keys;
                for (int level = node.GetY() + 1; level <= Math.Min(sibmax, nodemax); level++) {
                    var distance = nodeContour[level] - siblingContour[level];
                    if (distance + shiftValue < minDistance) {
                        shiftValue = minDistance - distance;
                    }
                }

                if (shiftValue > 0) {
                    node.SetX(node.GetX()+shiftValue);
                    node.SetMod(node.GetMod() + shiftValue);

                    //CenterNodesBetween(node, sibling);

                    shiftValue = 0;
                }
        }

        /*private static void CenterNodesBetween<T>(ITreeNode<T> leftNode, ITreeNode<T> rightNode) {
            var leftIndex = leftNode.Parent.Children.IndexOf(rightNode);
            var rightIndex = leftNode.Parent.Children.IndexOf(leftNode);

            var numNodesBetween = (rightIndex - leftIndex) - 1;

            if (numNodesBetween > 0) {
                var distanceBetweenNodes = (leftNode.X - rightNode.X) / (numNodesBetween + 1);

                int count = 1;
                for (int i = leftIndex + 1; i < rightIndex; i++) {
                    var middleNode = leftNode.Parent.Children[i];

                    var desiredX = rightNode.X + (distanceBetweenNodes * count);
                    var offset = desiredX - middleNode.X;
                    middleNode.X += offset;
                    middleNode.Mod += offset;

                    count++;
                }

                CheckForConflicts(leftNode);
            }
        }*/

        internal static void GetLeftContour<T>(ITreeNode<T> node, float modSum, ref Dictionary<int, float> values) {
            //check to see we haven't done this tree level by checking Y
            if (!values.ContainsKey(node.GetY())) {
                //initial entry of node level- (key=level number, value=node.x+modsum)
                values.Add(node.GetY(), node.GetX() + modSum);
            }
            else {
                //already in dictionary- set value to whichever is smaller- current X or an earlier calculated one
                //this gives us the current minimum X position
                values[node.GetY()] = Math.Min(values[node.GetY()], node.GetX() + modSum);
            }
            modSum += node.GetMod();
            foreach (var child in node.GetChildren()) {
                if (child != null) {
                    GetLeftContour(child, modSum, ref values);
                }
            }
        }
        internal static void GetRightContour<T>(ITreeNode<T> node, float modSum, ref Dictionary<int, float> values) {
            if (!values.ContainsKey(node.GetY()))
                values.Add(node.GetY(), node.GetX() + modSum);
            else
                values[node.GetY()] = Math.Max(values[node.GetY()], node.GetX() + modSum);

            modSum += node.GetMod();
            foreach (var child in node.GetChildren()) {
                if (child != null) {
                    GetRightContour(child, modSum, ref values);
                }
            }
        }

        internal static void AllOnscreenCheckAdjustment<T>(ITreeNode<T> node) {
            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(node, 0, ref nodeContour);

            float shiftAmount = 0;
            foreach (var y in nodeContour.Keys) {
                if (nodeContour[y] + shiftAmount < 0)
                    shiftAmount = (nodeContour[y] * -1);
            }

            if (shiftAmount > 0) {
                node.SetX(node.GetX() + shiftAmount);
                node.SetMod(node.GetMod() + shiftAmount);
            }
        }

        internal static void CalculateFinalPositions<T>(ITreeNode<T> node, float modSum) {
            node.SetX(node.GetX() + modSum);
            modSum += node.GetMod();

            foreach (var child in node.GetChildren())
                if (child != null) {
                    CalculateFinalPositions(child, modSum);
                }

            if (node.GetChildren().Count == 0) {
                node.SetWidth(node.GetX());
                node.SetHeight(node.GetY());
            }
            else {
                float maxw = 0;
                float maxh = 0;
                foreach(var child in node.GetChildren()) {
                    if (child != null) {
                        if(child.GetWidth()> maxw) {
                            maxw = child.GetWidth();
                        }
                        if (child.GetHeight() > maxh) {
                            maxh = child.GetHeight();
                        };
                    }
                    else {
                        break;
                    }
                    node.SetWidth(maxw);
                    node.SetHeight(maxh);
                }
            }
        }
    }
}

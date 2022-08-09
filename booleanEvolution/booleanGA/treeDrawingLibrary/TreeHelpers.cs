using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeHelpers {
        private static int nodeSize = 1;
        private static float siblingDistance = 0.0F;
        private static float treeDistance = 0.0F;
        internal static void InitializeNodes<T>(ITreeNode<T> node, int depth) {
            if (node != null) {
                node.setX(0);
                node.setY(depth);
                node.setMod(0);

                InitializeNodes(node.getLeftNode(), depth + 1);
                InitializeNodes(node.getRightNode(), depth + 1);
            }
        }

        internal static void initialAssignXMod<T>(ITreeNode<T> node) {
            //POST order traversal: L,R,root

            //null check
            if (node == null) {
                return;
            }

            //only check if L/R nodes exist
            if (node.getLeftNode() != null) {
                initialAssignXMod<T>(node.getLeftNode());
            }
            if (node.getRightNode() != null) {
                initialAssignXMod<T>(node.getRightNode());
            }

            //base conditions

            //is a leaf- has no children- leftnode=rightnode=null
            if (node.isLeaf()) {
                //it is left node if it is actually left node OR it is the ONLY node!
                if (node.isLeftNode()) {
                    node.setX(0);
                }
                else {
                    //if not left node, x=leftnode.X+nodesize+siblingDistance
                    node.setX(node.getParentNode().getLeftNode().getX() + nodeSize + siblingDistance);
                }
            }
            else {
                //get the children of the node
                List<ITreeNode<T>> children = node.getChildren();

                //node has only child.
                if (children.Count == 1) {
                    //is it leftNode?
                    if (node.isLeftNode()) {
                        //It's X needs to equal that of its child
                        node.setX(children[0].getX());
                    }
                    else {
                        //it has siblings

                        //get sibling and calculate X and Mod
                        var siblingnode=node.getParentNode().getLeftNode();
                        node.setX(siblingnode.getX() + nodeSize + siblingDistance);
                        node.setMod(node.getX() - children[0].getX());
                    }
                }
                else {
                    //get both children
                    var leftChild = node.getLeftNode();
                    var rightChild = node.getRightNode();

                    //caluclate the midpoint
                    var mid = (leftChild.getX() + rightChild.getX()) / 2;

                    if (node.isLeftNode()) {
                        node.setX(mid);
                    }
                    else {
                        var siblingnode = node.getParentNode().getLeftNode();
                        node.setX(siblingnode.getX() + nodeSize + siblingDistance);
                        node.setMod(node.getX() - mid);
                    }
                }

                if (children.Count > 0 && !node.isLeftNode()) {
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

            var sibling = node.getParentNode().getLeftNode();
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

                var nodeKeys = nodeContour.Keys;
                for (int level = node.getY() + 1; level <= Math.Min(sibmax, nodemax); level++) {
                    var distance = nodeContour[level] - siblingContour[level];
                    if (distance + shiftValue < minDistance) {
                        shiftValue = minDistance - distance;
                    }
                }

                if (shiftValue > 0) {
                    node.setX(node.getX()+shiftValue);
                    node.setMod(node.getMod() + shiftValue);

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
            if (!values.ContainsKey(node.getY())) {
                //initial entry of node level- (key=level number, value=node.x+modsum)
                values.Add(node.getY(), node.getX() + modSum);
            }
            else {
                //already in dictionary- set value to whichever is smaller- current X or an earlier calculated one
                //this gives us the current minimum X position
                values[node.getY()] = Math.Min(values[node.getY()], node.getX() + modSum);
            }
            modSum += node.getMod();
            foreach (var child in node.getChildren()) {
                if (child != null) {
                    GetLeftContour(child, modSum, ref values);
                }
            }
        }
        internal static void GetRightContour<T>(ITreeNode<T> node, float modSum, ref Dictionary<int, float> values) {
            if (!values.ContainsKey(node.getY()))
                values.Add(node.getY(), node.getX() + modSum);
            else
                values[node.getY()] = Math.Max(values[node.getY()], node.getX() + modSum);

            modSum += node.getMod();
            foreach (var child in node.getChildren()) {
                if (child != null) {
                    GetRightContour(child, modSum, ref values);
                }
            }
        }

        internal static void allOnscreenCheckAdjustment<T>(ITreeNode<T> node) {
            var nodeContour = new Dictionary<int, float>();
            GetLeftContour(node, 0, ref nodeContour);

            float shiftAmount = 0;
            foreach (var y in nodeContour.Keys) {
                if (nodeContour[y] + shiftAmount < 0)
                    shiftAmount = (nodeContour[y] * -1);
            }

            if (shiftAmount > 0) {
                node.setX(node.getX() + shiftAmount);
                node.setMod(node.getMod() + shiftAmount);
            }
        }

        internal static void CalculateFinalPositions<T>(ITreeNode<T> node, float modSum) {
            node.setX(node.getX() + modSum);
            modSum += node.getMod();

            foreach (var child in node.getChildren())
                if (child != null) {
                    CalculateFinalPositions(child, modSum);
                }

            if (node.getChildren().Count == 0) {
                node.setWidth(node.getX());
                node.setHeight(node.getY());
            }
            else {
                float maxw = 0;
                float maxh = 0;
                foreach(var child in node.getChildren()) {
                    if (child != null) {
                        if(child.getWidth()> maxw) {
                            maxw = child.getWidth();
                        }
                        if (child.getHeight() > maxh) {
                            maxh = child.getHeight();
                        };
                    }
                    else {
                        break;
                    }
                    node.setWidth(maxw);
                    node.setHeight(maxh);
                }
            }
        }
    }
}

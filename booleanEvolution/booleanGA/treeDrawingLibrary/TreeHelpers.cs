using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeHelpers {
        private static int nodeSize = 1;
        private static float siblingDistance = 0.0F;
        private static float treeDistance = 0.0F;
        internal static void InitializeNodes<T>(ITreeNode<T> node, int depth) {
            node.setX(1);
            node.setY(depth);
            node.setMod(0);

            InitializeNodes(node.getLeftNode(), depth + 1);
            InitializeNodes(node.getRightNode(), depth + 1);
        }

        internal static void initialAssignX<T>(ITreeNode<T> node) {
            //POST order traversal: L,R,root

            //null check
            if (node == null) {
                return;
            }

            //only check if L/R nodes exist
            if (node.getLeftNode() != null) {
                initialAssignX<T>(node.getLeftNode());
            }
            if (node.getRightNode() != null) {
                initialAssignX<T>(node.getRightNode());
            }

            //base conditions

            
            if (node.isLeftNode()) {
                node.setX(0);
            }
            else {
                //if not left node, x=leftnode.X+nodesize+1
                node.setX(node.getParentNode().getLeftNode().getX()+nodeSize+1);
            }

        }

        private static void onscreenCheckAdjustment<T>(ITreeNode<T> node) {
        }

    }
}

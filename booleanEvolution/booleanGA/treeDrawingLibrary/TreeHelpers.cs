using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeHelpers {
        private static void InitializeNodes<T>(ITreeNode<T> node, int depth) {
            node.setX(1);
            node.setY(depth);
            node.setMod(0);

            InitializeNodes(node.getLeftNode(), depth + 1);
            InitializeNodes(node.getRightNode(), depth + 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class treeGenerator {

        public void InitializeNodes<T>(ITreeNode<T> node) {
            TreeHelpers.InitializeNodes(node, 0);
            TreeHelpers.initialAssignX(node);
        }
    }
}

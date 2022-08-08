using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.parsing;
using treeDrawingLibrary;

namespace giSystemUtilities {
    public class convertExprToTreeNode {

        public convertExprToTreeNode() {

        }

        public static treeNode<string> convert(Expr exprroot, treeNode<string> root) {
            // Base case
            if (exprroot == null) {
                return null;
            }

            treeNode<string> node = new treeNode<string>();

            node.setValue(exprroot.value.ToString());
            node.setParentNode(root);

            treeNode<string> left = convert(exprroot.left, node);
            treeNode<string> right = convert(exprroot.right, node);
            node.setLeftNode(left);
            node.setRightNode(right);

            

            return node;

        }
    }
}

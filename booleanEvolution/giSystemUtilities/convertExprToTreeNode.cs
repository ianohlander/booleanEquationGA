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

        public static TreeNode<string> convert(Expr exprroot, TreeNode<string> root) {
            // Base case
            if (exprroot == null) {
                return null;
            }

            TreeNode<string> node = new TreeNode<string>();

            node.SetValue(exprroot.value.ToString());
            node.SetParentNode(root);
            node.SetType(exprroot.op.type.ToString());

            TreeNode<string> left = convert(exprroot.left, node);
            TreeNode<string> right = convert(exprroot.right, node);
            node.SetLeftNode(left);
            node.SetRightNode(right);

            

            return node;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using geneticInformationSystem.models.parsing;

namespace geneticInformationSystem.modules {
	public class ASTDisplay {
		static readonly int COUNT = 10;

		// Function to print binary tree in 2D
		// It does reverse inorder traversal
		static void print2DUtil(Expr root, int space, StringBuilder builder) {
			// Base case
			if (root == null)
				return;

			// Increase distance between levels
			space += COUNT;

			// Process right child first
			print2DUtil(root.right, space, builder);

			// Print current node after space count
			builder.Append("\n");

			for (int i = COUNT; i < space; i++) {
				builder.Append(" ");
			}
			builder.Append(root.value + "\n");

			// Process left child
			print2DUtil(root.left, space, builder);
		}

		static void print2DUtilTB(Expr root, int space, StringBuilder builder) {
			string nl=System.Environment.NewLine;
			// Base case
			if (root == null)
				return;

			// Increase distance between levels
			space += COUNT;

			// Process right child first
			print2DUtilTB(root.right, space, builder);

			// Print current node after space
			// count


			builder.Append(nl);
			//Console.Write("\n");
			for (int i = COUNT; i < space; i++) {
				//Console.Write(" ");
				builder.Append(" ");
			}
			builder.Append(root.value + nl);
			//Console.Write(root.value + "\n");

			// Process left child
			print2DUtilTB(root.left, space, builder);
		}

		// Wrapper over print2DUtil()
		public static string print2D(Expr root, bool textbox) {
			// Pass initial space count as 0
			if (textbox) {
				StringBuilder builder = new StringBuilder();
				print2DUtilTB(root, 0, builder);
				return builder.ToString();
			}
            else {
				StringBuilder builder = new StringBuilder();
				print2DUtil(root, 0, builder);
				return builder.ToString();
			}
		}

	}
}


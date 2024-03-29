﻿using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeGenerator {

        public void InitializeNodes<T>(ITreeNode<T> node) {
            TreeHelpers.InitializeNodes(node, 0);
            TreeHelpers.InitialAssignXMod(node);
            // ensure no node is being drawn off screen
            TreeHelpers.AllOnscreenCheckAdjustment(node);

            // assign final X values to nodes
            TreeHelpers.CalculateFinalPositions(node, 0);
        }
    }
}

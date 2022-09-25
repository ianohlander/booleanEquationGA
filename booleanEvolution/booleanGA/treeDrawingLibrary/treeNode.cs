using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class TreeNode<T> : ITreeNode<T> {
        private ITreeNode<T> leftNode;
        private ITreeNode<T> rightNode;
        private ITreeNode<T> parentNode;
        private float mod;
        private float x;
        private int y;
        private string type;
        private string label;
        private T value;
        private float width;
        private float height;

        public TreeNode(TreeNode<T> left, TreeNode<T> right, TreeNode<T> parent, T value) {
            this.leftNode = left;
            this.rightNode = right;
            this.parentNode = parent;
            this.value = value;
        }

        public TreeNode() {

        }

        public void SetValue(T _value) {
            value = _value;
        }
        public T GetValue() {
            return value;
        }

        public ITreeNode<T> GetLeftNode() {
            if (leftNode == null) {
                return rightNode;
            }
            else {
                return leftNode;
            }
        }

        public float GetMod() {
            return mod;
        }

        public ITreeNode<T> GetParentNode() {
            return parentNode;
        }

        public ITreeNode<T> GetRightNode() {
            return rightNode;
        }

        public new string GetType() {
            return type;
        }

        public float GetX() {
            return x;
        }

        public int GetY() {
            return y;
        }

        public bool IsLeaf() {
            if (rightNode == null && leftNode == null) {
                return true;
            }
            return false;
        }

        public bool IsLeftNode() {
            //if this is root (no parent)
            //OR there is no left node while this is right node
            //OR this IS the left node
            //treat as left node
            if (parentNode == null || parentNode.GetLeftNode()==null || this == parentNode.GetLeftNode()) {
                return true;
            }
            return false;
        }


        public void SetLeftNode(ITreeNode<T> _leftNode) {
            leftNode = _leftNode;
        }

        public void SetMod(float _mod) {
            mod = _mod;
        }

        public void SetParentNode(ITreeNode<T> _parentNode) {
            parentNode = _parentNode;
        }

        public void SetRightNode(ITreeNode<T> _rightNode) {
            rightNode = _rightNode;
        }

        public void SetType(string _type) {
            type = _type;
        }

        public void SetX(float _x) {
            x = _x;
        }

        public void SetY(int _y) {
            y = _y;
        }

        public void SetLabelText(string _label) {
            label = _label;
        }

        public string GetLabelText() {
            return label;
        }

        public int GetNumberOfChildren() {
            int children = 0;
            if (leftNode != null) {
                children++;
            }
            if (rightNode != null) {
                children++;
            }
            return children;
        }

        public List<ITreeNode<T>> GetChildren() {
            List<ITreeNode<T>> children = new List<ITreeNode<T>> {
                leftNode,
                rightNode
            };
            return children;
        }

        public float GetWidth() {
            return width;
        }

        public void SetWidth(float _width) {
            width=_width;
        }

        public float GetHeight() {
            return height;
        }

        public void SetHeight(float _height) {
            height=_height;
        }
    }
}

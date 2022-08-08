using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public class treeNode<T> : ITreeNode<T> {
        private ITreeNode<T> leftNode;
        private ITreeNode<T> rightNode;
        private ITreeNode<T> parentNode;
        private float mod;
        private float x;
        private int y;
        private string type;
        private string label;
        private T value;

        public treeNode(treeNode<T> left, treeNode<T> right, treeNode<T> parent) {
            this.leftNode = left;
            this.rightNode = right;
            this.parentNode = parent;
        }

        public void setValue(T _value) {
            value = _value;
        }
        public T getValue() {
            return value;
        }

        public ITreeNode<T> getLeftNode() {
            return leftNode;
        }

        public float getMod() {
            return mod;
        }

        public ITreeNode<T> getParentNode() {
            return parentNode;
        }

        public ITreeNode<T> getRightNode() {
            return rightNode;
        }

        public string getType() {
            return type;
        }

        public float getX() {
            return x;
        }

        public int getY() {
            return y;
        }

        public bool isLeaf() {
            if (rightNode == null && leftNode == null) {
                return true;
            }
            return false;
        }

        public bool isLeftNode() {
            if (this == parentNode.getLeftNode()) {
                return true;
            }
            return false;
        }


        public void setLeftNode(ITreeNode<T> _leftNode) {
            leftNode = _leftNode;
        }

        public void setMod(float _mod) {
            mod = _mod;
        }

        public void setParentNode(ITreeNode<T> _parentNode) {
            parentNode = _parentNode;
        }

        public void setRightNode(ITreeNode<T> _rightNode) {
            rightNode = _rightNode;
        }

        public void setType(string _type) {
            type = _type;
        }

        public void setX(float _x) {
            x = _x;
        }

        public void setY(int _y) {
            y = _y;
        }

        public void setLabelText(string _label) {
            label = _label;
        }

        public string getLabelText() {
            return label;
        }
    }
}

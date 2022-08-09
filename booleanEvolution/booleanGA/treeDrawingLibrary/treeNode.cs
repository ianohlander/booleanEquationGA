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
        private float width;
        private float height;

        public treeNode(treeNode<T> left, treeNode<T> right, treeNode<T> parent, T value) {
            this.leftNode = left;
            this.rightNode = right;
            this.parentNode = parent;
            this.value = value;
        }

        public treeNode() {

        }

        public void setValue(T _value) {
            value = _value;
        }
        public T getValue() {
            return value;
        }

        public ITreeNode<T> getLeftNode() {
            if (leftNode == null) {
                return rightNode;
            }
            else {
                return leftNode;
            }
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
            //if this is root (no parent)
            //OR there is no left node while this is right node
            //OR this IS the left node
            //treat as left node
            if (parentNode == null || parentNode.getLeftNode()==null || this == parentNode.getLeftNode()) {
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

        public int getNumberOfChildren() {
            int children = 0;
            if (leftNode != null) {
                children++;
            }
            if (rightNode != null) {
                children++;
            }
            return children;
        }

        public List<ITreeNode<T>> getChildren() {
            List<ITreeNode<T>> children = new List<ITreeNode<T>>();
            children.Add(leftNode);
            children.Add(rightNode); 
            return children;
        }

        public float getWidth() {
            return width;
        }

        public void setWidth(float _width) {
            width=_width;
        }

        public float getHeight() {
            return height;
        }

        public void setHeight(float _height) {
            height=_height;
        }
    }
}

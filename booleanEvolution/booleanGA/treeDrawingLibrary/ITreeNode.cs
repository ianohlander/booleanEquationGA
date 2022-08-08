using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public interface ITreeNode<T> {
        bool isLeaf();
        bool isLeftNode();
        ITreeNode<T> getRightNode();
        ITreeNode<T> getLeftNode();
        ITreeNode<T> getParentNode();
        void setParentNode(ITreeNode<T> parentNode);
        void setLeftNode(ITreeNode<T> leftNode);  
        void setRightNode(ITreeNode<T> rightNode); 
        void setValue(T value);
        T getValue();
        float getX();
        void setX(float x);   
        int getY();     
        void setY(int y);
        float getMod();
        void setMod(float mod);  

        void setType(string type);
        string getType();  

        void setLabelText(string label);

        string getLabelText();

    }
}

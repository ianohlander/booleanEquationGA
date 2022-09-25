using System;
using System.Collections.Generic;
using System.Text;

namespace treeDrawingLibrary {
    public interface ITreeNode<T> {
        bool IsLeaf();
        bool IsLeftNode();
        ITreeNode<T> GetRightNode();
        ITreeNode<T> GetLeftNode();
        ITreeNode<T> GetParentNode();
        void SetParentNode(ITreeNode<T> parentNode);
        void SetLeftNode(ITreeNode<T> leftNode);  
        void SetRightNode(ITreeNode<T> rightNode); 
        void SetValue(T value);
        T GetValue();
        float GetX();
        void SetX(float x);   
        int GetY();     
        void SetY(int y);
        float GetMod();
        void SetMod(float mod);  

        void SetType(string type);
        string GetType();  

        void SetLabelText(string label);

        string GetLabelText();

        List<ITreeNode<T>> GetChildren();

        float GetWidth();
        void SetWidth(float width);

        float GetHeight();
        void SetHeight(float height);
    }
}

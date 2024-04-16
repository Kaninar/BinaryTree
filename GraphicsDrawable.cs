using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Cours_Task5_Tree
{
    internal class GraphicsDrawable : IDrawable
    {
        private float _Off;
        private float _OneLevelHeight;
        private float _NodeRadius;
        private (float X, float Y) _Center;
        private float MinimalRadius = 10;
        private float MaximalRadius = 50;

        private Color _StrokeColor = Colors.White;
        private float _StrokeSize = 2;
        private Color _FillColor = Colors.Black;
        private Color _FontColor = Colors.White;

        BinaryTree _Tree;
        public GraphicsDrawable(BinaryTree Tree)
        { 
            _Tree = Tree;
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _Center = (dirtyRect.Width / 2, dirtyRect.Height);
            _NodeRadius = (dirtyRect.Width / ((float)Math.Pow(2, _Tree.Height - 1) + 1) - 2) / 3;
            
            if (_NodeRadius < MinimalRadius)
            { 
                _NodeRadius = MinimalRadius;
            }
            else if (_NodeRadius > MaximalRadius)
            {
                _NodeRadius = MaximalRadius;
            }

            _Off = _NodeRadius;            
            _OneLevelHeight = ((dirtyRect.Height - _Off * 2) / _Tree.Height - 1);

            canvas.StrokeColor = _StrokeColor;
            canvas.StrokeSize = _StrokeSize;
            canvas.FillColor = _FillColor;
            canvas.FontColor = _FontColor;

            RecursiveDraw(canvas, (_Center.X, _Off), dirtyRect.Height / 2 - _Off, _Tree._MainRoot);
        }
        private void RecursiveDraw(ICanvas canvas, (float X, float Y) RootNodeCenter, float HeapWidth, BinaryTreeNode Root)
        {
            if (Root == null) 
            { 
                return; 
            }

            BinaryTreeNode Left = Root.LeftChildNode;
            BinaryTreeNode Right = Root.RightChildNode;

            if (Root.LeftChildNode != null)
            {
                float LeftChildCenterX = RootNodeCenter.X - HeapWidth / 2;
                float LeftChildCenterY = RootNodeCenter.Y + _OneLevelHeight;

                (float X, float Y) LeftChildCenter = (LeftChildCenterX, LeftChildCenterY);
                
                canvas.DrawLine(RootNodeCenter.X, RootNodeCenter.Y, LeftChildCenterX, LeftChildCenterY);  
                RecursiveDraw(canvas, LeftChildCenter, HeapWidth / 2, Left);
            }

            if (Root.RightChildNode != null)
            {
                float RightChildX = RootNodeCenter.X + HeapWidth / 2;
                float RightChildY = RootNodeCenter.Y + _OneLevelHeight;

                (float, float) RightChildCords = (RightChildX, RightChildY);

                canvas.DrawLine(RootNodeCenter.X, RootNodeCenter.Y, RightChildX, RightChildY);
                RecursiveDraw(canvas, RightChildCords, HeapWidth / 2, Right);
            }

            canvas.FillCircle(RootNodeCenter.X, RootNodeCenter.Y, _NodeRadius);

            if (Left != null && Right != null)
            {
                canvas.StrokeColor = Colors.Orange;
            }

            canvas.DrawCircle(RootNodeCenter.X, RootNodeCenter.Y, _NodeRadius);
            canvas.StrokeColor = _StrokeColor;
            canvas.DrawString(Root.Value.ToString(), RootNodeCenter.X-_NodeRadius, RootNodeCenter.Y-_NodeRadius, _NodeRadius*2, _NodeRadius*2 ,HorizontalAlignment.Center, VerticalAlignment.Center);
        }
    }
}

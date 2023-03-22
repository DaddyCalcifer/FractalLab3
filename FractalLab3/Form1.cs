using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FractalLab3
{
    public partial class Form1 : Form
    {
        int MaxDepth = 6;

        readonly Pen _pen = new Pen(Color.Black, 2);
        readonly List<Point> _points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            Text = "Двоичное дерево";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void DrawFractal(Graphics g)
        {
            _points.Clear();

            var root = new TreeNode(ClientSize.Width / 2, ClientSize.Height - 100, 150);

            DrawBranch(g, root, 0);

            for (var i = 0; i < _points.Count - 1; i+=4)
            {
                g.DrawLine(_pen, _points[i], _points[i + 1]);
                g.DrawLine(_pen, _points[i+2], _points[i + 3]);
            }
        }

        private void DrawBranch(Graphics g, TreeNode node, int depth)
        {
            if (depth > MaxDepth)
            {
                return;
            }

            var p1 = new Point(node.X, node.Y);
            var p2 = new Point(Convert.ToInt32(node.X), Convert.ToInt32(node.Y - node.Lenght));

            var p_left = new Point(Convert.ToInt32(node.X - node.Lenght), Convert.ToInt32(node.Y - node.Lenght));
            var p_right = new Point(Convert.ToInt32(node.X + node.Lenght), Convert.ToInt32(node.Y - node.Lenght));

            _points.Add(p1); _points.Add(p2);
            _points.Add(p_left); _points.Add(p_right);

            var left = new TreeNode(p_left.X, p_left.Y, node.Lenght / 2);
            var right = new TreeNode(p_right.X, p_right.Y, node.Lenght / 2);

            DrawBranch(g, left, depth + 1);
            DrawBranch(g, right, depth + 1);
        }

        private class TreeNode
        {
            public TreeNode(int x, int y, int lenght)
            {
                X = x;
                Y = y;
                Lenght = lenght;
            }

            public int X { get; }
            public int Y { get; }
            public int Lenght { get; }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            MaxDepth = (int)numericUpDown1.Value;
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);
            DrawFractal(g);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}

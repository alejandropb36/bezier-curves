using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bezier_curves
{
    public partial class MainForm : Form
    {
        int counter;
        Point[] points;
        Point l1, l2, l3;
        Point l4, l5, l6;
        public MainForm()
        {
            counter = 0;
            points = new Point[3];
            l1 = new Point();
            l2 = new Point();
            l3 = new Point();
            l4 = new Point();
            l5 = new Point();
            l6 = new Point();
            InitializeComponent();
        }

        private void WorkSpace_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}

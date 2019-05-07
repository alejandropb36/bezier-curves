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
        int contador;
        Point[] puntos;
        Point l1, l2, l3;
        Point l4, l5, l6;
        Point l7;
        Pen pen;

        public MainForm()
        {
            contador = 0;
            puntos = new Point[3];
            l1 = new Point();
            l2 = new Point();
            l3 = new Point();
            l4 = new Point();
            l5 = new Point();
            l6 = new Point();
            pen = new Pen(Color.Black);
            InitializeComponent();
        }

        Pen GetPen(Color color)
        {
            pen.Color = color;
            return pen;
        }

        int DistanciaMayor(Point[] puntos, int n)
        {
            int resultado;
            resultado = 0;

            resultado = (int)(Math.Sqrt(Math.Pow((puntos[n - 1].X - puntos[0].X), 2)
                + Math.Pow((puntos[n - 1].Y - puntos[0].Y), 2)));

            return resultado;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            puntos = new Point[Convert.ToInt32(comboBox1.Text)];
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            WorkSpace.Refresh();
        }

        private void WorkSpace_MouseClick(object sender, MouseEventArgs e)
        {
            puntos[contador] = new Point(e.X, e.Y);
            contador++;
            WorkSpace.CreateGraphics().DrawEllipse(GetPen(Color.Black), e.X, e.Y, 3, 3);
            if (contador == Convert.ToInt32(comboBox1.Text))
            {
                DibujarLineas(puntos, contador - 1);
                contador = 0;
            }
        }

        void DibujarLineas(Point[] puntos, int numeroPuntos)
        {
            WorkSpace.CreateGraphics().Clear(Color.White);
            if (Convert.ToInt32(comboBox1.Text) == 3)
            {
                CalcularCurvasGrado2(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
            else if (Convert.ToInt32(comboBox1.Text) == 4)
            {
                CalcularCurvasGrado3(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
        }

        Point GetPuntoCurva(Point[] conjuntoPuntos, double t, int np)
        {
            double ax, bx, cx;
            double ay, by, cy;
            Double tSquared, tCubed;
            Point result = new Point();

            cx = 3.0 * (conjuntoPuntos[1].X - conjuntoPuntos[0].X);
            bx = 3.0 * (conjuntoPuntos[2].X - conjuntoPuntos[1].X) - cx;
            ax = conjuntoPuntos[np].X - conjuntoPuntos[0].X - cx - bx;

            cy = 3.0 * (conjuntoPuntos[1].Y - conjuntoPuntos[0].Y);
            by = 3.0 * (conjuntoPuntos[2].Y - conjuntoPuntos[1].Y) - cy;
            ay = conjuntoPuntos[np].Y - conjuntoPuntos[0].Y - cy - by;

            tSquared = t * t;
            tCubed = tSquared * t;

            result.X = Convert.ToInt32((ax * tCubed) + (bx * tSquared) + (cx * t) + conjuntoPuntos[0].X);
            result.Y = Convert.ToInt32((ay * tCubed) + (by * tSquared) + (cy * t) + conjuntoPuntos[0].Y);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Yellow), (float)(conjuntoPuntos[0].X + (ax * tCubed)), (float)(conjuntoPuntos[0].Y + (ay * tCubed)), (float)(conjuntoPuntos[2].X), (float)(conjuntoPuntos[2].Y));

            return result;
        }

        void CalcularCurvasGrado2(Point[] conjuntoPuntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float t;
            t = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                curva[i] = GetPuntoCurvaGrado2(conjuntoPuntos, i * t);
                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), puntos);
                DibujarCurvaActual(curva, i);
            }
        }

        Point GetPuntoCurvaGrado2(Point[] conjuntoPuntos, double t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2;

            rx1 = (int)((conjuntoPuntos[1].X - conjuntoPuntos[0].X) * t) + conjuntoPuntos[0].X;
            ry1 = (int)((conjuntoPuntos[1].Y - conjuntoPuntos[0].Y) * t) + conjuntoPuntos[0].Y;
            rx2 = (int)((conjuntoPuntos[2].X - conjuntoPuntos[1].X) * t) + conjuntoPuntos[1].X;
            ry2 = (int)((conjuntoPuntos[2].Y - conjuntoPuntos[1].Y) * t) + conjuntoPuntos[1].Y;
            resultado.X = (int)Math.Round(((rx2 - rx1) * t) + rx1);
            resultado.Y = (int)Math.Round(((ry2 - ry1) * t) + ry1);

            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Red), rx1, ry1, rx2, ry2);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            return resultado;
        }

        void DibujarCurvaActual(Point[] curva, int n)
        {
            for (int i = 0; i < n; i++)
            {
                WorkSpace.CreateGraphics().DrawRectangle(GetPen(Color.Blue), curva[i].X, curva[i].Y, 1, 1);
            }
        }

        void CalcularCurvasGrado3(Point[] conjuntoPuntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float t;
            t = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l4.X, l4.Y, l5.X, l5.Y);
                curva[i] = GetPuntoCurvaGrado3(conjuntoPuntos, i * t);

                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), puntos);
                DibujarCurvaActual(curva, i);

            }
        }

        Point GetPuntoCurvaGrado3(Point[] conjuntoPuntos, double t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5;

            rx1 = (int)((conjuntoPuntos[1].X - conjuntoPuntos[0].X) * t) + conjuntoPuntos[0].X;
            ry1 = (int)((conjuntoPuntos[1].Y - conjuntoPuntos[0].Y) * t) + conjuntoPuntos[0].Y;
            rx2 = (int)((conjuntoPuntos[2].X - conjuntoPuntos[1].X) * t) + conjuntoPuntos[1].X;
            ry2 = (int)((conjuntoPuntos[2].Y - conjuntoPuntos[1].Y) * t) + conjuntoPuntos[1].Y;
            rx3 = (int)((conjuntoPuntos[3].X - conjuntoPuntos[2].X) * t) + conjuntoPuntos[2].X;
            ry3 = (int)((conjuntoPuntos[3].Y - conjuntoPuntos[2].Y) * t) + conjuntoPuntos[2].Y;
            rx4 = (int)((rx2 - rx1) * t) + rx1;
            ry4 = (int)((ry2 - ry1) * t) + ry1;
            rx5 = (int)((rx3 - rx2) * t) + rx2;
            ry5 = (int)((ry3 - ry2) * t) + ry2;

            resultado.X = (int)Math.Round(((rx5 - rx4) * t) + rx4);
            resultado.Y = (int)Math.Round(((ry5 - ry4) * t) + ry4);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), rx1, ry1, rx2, ry2);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), rx2, ry2, rx3, ry3);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Blue), rx4, ry4, rx5, ry5);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            l3 = new Point(rx3, ry3);
            l4 = new Point(rx4, ry4);
            l5 = new Point(rx5, ry5);

            return resultado;
        }

        void CalcularCurvasGrado4(Point[] conjuntoPuntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float t;
            t = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l1.X, l1.Y, l2.X, l2.Y);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l2.X, l2.Y, l3.X, l3.Y);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), l4.X, l4.Y, l5.X, l5.Y);
                curva[i] = GetPuntoCurvaGrado4(conjuntoPuntos, i * t);

                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), puntos);
                DibujarCurvaActual(curva, i);

            }
        }

        Point GetPuntoCurvaGrado4(Point[] conjuntoPuntos, double t)
        {
            Point resultado = new Point();
            int rx1, ry1, rx2, ry2, rx3, ry3, rx4, ry4, rx5, ry5;

            rx1 = (int)((conjuntoPuntos[1].X - conjuntoPuntos[0].X) * t) + conjuntoPuntos[0].X;
            ry1 = (int)((conjuntoPuntos[1].Y - conjuntoPuntos[0].Y) * t) + conjuntoPuntos[0].Y;
            rx2 = (int)((conjuntoPuntos[2].X - conjuntoPuntos[1].X) * t) + conjuntoPuntos[1].X;
            ry2 = (int)((conjuntoPuntos[2].Y - conjuntoPuntos[1].Y) * t) + conjuntoPuntos[1].Y;
            rx3 = (int)((conjuntoPuntos[3].X - conjuntoPuntos[2].X) * t) + conjuntoPuntos[2].X;
            ry3 = (int)((conjuntoPuntos[3].Y - conjuntoPuntos[2].Y) * t) + conjuntoPuntos[2].Y;
            rx4 = (int)((rx2 - rx1) * t) + rx1;
            ry4 = (int)((ry2 - ry1) * t) + ry1;
            rx5 = (int)((rx3 - rx2) * t) + rx2;
            ry5 = (int)((ry3 - ry2) * t) + ry2;

            resultado.X = (int)Math.Round(((rx5 - rx4) * t) + rx4);
            resultado.Y = (int)Math.Round(((ry5 - ry4) * t) + ry4);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), rx1, ry1, rx2, ry2);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), rx2, ry2, rx3, ry3);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Blue), rx4, ry4, rx5, ry5);
            l1 = new Point(rx1, ry1);
            l2 = new Point(rx2, ry2);
            l3 = new Point(rx3, ry3);
            l4 = new Point(rx4, ry4);
            l5 = new Point(rx5, ry5);

            return resultado;
        }

    }
}

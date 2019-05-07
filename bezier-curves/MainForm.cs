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
        Point[] points;
        Point punto1, punto2, punto3;
        Point punto4, punto5, punto6;
        Point punto7, punto8, punto9;
        Pen pen;

        public MainForm()
        {
            contador = 0;
            points = new Point[3];
            punto1 = new Point();
            punto2 = new Point();
            punto3 = new Point();
            punto4 = new Point();
            punto5 = new Point();
            punto6 = new Point();
            punto7 = new Point();
            punto8 = new Point();
            punto9 = new Point();
            punto9 = new Point();
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
            points = new Point[Convert.ToInt32(comboBox1.Text)];
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            WorkSpace.Refresh();
        }

        private void WorkSpace_MouseClick(object sender, MouseEventArgs e)
        {
            points[contador] = new Point(e.X, e.Y);
            contador++;
            WorkSpace.CreateGraphics().DrawEllipse(GetPen(Color.Black), e.X, e.Y, 3, 3);
            if (contador == Convert.ToInt32(comboBox1.Text))
            {
                DibujarLineas(points, contador - 1);
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
                //CalcularCurvasGrado3(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
            else if(Convert.ToInt32(comboBox1.Text) == 5)
            {
                //ClacularCurvasGrado4(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
        }

        void CalcularCurvasGrado2(Point[] puntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float tolerancia;
            tolerancia = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto1.X, punto1.Y, punto2.X, punto2.Y);
                curva[i] = GetPuntoCurvaGrado2(puntos, (float)i * tolerancia);
                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), points);
                DibujarCurvaActual(curva, i, Color.Blue);

            }
        }

        Point GetPuntoCurvaGrado2(Point[] puntos, float tolerancia)
        {
            Point resultado = new Point();
            int punto1X, punto1Y;
            int punto2X, punto2Y;

            punto1X = (int)Math.Round((puntos[1].X - puntos[0].X) * tolerancia) + puntos[0].X;
            punto1Y = (int)Math.Round((puntos[1].Y - puntos[0].Y) * tolerancia) + puntos[0].Y;
            punto2X = (int)Math.Round((puntos[2].X - puntos[1].X) * tolerancia) + puntos[1].X;
            punto2Y = (int)Math.Round((puntos[2].Y - puntos[1].Y) * tolerancia) + puntos[1].Y;
            resultado.X = (int)Math.Round(((punto2X - punto1X) * tolerancia) + punto1X);
            resultado.Y = (int)Math.Round(((punto2Y - punto1Y) * tolerancia) + punto1Y);

            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Red), punto1X, punto1Y, punto2X, punto2Y);
            punto1 = new Point(punto1X, punto1Y);
            punto2 = new Point(punto2X, punto2Y);
            return resultado;
        }

        void DibujarCurvaActual(Point[] curva, int n, Color color)
        {
            for (int i = 0; i < n; i++)
            {
                WorkSpace.CreateGraphics().DrawRectangle(GetPen(color), curva[i].X, curva[i].Y, 1, 1);
            }
        }

    }
}

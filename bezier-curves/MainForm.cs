﻿using System;
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
                CalcularCurvasGrado2Matricial(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
            else if (Convert.ToInt32(comboBox1.Text) == 4)
            {
                CalcularCurvasGrado3(puntos, DistanciaMayor(puntos, numeroPuntos));
                //CalcularCurvasGrado3Matricial(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
            else if(Convert.ToInt32(comboBox1.Text) == 5)
            {
                CalcularCurvasGrado4(puntos, DistanciaMayor(puntos, numeroPuntos));
                //CalcularCurvasGrado4Matricial(puntos, DistanciaMayor(puntos, numeroPuntos));
            }
        }

        void CalcularCurvasGrado2(Point[] puntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float tolerancia;
            tolerancia = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto1, punto2);
                curva[i] = GetPuntoCurvaGrado2(puntos, (float)i * tolerancia);
                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), points);
                DibujarCurvaBezier(curva, i, Color.Blue);
            }
        }

        Point GetPuntoCurvaGrado2(Point[] puntos, float tolerancia)
        {
            Point resultado;

            punto1 = GetPunto(puntos[0], puntos[1], tolerancia);
            punto2 = GetPunto(puntos[1], puntos[2], tolerancia);

            /* Punto en la curva*/
            resultado = GetPunto(punto1, punto2, tolerancia);

            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Red), punto1, punto2);

            return resultado;
        }

        void DibujarCurvaBezier(Point[] curva, int n, Color color)
        {
            for (int i = 0; i < n; i++)
            {
                WorkSpace.CreateGraphics().DrawRectangle(GetPen(color), curva[i].X, curva[i].Y, 1, 1);
            }
        }

        Point GetPunto(Point a, Point b, float tolerancia)
        {
            Point point = new Point();

            point.X = (int)Math.Round((b.X - a.X) * tolerancia) + a.X;
            point.Y = (int)Math.Round((b.Y - a.Y) * tolerancia) + a.Y;

            return point;
        }

        void CalcularCurvasGrado3(Point[] puntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float tolerancia;
            tolerancia = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto1, punto2);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto2, punto3);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto4, punto5);

                curva[i] = GetPuntoCurvaGrado3(puntos, (float)i * tolerancia);
                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), points);
                DibujarCurvaBezier(curva, i, Color.Blue);
            }
        }



        Point GetPuntoCurvaGrado3(Point[] puntos, float tolerancia)
        {
            Point resultado;

            punto1 = GetPunto(puntos[0], puntos[1], tolerancia);
            punto2 = GetPunto(puntos[1], puntos[2], tolerancia);
            punto3 = GetPunto(puntos[2], puntos[3], tolerancia);
            punto4 = GetPunto(punto1, punto2, tolerancia);
            punto5 = GetPunto(punto2, punto3, tolerancia);

            /* Punto en la curva*/
            resultado = GetPunto(punto4, punto5, tolerancia);

            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), punto1, punto2);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), punto2, punto3);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Red), punto4, punto5);

            return resultado;
        }

        void CalcularCurvasGrado4(Point[] puntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float tolerancia;
            tolerancia = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto1, punto2);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto2, punto3);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto3, punto4);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto5, punto6);
                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto6, punto7);

                WorkSpace.CreateGraphics().DrawLine(GetPen(Color.White), punto8, punto9);

                curva[i] = GetPuntoCurvaGrado4(puntos, (float)i * tolerancia);
                WorkSpace.CreateGraphics().DrawLines(GetPen(Color.Black), points);
                DibujarCurvaBezier(curva, i, Color.Blue);
            }
        }

        Point GetPuntoCurvaGrado4(Point[] puntos, float tolerancia)
        {
            Point resultado;

            punto1 = GetPunto(puntos[0], puntos[1], tolerancia);
            punto2 = GetPunto(puntos[1], puntos[2], tolerancia);
            punto3 = GetPunto(puntos[2], puntos[3], tolerancia);
            punto4 = GetPunto(puntos[3], puntos[4], tolerancia);
            punto5 = GetPunto(punto1, punto2, tolerancia);
            punto6 = GetPunto(punto2, punto3, tolerancia);
            punto7 = GetPunto(punto3, punto4, tolerancia);
            punto8 = GetPunto(punto5, punto6, tolerancia);
            punto9 = GetPunto(punto6, punto7, tolerancia);

            /* Punto en la curva*/
            resultado = GetPunto(punto8, punto9, tolerancia);

            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Yellow), punto1, punto2);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.YellowGreen), punto2, punto3);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Green), punto3, punto4);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Pink), punto5, punto6);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Pink), punto6, punto7);
            WorkSpace.CreateGraphics().DrawLine(GetPen(Color.Blue), punto8, punto9);

            return resultado;
        }

        Point GetPuntoCurvaMatricial(Point[] puntos, float u)
        {
            Point Resultado = new Point();
            float ax, bx, cx, ay, by, cy, x1, x2, x3, y1, y2, y3, u2;
            u2 = u * u;
            ax = puntos[0].X;
            bx = puntos[1].X;
            cx = puntos[2].X;
            ay = puntos[0].Y;
            by = puntos[1].Y;
            cy = puntos[2].Y;
            x1 = ax + (-2 * bx) + cx;
            x2 = (-2 * ax) + (2 * bx);
            x3 = ax;
            y1 = ay + (-2 * by) + cy;
            y2 = (-2 * ay) + (2 * by);
            y3 = ay;

            Resultado.X = (int)Math.Round((x1 * u2) + (x2 * u) + x3);
            Resultado.Y = (int)Math.Round((y1 * u2) + (y2 * u) + y3);
            return Resultado;


        } 

        void CalcularCurvasGrado2Matricial(Point[] puntos, int numeroPuntos)
        {
            Point[] curva = new Point[numeroPuntos];
            float tolerancia;
            tolerancia = (float)1 / (float)(numeroPuntos - 1);

            for (int i = 0; i < numeroPuntos; i++)
            {
                curva[i] = (GetPuntoCurvaMatricial(puntos, (float)i * tolerancia));
            }

            DibujarCurvaBezier(curva, numeroPuntos - 1, Color.Red);


        Point GetPuntoQ(Point cp, Point cp2, float u)
        {
            Point Resultado = new Point();
            int Qx, Qy;
            Qx = (int)Math.Round(cp.X + u * (cp2.X - cp.X));

            Qy = (int)Math.Round(cp.Y + u * (cp2.Y - cp.Y));
            Resultado = new Point(Qx, Qy);
            return Resultado;
        }


        void CalcularCurvasGrado3Matricial(Point[] cp, int np)
        {
            Point[] curve = new Point[np];
            Point[] curved2 = new Point[2];
            Point[] curved3 = new Point[2];
            Point[] curved4 = new Point[2];
            Point[] curvepuntos = new Point[3];

            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {

                curvepuntos[0] = GetPuntoQ(cp[0], cp[1], (float)i * t);
                curvepuntos[1] = GetPuntoQ(cp[1], cp[2], (float)i * t);
                curvepuntos[2] = GetPuntoQ(cp[2], cp[3], (float)i * t);

                curve[i] = (GetPuntoCurvaMatricial(curvepuntos, (float)i * t));

            }

            DibujarCurvaBezier(curve, np - 1, Color.Red);
        }



        void CalcularCurvasGrado4Matricial(Point[] cp2, int np)
        {
            Point[] curve = new Point[np];
            Point[] curved2 = new Point[2];
            Point[] curved3 = new Point[2];
            Point[] curved4 = new Point[2];
            Point[] curvepuntos = new Point[3];
            Point[] cp = new Point[4];

            float t;
            t = (float)1 / (float)(np - 1);

            for (int i = 0; i < np; i++)
            {
                cp[0] = GetPuntoQ(cp2[0], cp2[1], (float)i * t);
                cp[1] = GetPuntoQ(cp2[1], cp2[2], (float)i * t);
                cp[2] = GetPuntoQ(cp2[2], cp2[3], (float)i * t);
                cp[3] = GetPuntoQ(cp2[3], cp2[4], (float)i * t);


                curvepuntos[0] = GetPuntoQ(cp[0], cp[1], (float)i * t);

                curvepuntos[1] = GetPuntoQ(cp[1], cp[2], (float)i * t);

                curvepuntos[2] = GetPuntoQ(cp[2], cp[3], (float)i * t);


                curve[i] = (GetPuntoCurvaMatricial(curvepuntos, (float)i * t));
            }

            DibujarCurvaBezier(curve, np - 1, Color.Red);
        }
    }
}

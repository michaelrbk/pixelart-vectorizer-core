﻿using PixelArtVectorize.Image;
using QuikGraph;
using System;
using System.Collections;
using System.Drawing;


namespace PixelArtVectorize
{
    class SvgVector
    {
        SVGDocument svg;

        public int Width;
        public int Height;
        public int scale = 50;

        public bool DrawPixelArt = true;
        public bool DrawCellBorder = true;
        public bool DrawEdges = true;
        public bool DrawVertex = true;
        public bool DrawNewCells = false;
        public bool DrawNewControlPoints = false;
        public bool DrawValence = false;


        public String ToImageSVG(UndirectedGraph<Pixel, TaggedUndirectedEdge<Pixel, EdgeTag>> g, string fileName = "image.svg")
        {
            svg = new SVGDocument(Width * scale + 1, Height * scale + 1);
            //Percorre o Grafo e desenha o pixelart como SVG
            svg.DrawCellBorder(DrawCellBorder);

            foreach (var v in g.Vertices)
            {
                //Desenha o quadrado do pixelart
                if (DrawPixelArt)
                {
                    svg.DrawRectangle(v.color,
                        Color.Black,
                         1,
                         v.x * scale,
                         v.y * scale,
                         scale,
                         scale);
                }

                //desenha novo formato das celulas
                if (DrawNewCells && !DrawNewControlPoints)
                {

                    String points = "";
                    foreach (Point p in v.points)
                    {
                        points += p.X.ToString() + "," + p.Y.ToString() + " ";
                    }
                    //Console.WriteLine(points);
                    if (v.color.A != 255) // Se cor é transparente, mostra como branco
                    {
                        svg.DrawPolygon(Color.White, Color.Black, 0.100, points);
                    }
                    else
                    {
                        svg.DrawPolygon(v.color, Color.Black, 0.100, points);
                    }
                }

                //desenha o nodo
                if (DrawVertex)
                {
                    svg.DrawCircle(Color.Blue,
                        Color.Transparent,
                        0,
                        v.x * scale + scale / 2,
                        v.y * scale + scale / 2,
                        scale < 10 ? 1 : scale / 10);
                    if (DrawValence)
                    {
                        svg.DrawText(v.valence.ToString(),
                            30.0,
                            Color.White,
                            new Point(v.x * scale + scale / 2, v.y * scale + scale / 2));
                    }
                }

            }

            //desenha os vertices
            if (DrawEdges)
            {
                foreach (var v in g.Edges)
                {
                    svg.DrawLine(Color.Blue, 0.800,
                        v.Source.x * scale + scale / 2,
                        v.Source.y * scale + scale / 2,
                        v.Target.x * scale + scale / 2,
                        v.Target.y * scale + scale / 2);
                }
            }


            //Retorna o nome do arquivo salvo
            svg.Save(fileName);
            return fileName;
        }

        public String ToNewEdges(UndirectedGraph<Pixel, TaggedUndirectedEdge<Pixel, EdgeTag>> g, string fileName = "image.svg")
        {
            svg = new SVGDocument(Width * scale + 1, Height * scale + 1);
            foreach (var e in g.Edges)
            {
                if ((e.Tag.Visible))
                {
                    svg.DrawLine(Color.Black, 0.800,
                        e.Source.x * scale + scale / 2,
                        e.Source.y * scale + scale / 2,
                        e.Target.x * scale + scale / 2,
                        e.Target.y * scale + scale / 2);
                }
            }

            //Retorna o nome do arquivo salvo
            svg.Save(fileName);
            return fileName;
        }

        internal string ToCurves(System.Collections.ArrayList curves, string fileName = "image.svg")
        {
            svg = new SVGDocument(Width * scale + 1, Height * scale + 1);
            String data;
            for (int i = 0; i < curves.Count; i++)
            {
                data = "M";
                System.Collections.ArrayList curve = curves[i] as System.Collections.ArrayList;
                Pixel pixel = curve[0] as Pixel;
                data += pixel.x + "," + pixel.y;
                curve.Add(curve[curve.Count - 1]);
                data += CatmullRom2bezier(curve);
                svg.DrawPath(Color.White, Color.Red, 1, data);
            }

            //Retorna o nome do arquivo salvo
            svg.Save(fileName);
            return fileName;
        }

        internal string NewImage(UndirectedGraph<Pixel, TaggedUndirectedEdge<Pixel, EdgeTag>> g, Shapes shapes, string fileName = "image.svg")
        {
            svg = new SVGDocument(Width * scale + 1, Height * scale + 1);
            String data;
            Color color = new Color();
            foreach (Shape shape in shapes)
            {
                data = "M ";
                Pixel lastPixel = null;
                for (int i = 0; i < shape.Count; i++)
                {

                    ArrayList curve = ((Curve)shape[i]).CurveToPoints();
                    if (i != 0 && !curve[0].Equals(lastPixel)) // Corrige curvas que possa estar no sentido errado
                    {
                        curve.Reverse();
                    }

                    Pixel pixel = curve[0] as Pixel;
                    lastPixel = curve[curve.Count - 1] as Pixel;
                    if (i == 0)
                    {
                        data += pixel.x + "," + pixel.y;
                    }

                    curve.Add(curve[curve.Count - 1]);

                    data += CatmullRom2bezier(curve) + " ";

                    color = ((Curve)shape[0]).Color;


                }
                svg.DrawPath(color, color, 0.01, data);

            }
            /*
            foreach (var v in g.Vertices)
            {
                svg.DrawCircle(Color.Blue,
                       Color.Transparent,
                       0,
                       v.x * 7 + 7 / 2,
                       v.y * 7 + 7 / 2,
                       1);
            }*/

            //Retorna o nome do arquivo salvo
            svg.Save(fileName);
            return fileName;
        }


        internal String CatmullRom2bezier(System.Collections.ArrayList points)
        {

            String ret = "";
            for (int i = 0, iLen = points.Count - 1; iLen - 1 > i; i++)
            {
                ArrayList p = new ArrayList(); ;

                //Tratamento para os primeiros e os ultimos pontos da curva
                if (i == 0)
                {
                    p.Add(new Point(((Pixel)points[i]).x, ((Pixel)points[i]).y));
                    p.Add(new Point(((Pixel)points[i]).x, ((Pixel)points[i]).y));
                    p.Add(new Point(((Pixel)points[i + 1]).x, ((Pixel)points[i + 1]).y));
                    p.Add(new Point(((Pixel)points[i + 2]).x, ((Pixel)points[i + 2]).y));
                }
                else if (iLen - 2 == i)
                {
                    p.Add(new Point(((Pixel)points[i - 1]).x, ((Pixel)points[i - 1]).y));
                    p.Add(new Point(((Pixel)points[i]).x, ((Pixel)points[i]).y));
                    p.Add(new Point(((Pixel)points[i + 1]).x, ((Pixel)points[i + 1]).y));
                    p.Add(new Point(((Pixel)points[i + 1]).x, ((Pixel)points[i + 1]).y));
                }
                else
                {
                    p.Add(new Point(((Pixel)points[i - 1]).x, ((Pixel)points[i - 1]).y));
                    p.Add(new Point(((Pixel)points[i]).x, ((Pixel)points[i]).y));
                    p.Add(new Point(((Pixel)points[i + 1]).x, ((Pixel)points[i + 1]).y));
                    p.Add(new Point(((Pixel)points[i + 2]).x, ((Pixel)points[i + 2]).y));
                }

                // Catmull-Rom para Cubic Bezier Matriz de conversão simplificada
                //    0       1       0       0
                //  -1/6      1      1/6      0
                //    0      1/6      1     -1/6
                //    0       0       1       0

                System.Collections.ArrayList bp = new ArrayList(); ; // Bezier Points
                Point p0 = (Point)p[0];
                Point p1 = (Point)p[1];
                Point p2 = (Point)p[2];
                Point p3 = (Point)p[3];

                bp.Add(new PointD(p1.X, p1.Y));
                bp.Add(new PointD(((-p0.X + 6 * (double)p1.X + p2.X) / 6), (-p0.Y + 6 * (double)p1.Y + p2.Y) / 6));
                bp.Add(new PointD((p1.X + 6 * (double)p2.X - p3.X) / 6, (p1.Y + 6 * (double)p2.Y - p3.Y) / 6));
                bp.Add(new PointD(p2.X, p2.Y));

                ret += "C" + ((PointD)bp[1]).X.ToString().Replace(",", ".") + "," + ((PointD)bp[1]).Y.ToString().Replace(",", ".") + " " + ((PointD)bp[2]).X.ToString().Replace(",", ".") + "," + ((PointD)bp[2]).Y.ToString().Replace(",", ".") + " " + ((PointD)bp[3]).X.ToString().Replace(",", ".") + "," + ((PointD)bp[3]).Y.ToString().Replace(",", ".") + " ";
            }

            return ret;
        }

    }
}

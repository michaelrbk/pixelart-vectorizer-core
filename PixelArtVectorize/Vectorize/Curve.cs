﻿using QuikGraph;
using System;
using System.Collections;
using System.Drawing;

namespace PixelArtVectorize
{
    class Curve
    {

        public ArrayList CurveOfEdges { get; set; }
        public Color Color { get; set; }

        public Curve(ArrayList curveOfEdges, Color c)
        {
            CurveOfEdges = curveOfEdges;
            Color = c;
        }

        public override string ToString()
        {
            return CurveOfEdges.ToString();
        }

        public double CurveSize()
        {
            TaggedUndirectedEdge<Pixel, EdgeTag> edge;
            double lenght = 0;
            foreach (var e in CurveOfEdges)
            {
                edge = (TaggedUndirectedEdge<Pixel, EdgeTag>)e;
                Point p1 = edge.Source.getPoint();
                Point p2 = edge.Target.getPoint();
                lenght += (Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
            }
            return lenght;

        }

        public ArrayList CurveToPoints()
        {
            ArrayList curveOfEdge = CurveOfEdges;
            ArrayList curveOfPoints;
            Pixel lastPoint;
            Pixel firstPoint;

            firstPoint = null;
            lastPoint = null;
            curveOfPoints = new ArrayList();
            if (curveOfEdge.Count == 1)
            {
                curveOfPoints.Add(((TaggedUndirectedEdge<Pixel, EdgeTag>)curveOfEdge[0]).Source);
                curveOfPoints.Add(((TaggedUndirectedEdge<Pixel, EdgeTag>)curveOfEdge[0]).Target);
            }
            else
            {
                for (int i = 0; i < curveOfEdge.Count; i++)
                {
                    TaggedUndirectedEdge<Pixel, EdgeTag> ed = (TaggedUndirectedEdge<Pixel, EdgeTag>)curveOfEdge[i];
                    if (i == 0)
                    {
                        firstPoint = ed.Source;
                        lastPoint = ed.Target;
                    }
                    else
                    {
                        if (i == 1)
                        {
                            if (ed.Source.Equals(firstPoint))
                            {
                                firstPoint = lastPoint;
                                lastPoint = ed.Source;

                            }
                            else if (ed.Target.Equals(firstPoint))
                            {
                                firstPoint = lastPoint;
                                lastPoint = ed.Target;
                            }
                            curveOfPoints.Add(firstPoint);
                            curveOfPoints.Add(lastPoint);


                        }
                        if (ed.Source.Equals(lastPoint))
                        {
                            lastPoint = ed.Target;
                        }
                        else
                        {
                            lastPoint = ed.Source;
                        }
                        if (i > 0)
                        {
                            curveOfPoints.Add(lastPoint);
                        }
                    }
                }
            }

            return curveOfPoints;
        }
    }


}

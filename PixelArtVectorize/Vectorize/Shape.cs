using System;
using System.Collections;
using System.Drawing;

namespace PixelArtVectorize
{
    class Shape : ArrayList, IComparable<Shape>
    {

        public Color GetColor()
        {
            return ((Curve)this[0]).Color;
        }


        public int CompareTo(Shape otherShape)
        {
            double sizeShapeA;
            double sizeShapeB;
            /*
            foreach (Curve curve in this)
            {
                sizeShapeA += curve.curveSize();
            }

            foreach (Curve otherCurve in otherShape)
            {
                sizeShapeB += otherCurve.curveSize();
            }
            
            if (sizeShapeA > sizeShapeB)
                return -1;
            if (sizeShapeA < sizeShapeB)
                return 1;
            else
                return 0;*/
            sizeShapeA = PolygonArea();
            sizeShapeB = otherShape.PolygonArea();
            if (sizeShapeA > sizeShapeB)
            {
                return -1;
            }

            if (sizeShapeA < sizeShapeB)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool IsSameShape(Shape otherShape)
        {
            if (Count != otherShape.Count)
            {
                return false;
            }

            Curve curve;
            Curve otherCurve;
            for (int i = 0; i < Count; i++)
            {
                curve = ((Curve)this[i]);
                otherCurve = ((Curve)otherShape[i]);
                if (curve.CurveOfEdges != otherCurve.CurveOfEdges)
                {
                    return false;
                }
            }

            return true;
        }

        public ArrayList ToPoints()
        {
            ArrayList shapeOfPoints = new ArrayList();
            ArrayList points;
            Pixel lastPoint = null;

            for (int i = 0; i < Count; i++)
            {

                points = ((Curve)this[i]).CurveToPoints();
                if (i != 0 && !points[0].Equals(lastPoint)) // Corrige curvas que possa estar no sentido errado
                {
                    points.Reverse();
                }

                lastPoint = (Pixel)points[points.Count - 1];
                shapeOfPoints.AddRange(points);
            }

            return shapeOfPoints;
        }


        /*
         * //  Public-domain function by Darel Rex Finley, 2006.

            double polygonArea(double *X, double *Y, int points) {
              double  area=0. ;
              int     i, j=points-1  ;
              for (i=0; i<points; i++) {
                area+=(X[j]+X[i])*(Y[j]-Y[i]); j=i; }
              return area*.5; }
         */

        double PolygonArea()
        {
            ArrayList points = new ArrayList(ToPoints());
            int i, j = points.Count - 1;

            double area = 0;


            for (i = 0; i < points.Count; i++)
            {

                area += (((Pixel)points[j]).x + ((Pixel)points[i]).x) * (((Pixel)points[j]).y - ((Pixel)points[i]).y);
                j = i;
                //area -= ((Pixel)points[i]).y * ((Pixel)points[j]).x;
            }

            area = area / 2;
            return (area < 0 ? -area : area);
        }

    }
}

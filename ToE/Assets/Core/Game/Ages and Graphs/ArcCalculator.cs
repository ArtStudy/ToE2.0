using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Ages_and_Graphs
{
    
    public static class ArcCalculator
    {
        public static int countOfPoints = 20;

        public static Vector3[] ArcPointsCalculation(Vector3 parent, Vector3 child)
        {
            Vector3[] Points = new Vector3[countOfPoints];
            Vector3 delta = new Vector3((child.x - parent.x)/countOfPoints, (child.y - parent.y)/countOfPoints, (child.z - parent.z)/countOfPoints);
            for (int i = 0; i < countOfPoints; i++)
            {               
                Points[i] = parent * child.magnitude / parent.magnitude;
                parent += delta;
            }
            return Points;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Core.Game.Ages_and_Graphs
{
    static class WayDrawer
    {
        public static void DrawWay(WayObj way)
            {
            LineRenderer lineRenderer = way.GetComponent<LineRenderer>();
            lineRenderer.positionCount = ArcCalculator.countOfPoints;
            lineRenderer.SetPositions(ArcCalculator.ArcPointsCalculation(way.ParentLevel.transform.position, way.ChildLevel.transform.position));            
            }
    }
}

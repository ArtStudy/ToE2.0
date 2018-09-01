using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Ages_and_Graphs
{
    static class WayDrawer
    {
        public static void DrawWay(WayObj way)
            {
            LineRenderer lineRenderer = way.GetComponent<LineRenderer>();
            lineRenderer.SetPositions(ArcCalculator.ArcPointsCalculation(way.Parent.transform.position, way.Child.transform.position));
            }
    }
}

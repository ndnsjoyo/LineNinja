using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waypoint
{
    public class WaypointGroup : MonoBehaviour
    {
        private Vector3[] waypoints;
        public Vector3[] Waypoints
        {
            get { return waypoints; }
        }

        void Awake()
        {
            waypoints = new Vector3[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                waypoints[i] = transform.GetChild(i).position;
        }
    }
}



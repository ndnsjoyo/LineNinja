using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waypoint
{
    public class WaypointGroup : MonoBehaviour
    {
        private Vector3[] _waypoints;
        public Vector3[] Waypoints
        {
            get { return _waypoints; }
        }

        void Awake()
        {
            // 加载所有子对象的transform位置
            _waypoints = new Vector3[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                _waypoints[i] = transform.GetChild(i).position;
        }
    }
}



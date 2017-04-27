using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Waypoint
{
    public class WaypointFollower : MonoBehaviour
    {
        public WaypointGroup waypointGroup;

        // router
        public enum RoutingType
        {
            ONCE,
            CIRCULAR,
            RETURN
        }
        public RoutingType routeType = RoutingType.ONCE;

        private Vector3[] waypoints;
        private int nextWaypoint = -1;
        public int initDirection = 1;

        // movement controller
        public bool moving = true;
        public float movingSpeed = 1.0f;

        void Start()
        {
            waypoints = waypointGroup.Waypoints;
            float minDist = float.PositiveInfinity;
            for (int i = 0; i < waypoints.Length; i++)
            {
                float dist = (transform.position - waypoints[i]).magnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    nextWaypoint = i;
                }
            }
            LookAtNextWaypoint();
        }

        void FixedUpdate()
        {
            if (moving)
            {
                transform.Translate(Vector3.forward * movingSpeed * Time.fixedDeltaTime);
                // Debug.Log((transform.position - waypoints[nextWaypoint]).magnitude);

                if ((transform.position - waypoints[nextWaypoint]).magnitude < 0.5f)
                {
                    nextWaypoint = GetNextWaypoint();
                    if (nextWaypoint == -1)
                    {
                        moving = false;
                        return;
                    }
                    LookAtNextWaypoint();
                }
            }
        }

        int GetNextWaypoint()
        {
            switch (routeType)
            {
                case RoutingType.ONCE:
                    if (0 < nextWaypoint && nextWaypoint < waypoints.Length - 1)
                    {
                        return nextWaypoint + initDirection;
                    }
                    break;
                case RoutingType.CIRCULAR:
                    if (nextWaypoint == waypoints.Length - 1 && initDirection == 1)
                    {
                        return 0;
                    }
                    else if (nextWaypoint == 0 && initDirection == -1)
                    {
                        return waypoints.Length - 1;
                    }
                    else
                    {
                        return nextWaypoint + initDirection;
                    }
                case RoutingType.RETURN:
                    if ((nextWaypoint == waypoints.Length - 1 && initDirection == 1)
                    || (nextWaypoint == 0 && initDirection == -1))
                    {
                        initDirection *= -1;
                    }
                    return nextWaypoint + initDirection;
                default:
                    break;
            }
            return -1;
        }

        void LookAtNextWaypoint()
        {
            transform.rotation = Quaternion.LookRotation(waypoints[nextWaypoint] - transform.position);
        }
    }
}
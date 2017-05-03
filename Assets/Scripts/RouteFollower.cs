using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using DG.Tweening;

[RequireComponent(typeof(splineMove))]
public class RouteFollower : MonoBehaviour
{
    private splineMove _splineMove;

    public float speed = 5;
    public bool lockRotation = true;
    public float waitTime = 1.0f;

    void Start()
    {
        _splineMove = GetComponent<splineMove>();

        _splineMove.onStart = true;
        _splineMove.moveToPath = true;
        _splineMove.loopType = splineMove.LoopType.pingPong;
        _splineMove.pathType = PathType.Linear;

        _splineMove.speed = speed;
        if (lockRotation)
            _splineMove.lockRotation = AxisConstraint.Y;

        _splineMove.events[0]
            .AddListener(() => _splineMove.Pause(waitTime));
        _splineMove.events[_splineMove.events.Count - 1]
            .AddListener(() => _splineMove.Pause(waitTime));
    }
}

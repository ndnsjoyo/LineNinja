using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRegulator : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float Speed = 10.0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.velocity;
        velocity.y = 0.0f;
        _rigidbody.velocity = velocity.normalized * Speed;
    }
}

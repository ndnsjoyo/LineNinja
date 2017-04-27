using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpeedRegulator : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private float _speed = 0.0f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private bool _locked = true;
    public bool Locked
    {
        get { return _locked; }
        set
        {
            if (value)
            {
                Debug.Log("locked speed");
                _speed = _rigidbody.velocity.magnitude;
            }
            else
            {
                Debug.Log("unlocked speed");
            }
            _locked = value;
        }
    }

    void FixedUpdate()
    {
        if (_locked)
        {
            Vector3 direction = transform.rotation * Vector3.forward;
            _rigidbody.velocity = direction * _speed;
        }
    }
}

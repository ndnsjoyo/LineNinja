using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DirectionRegulator : MonoBehaviour
{
    private Rigidbody _rigidbody;

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
                Debug.Log("locked direction");
            }
            else
            {
                Debug.Log("unlocked direction");
            }
            _locked = value;
        }
    }

    void FixedUpdate()
    {
        if (_locked)
        {
            Vector3 horizontalVelocity = _rigidbody.velocity;
            horizontalVelocity.y = 0.0f;
            if (horizontalVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(horizontalVelocity, Vector3.up);
            }
        }
    }
}

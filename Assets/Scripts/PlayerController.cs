using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float impluseLevel = 1.0f;

    private bool _alive = true;

    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            // if (Input.GetKey("w"))
            //     _rigidbody.AddForce(Vector3.forward * impluseLevel * _rigidbody.mass, ForceMode.Impulse);
            // else if (Input.GetKey("s"))
            //     _rigidbody.AddForce(Vector3.back * impluseLevel * _rigidbody.mass, ForceMode.Impulse);
            if (Input.GetKey("a"))
                _rigidbody.AddForce(Vector3.left * impluseLevel * _rigidbody.mass, ForceMode.Impulse);
            else if (Input.GetKey("d"))
                _rigidbody.AddForce(Vector3.right * impluseLevel * _rigidbody.mass, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (_alive)
        {
            _rigidbody.velocity = new Vector3(
                _rigidbody.velocity.x,
                _rigidbody.velocity.y,
                5.0f);
        }
    }

    public Material deadMaterial;
    public void OnDead()
    {
        _alive = false;
        GetComponent<MeshRenderer>().material = deadMaterial;
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        GUI.Label(new Rect(30, 10, 100, 20), _rigidbody.velocity.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Unity 组件
    private Rigidbody _rigidbody;

    // 控制参数
    // 横向移动冲量
    public float horizontalImpluse = 1.0f;
    public float bounceLevel = 1.0f;

    // 状态
    private bool _alive = true;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Velocity = Vector3.forward * 10.0f;
    }

    void Update()
    {
        if (_alive)
        {
            if (Input.GetKey("a"))
                _rigidbody.AddForce(Vector3.left * horizontalImpluse * _rigidbody.mass, ForceMode.Impulse);
            else if (Input.GetKey("d"))
                _rigidbody.AddForce(Vector3.right * horizontalImpluse * _rigidbody.mass, ForceMode.Impulse);
        }
    }

    public Vector3 Velocity
    {
        get { return _rigidbody.velocity; }
        set { _rigidbody.velocity = value; }
    }

    public void OnDead()
    {
        _alive = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bounce")
        {
            _rigidbody.AddForce(-other.impulse * bounceLevel, ForceMode.Impulse);
        }
    }

    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 100, 20), _rigidbody.velocity.ToString());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Unity 组件
    private Rigidbody _rigidbody;

    // 控制参数
    // 前进速度
    public float velocity = 10.0f;
    // 横向移动冲量
    public float horizontalImpluse = 1.0f;
    // 反弹力度
    public float bounceLevel = 1.0f;
    // 反弹后恢复速度时间
    public float bounceRecoverTime = 0.5f;

    // 状态
    private bool _alive = true;

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();

        // 初始化速度
        Velocity = Vector3.forward * velocity;
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

    private float _bounceCountDown = -1.0f;
    void FixedUpdate()
    {
        if (_bounceCountDown != -1.0f)
        {
            if (_bounceCountDown < 0.0f)
            {
                Velocity = Vector3.forward * velocity;
                _bounceCountDown = -1.0f;
            }
            else
            {
                _bounceCountDown -= Time.fixedDeltaTime;
            }
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
            _bounceCountDown = bounceRecoverTime;
        }
    }

    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 100, 20), _rigidbody.velocity.ToString());
    }
}

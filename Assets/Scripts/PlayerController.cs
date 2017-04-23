using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Unity 组件
    private Rigidbody _rigidbody;

    // 控制参数
    // 前进基准速度
    public const float baseSpeed = 10.0f;
    // 横向移动冲量
    public float horizontalImpluse = 1.0f;

    // 属性
    // 速度属性
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    // 方向属性
    public Vector3 Direction
    {
        get { return _rigidbody.velocity.normalized; }
        set { _rigidbody.velocity = value.normalized * _speed; }
    }

    // 状态
    private float _speed = baseSpeed;
    private bool _alive = true;

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();

        // 初始化方向
        Direction = Vector3.forward;
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

    public void OnDead()
    {
        _alive = false;
    }

    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 100, 20), _rigidbody.velocity.ToString());
    }
}

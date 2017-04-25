using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Unity 组件
    private Rigidbody _rigidbody;

    // 控制参数
    // 前进基准速度
    public float baseSpeed = 10.0f;
    // 横向移动冲量
    public float horizontalImpluse = 1.0f;
    // 跳跃高度
    public float jumpHeight = 2.5f;
    // 跳跃距离
    public float jumpDistance = 7.0f;

    // 属性
    // 存活属性
    public bool IsAlive
    {
        get { return _alive; }
    }
    // 速度属性
    public float Speed
    {
        get { return _rigidbody.velocity.magnitude; }
        set { _rigidbody.velocity = Direction.normalized * value; }
    }
    // 方向属性
    public Vector3 Direction
    {
        get { return _rigidbody.velocity.normalized; }
        set { _rigidbody.velocity = value.normalized * Speed; }
    }

    // 状态
    private float _speed;
    private bool _alive = true;
    private float _jumpStartZ = float.PositiveInfinity;

    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 100, 20), _rigidbody.velocity.magnitude.ToString());
    }

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();

        // 初始化
        ResetVelocity();
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.forward * baseSpeed;
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

    void FixedUpdate()
    {
        // 落下控制
        if (transform.position.z > _jumpStartZ + jumpDistance)
        {
            _jumpStartZ = float.PositiveInfinity;
            JumpDown();
        }
    }

    public void OnDead()
    {
        if (!float.IsPositiveInfinity(_jumpStartZ))
            _rigidbody.useGravity = true;

        Debug.Log("死亡");
        _alive = false;
    }

    // 跳跃控制
    public void OnJump()
    {
        _jumpStartZ = transform.position.z;
        JumpUp();
    }
    private void JumpUp()
    {
        Speed *= 1.5f;
        _rigidbody.useGravity = false;
        transform.Translate(Vector3.up * jumpHeight);
    }
    private void JumpDown()
    {
        transform.Translate(Vector3.down * jumpHeight);
        _rigidbody.useGravity = true;
        Speed /= 1.5f;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpeedRegulator))]
[RequireComponent(typeof(DirectionRegulator))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float FixedMovedDistance
    {
        get { return _rigidbody.velocity.magnitude * Time.fixedDeltaTime; }
    }
    public bool UseGravity { get { return _rigidbody.useGravity; } set { _rigidbody.useGravity = value; } }

    // 速率限制器
    private SpeedRegulator _speedRegulator;
    public float Speed { get { return _speedRegulator.Speed; } set { _speedRegulator.Speed = value; } }

    // 方向控制器
    private DirectionRegulator _directionRegulator;

    private bool _motionLock = true;
    public bool MotionLock
    {
        get { return _motionLock; }
        set
        {
            _motionLock = value;
            _speedRegulator.Locked = value;
            _directionRegulator.Locked = value;
        }
    }

    // 状态机
    [HideInInspector] public PlayerState.State State;
    // 存活检测
    public bool IsAlive { get { return State.GetType() != typeof(PlayerState.Dead); } }

    // 各状态设置
    public float runningSpeed = 10.0f;
    public float jumpingHeight = 2.5f;
    public float jumpingDistance = 7.0f;
    public float dashingSpeed = 20.0f;
    public float dashingDistance = 20.0f;
    public int dashingRefreshTimesLimit = 2;

    // 刀
    private bool _withKatana = false;
    public bool WithKatana
    {
        get { return _withKatana; }
        set
        {
            if (value) { Debug.Log("装备刀"); }
            else { Debug.Log("消耗刀"); }
            _withKatana = value;
        }
    }

    public Animator _animator;
    public Animator Animator { get { return _animator; } }

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();
        _speedRegulator = GetComponent<SpeedRegulator>();
        _directionRegulator = GetComponent<DirectionRegulator>();

        State = new PlayerState.Debug(this);
    }

    void Update()
    {
        State.Update();
    }
    void FixedUpdate()
    {
        State.FixedUpdate();
    }

    // Debug
    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 200, 20), _rigidbody.velocity.ToString());
        // 显示速率
        GUI.Label(new Rect(10, 30, 200, 20), _rigidbody.velocity.magnitude.ToString());
        // 显示状态
        GUI.Label(new Rect(10, 50, 200, 20), State.GetType().ToString());
    }
}
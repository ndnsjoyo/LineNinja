using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody
    {
        get { return _rigidbody; }
    }

    // 基准速度
    public float baseSpeed = 10.0f;

    private PlayerState.State _state;
    public PlayerState.State State
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool IsAlive
    {
        get { return _state.GetType() != typeof(PlayerState.Dead); }
    }

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();

        _state = new PlayerState.Debug(this);
        _state.Enter();
    }

    void Update()
    {
        _state.Update();
    }

    void FixedUpdate()
    {
        _state.FixedUpdate();
    }

    // Debug
    void OnGUI()
    {
        // 显示速度
        GUI.Label(new Rect(10, 10, 200, 20), _rigidbody.velocity.magnitude.ToString());
        GUI.Label(new Rect(10, 30, 200, 20), State.GetType().ToString());
    }
}
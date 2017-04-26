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

    private SpeedRegulator _speedRegulator;
    public SpeedRegulator SpeedRegulator
    {
        get { return _speedRegulator; }
    }

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

    // 刀
    private bool _withKatana = false;
    public bool WithKatana
    {
        get { return _withKatana; }
        set
        {
            if (value)
            {
                Debug.Log("装备刀");
            }
            else
            {
                Debug.Log("消耗刀");
            }
            _withKatana = value;
        }
    }

    void Start()
    {
        // 获取组件
        _rigidbody = GetComponent<Rigidbody>();
        _speedRegulator = GetComponent<SpeedRegulator>();

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
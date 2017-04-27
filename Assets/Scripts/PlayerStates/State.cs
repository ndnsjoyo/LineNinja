using System;
using System.Reflection;
using UnityEngine;

namespace PlayerState
{
    public class State : UnityEngine.Object
    {
        public PlayerController player;
        public State(PlayerController player) { this.player = player; }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }

        // 下一个State的初始化会在当前状态的Exit前进行，需要修改
        public void SwitchTo(State targetState)
        {
            Exit();
            UnityEngine.Debug.Log("switch to " + targetState.GetType());
            player.State = targetState;
        }
    }
}

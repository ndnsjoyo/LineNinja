using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Running : State
    {
        public Running(PlayerController player) : base(player) { }

        public override void Enter()
        {
            UnityEngine.Debug.Log("开始奔跑");
            player.SpeedRegulator.Speed = 10.0f;
        }
    }
}

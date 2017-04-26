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
            if (player.Rigidbody.velocity.magnitude == 0.0f)
            {
                player.Rigidbody.velocity = Vector3.forward * player.baseSpeed;
            }
            else
            {
                player.Rigidbody.velocity = player.Rigidbody.velocity.normalized * player.baseSpeed;
            }
        }
    }
}

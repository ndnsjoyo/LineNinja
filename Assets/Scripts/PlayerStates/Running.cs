using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Running : State
    {
        public Running(PlayerController player) : base(player)
        {
            UnityEngine.Debug.Log("开始奔跑");
            player.Speed = player.runningSpeed;
            player.Animator.SetBool("IsRunning", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Dashing : State
    {
        static readonly private float dashDistance = 20.0f;

        public Dashing(PlayerController player) : base(player) { }

        public override void Enter()
        {
            UnityEngine.Debug.Log("开始冲刺");
            player.SpeedRegulator.Speed = 20.0f;
        }

        private int refreshTimeLimit = 2;
        private float milometer = 0.0f;
        public override void FixedUpdate()
        {
            milometer += Time.fixedDeltaTime * player.Rigidbody.velocity.magnitude;
            if (milometer > dashDistance)
            {
                player.State.SwitchTo(typeof(Running));
            }
        }

        public override void Exit()
        {
            UnityEngine.Debug.Log("结束冲刺");
        }

        public void Refresh()
        {
            if (refreshTimeLimit > 0)
            {
                milometer = 0.0f;
                refreshTimeLimit--;
            }
            else
            {
                UnityEngine.Debug.Log("刷新失败，刷新次数耗尽");
            }
        }
    }
}

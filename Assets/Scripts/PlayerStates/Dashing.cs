using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Dashing : State
    {
        public Dashing(PlayerController player) : base(player)
        {
            UnityEngine.Debug.Log("开始冲刺");
            player.Speed = player.dashingSpeed;
            _refreshTimesLimit = player.dashingRefreshTimesLimit;
        }

        private float _milometer = 0.0f;
        public override void FixedUpdate()
        {
            _milometer += player.FixedMovedDistance;
            if (_milometer > player.dashingDistance)
            {
                player.State.SwitchTo(new Running(player));
            }
        }

        public override void Exit()
        {
            UnityEngine.Debug.Log("结束冲刺");
        }

        private int _refreshTimesLimit;
        public void Refresh()
        {
            if (_refreshTimesLimit > 0)
            {
                UnityEngine.Debug.Log("刷新冲刺");
                _milometer = 0.0f;
                _refreshTimesLimit--;
            }
            else
            {
                UnityEngine.Debug.Log("冲刺刷新次数耗尽");
            }
        }
    }
}

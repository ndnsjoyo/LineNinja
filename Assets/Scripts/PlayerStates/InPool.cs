using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class InPool : State
    {
        private float originalSpeed = 1.0f;
        public InPool(PlayerController player) : base(player)
        {
            originalSpeed = player.Rigidbody.velocity.magnitude;
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("进入水池");

            player.Rigidbody.velocity = player.Rigidbody.velocity.normalized * originalSpeed * 0.6f;
        }

        public override void Exit()
        {
            UnityEngine.Debug.Log("离开水池");

            player.Rigidbody.velocity = player.Rigidbody.velocity.normalized * originalSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Jumping : State
    {
        static readonly private float jumpingDistance = 7.0f;

        private float startZ = 0.0f;

        public Jumping(PlayerController player) : base(player)
        {
            startZ = player.transform.position.z;
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("起跳");
        }

        public override void FixedUpdate()
        {

            if (player.transform.position.z > startZ + jumpingDistance)
            {
                UnityEngine.Debug.Log("落地");
                player.State.SwitchTo(typeof(PlayerState.Running));
            }
        }
    }
}

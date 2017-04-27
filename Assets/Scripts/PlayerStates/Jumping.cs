using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Jumping : State
    {
        static public float height = 2.5f;
        static public float distance = 7.0f;

        public Jumping(PlayerController player) : base(player)
        {
            UnityEngine.Debug.Log("起跳");

            player.Speed = player.runningSpeed;

            Vector3 position = player.transform.position;
            position.y += height;
            player.transform.position = position;

            player.UseGravity = false;
        }

        private float _milometer = 0.0f;
        public override void FixedUpdate()
        {
            _milometer += player.FixedMovedDistance;
            if (_milometer > distance)
            {
                UnityEngine.Debug.Log("落地");
                player.State.SwitchTo(new Running(player));
            }
        }

        public override void Exit()
        {
            Vector3 position = player.transform.position;
            position.y -= height;
            player.transform.position = position;

            player.UseGravity = true;
        }
    }
}

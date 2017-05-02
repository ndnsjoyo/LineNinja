using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PlayerState
{
    public class Jumping : State
    {
        private float startY;

        public Jumping(PlayerController player) : base(player)
        {
            UnityEngine.Debug.Log("起跳");

            player.Speed = player.runningSpeed;

            startY = player.transform.position.y;

            player.transform
                .DOMoveY(startY + player.jumpingHeight, player.jumpingUpTime)
                .OnStart(() => player.UseGravity = false);
            // Vector3 position = player.transform.position;
            // position.y += height;
            // player.transform.position = position;

            player.Animator.SetTrigger("Jump");
        }

        private float _milometer = 0.0f;
        public override void FixedUpdate()
        {
            _milometer += player.FixedMovedDistance;
            if (_milometer > player.jumpingDistance)
            {
                UnityEngine.Debug.Log("落地");
                player.State.SwitchTo(new Running(player));
            }
        }

        public override void Exit()
        {
            player.transform
                .DOMoveY(startY, player.jumpingDownTime)
                .OnComplete(() => player.UseGravity = true);
            // Vector3 position = player.transform.position;
            // position.y -= height;
            // player.transform.position = position;

            player.Animator.SetTrigger("Jump");
        }
    }
}

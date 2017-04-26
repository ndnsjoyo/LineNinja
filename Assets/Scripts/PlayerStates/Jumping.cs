using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Jumping : State
    {
        static readonly private float jumpingHeight = 2.5f;
        static readonly private float jumpingDistance = 7.0f;

        private float startZ = 0.0f;

        public Jumping(PlayerController player) : base(player)
        {
            startZ = player.transform.position.z;
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("起跳");
            player.Rigidbody.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }

        public override void FixedUpdate()
        {
            if (player.transform.position.y > jumpingHeight)
            {
                Vector3 velocity = player.Rigidbody.velocity;
                velocity.y = 0.0f;
                player.Rigidbody.velocity = velocity;

                player.Rigidbody.useGravity = false;
            }

            if (player.transform.position.z > startZ + jumpingDistance)
            {
                player.Rigidbody.useGravity = true;
            }
        }
    }
}

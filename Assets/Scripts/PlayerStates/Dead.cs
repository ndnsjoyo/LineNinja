using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Dead : State
    {
        public Dead(PlayerController player) : base(player) { }

        public override void Enter()
        {
            UnityEngine.Debug.Log("死亡");
        }
    }
}

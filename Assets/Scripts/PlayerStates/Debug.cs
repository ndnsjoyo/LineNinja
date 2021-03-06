﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
    public class Debug : State
    {
        public Debug(PlayerController player) : base(player)
        {
            UnityEngine.Debug.Log("进入 Debug");
        }

        public override void Update()
        {
            if (Input.GetKey("q")) player.State.SwitchTo(new Running(player));
            if (Input.GetKey("e")) player.State.SwitchTo(new Dashing(player));
        }
    }
}

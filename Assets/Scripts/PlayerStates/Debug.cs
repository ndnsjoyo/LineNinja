using System.Collections;
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
            if (Input.GetKey("w"))
                player.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
            else if (Input.GetKey("s"))
                player.GetComponent<Rigidbody>().AddForce(Vector3.back * 5, ForceMode.Impulse);

            if (Input.GetKey("a"))
                player.GetComponent<Rigidbody>().AddForce(Vector3.left * 5, ForceMode.Impulse);
            else if (Input.GetKey("d"))
                player.GetComponent<Rigidbody>().AddForce(Vector3.right * 5, ForceMode.Impulse);

            if (Input.GetKey("q")) player.State.SwitchTo(new Running(player));
            if (Input.GetKey("e")) player.State.SwitchTo(new Dashing(player));
        }
    }
}

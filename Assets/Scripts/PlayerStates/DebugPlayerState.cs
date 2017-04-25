using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerState : PlayerState
{
    public DebugPlayerState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("进入 Debug");
    }

    public override void Update()
    {
        if (Input.GetKey("w"))
            player.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
        else if (Input.GetKey("s"))
            player.GetComponent<Rigidbody>().AddForce(Vector3.back, ForceMode.Impulse);

        if (Input.GetKey("a"))
            player.GetComponent<Rigidbody>().AddForce(Vector3.left, ForceMode.Impulse);
        else if (Input.GetKey("d"))
            player.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);

        if (Input.GetKey("r"))
            player.State.SwitchTo(typeof(RunningPlayerState));
    }
}

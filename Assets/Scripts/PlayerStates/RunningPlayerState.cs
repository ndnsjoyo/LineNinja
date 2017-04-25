using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningPlayerState : PlayerState
{
    public RunningPlayerState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("开始奔跑");
        player.Rigidbody.velocity = Vector3.forward * player.baseSpeed;
    }
}

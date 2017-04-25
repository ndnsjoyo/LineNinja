using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerState : PlayerState
{
    public DeadPlayerState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("死亡");
    }
}

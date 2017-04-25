using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        Debug.Log("起跳");
        player.Rigidbody.AddForce(Vector3.up * 7.5f, ForceMode.Impulse);
    }
}

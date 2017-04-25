using UnityEngine;

public class GroundCollisionHandler : CollisionHandler
{
    public GroundCollisionHandler(GameObject gameObject) : base(gameObject) { }
    public override void OnEnter(PlayerController player)
    {
        if (player.State.GetType() == typeof(JumpingPlayerState))
        {
            Debug.Log("跳跃落地");
            player.State.SwitchTo(typeof(RunningPlayerState));
        }
    }
}

using UnityEngine;

public class HouseCollisionHandler : CollisionHandler
{
    public HouseCollisionHandler(GameObject gameObject) : base(gameObject) { }
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("房屋");
    }
}

public class DeadlyCollisionHandler : CollisionHandler
{
    public DeadlyCollisionHandler(GameObject gameObject) : base(gameObject) { }
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("致命");
        if (player.IsAlive)
        {
            player.Kill();
        }
    }
}

public class JumpCollisionHandler : CollisionHandler
{
    public JumpCollisionHandler(GameObject gameObject) : base(gameObject) { }
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("跳跃");
        if (player.IsAlive)
        {
            player.State.SwitchTo(typeof(JumpingPlayerState));
        }
    }
}

public class KillableCollisionHandler : CollisionHandler
{
    public KillableCollisionHandler(GameObject gameObject) : base(gameObject) { }
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("击杀");
        if (player.IsAlive)
        {
            Destroy(gameObject);
        }
    }
}

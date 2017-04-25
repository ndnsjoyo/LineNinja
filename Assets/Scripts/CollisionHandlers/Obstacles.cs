using UnityEngine;

public class HouseCollisionHandler : CollisionHandler
{
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("房屋");
    }
}

public class DeadlyCollisionHandler : CollisionHandler
{
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("致命");
        if (player.IsAlive)
        {
            player.OnDead();
        }
    }
}

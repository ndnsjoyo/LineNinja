using UnityEngine;

namespace CollisionHandler
{
    public class House : Handler
    {
        public House(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("房屋");
        }
    }

    public class Deadly : Handler
    {
        public Deadly(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("致命");
            if (player.IsAlive)
            {
                player.State.SwitchTo(typeof(PlayerState.Dead));
            }
        }
    }

    public class Jump : Handler
    {
        public Jump(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("跳跃");
            if (player.IsAlive)
            {
                player.State.SwitchTo(typeof(PlayerState.Jumping));
            }
        }
    }

    public class Killable : Handler
    {
        public Killable(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("击杀");
            if (player.IsAlive)
            {
                manager.Destroyed = true;
                Destroy(manager);
            }
        }
    }
}

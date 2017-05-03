using UnityEngine;

namespace CollisionHandler
{
    public class DashBreakable : Handler
    {
        public DashBreakable(CollisionHandlerManager manager) : base(manager) { }

        public override void OnEnter(PlayerController player)
        {
            if (player.State.GetType() == typeof(PlayerState.Dashing))
            {
                Debug.Log("冲刺摧毁障碍");

                manager.Destroy();
            }
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
                player.State.SwitchTo(new PlayerState.Dead(player));
            }
        }
    }

    public class SpringBoard : Handler
    {
        public SpringBoard(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("起跳器");
            if (player.IsAlive)
            {
                player.State.SwitchTo(new PlayerState.Jumping(player));
            }
        }
    }
}

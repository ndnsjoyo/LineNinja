using UnityEngine;

namespace CollisionHandler
{
    public class Pool : Handler
    {
        public Pool(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("水池 进入");
            if (player.IsAlive)
            {
                player.State.SwitchTo(typeof(PlayerState.InPool));
            }
        }

        public override void OnExit(PlayerController player)
        {
            Debug.Log("水池 离开");
            if (player.IsAlive)
            {
                player.State.SwitchTo(typeof(PlayerState.Running));
            }
        }
    }

    public class Bamboo : Handler
    {
        public Bamboo(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            if (player.IsAlive)
            {
                if (player.State.GetType() == typeof(PlayerState.Dashing))
                {
                    Debug.Log("刷新冲刺");
                    (player.State as PlayerState.Dashing).Refresh();
                }
            }
        }
    }
}

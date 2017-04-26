using UnityEngine;

namespace CollisionHandler
{
    public class Ground : Handler
    {
        public Ground(GameObject gameObject) : base(gameObject) { }
        public override void OnEnter(PlayerController player)
        {
            if (player.State.GetType() == typeof(PlayerState.Jumping))
            {
                Debug.Log("跳跃落地");
                player.State.SwitchTo(typeof(PlayerState.Running));
            }
        }
    }

    public class Pool : Handler
    {
        public Pool(GameObject gameObject) : base(gameObject) { }
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
}

using UnityEngine;

namespace CollisionHandler
{
    public class DashRefresher : Handler
    {
        public DashRefresher(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            if (player.IsAlive)
            {
                if (player.State.GetType() == typeof(PlayerState.Dashing))
                {
                    Debug.Log("Dash刷新器");
                    (player.State as PlayerState.Dashing).Refresh();
                }
            }
        }
    }

    public class Pool : Handler
    {
        public Pool(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("进入池塘");
            player.Speed *= 0.6f;
        }
        public override void OnExit(PlayerController player)
        {
            Debug.Log("离开池塘");
            player.Speed /= 0.6f;
        }
    }
}

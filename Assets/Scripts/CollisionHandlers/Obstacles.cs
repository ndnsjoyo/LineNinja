using UnityEngine;

namespace CollisionHandler
{
    public class House : Handler
    {
        public House(GameObject gameObject) : base(gameObject) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("房屋");
        }
    }

    public class Deadly : Handler
    {
        public Deadly(GameObject gameObject) : base(gameObject) { }
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
        public Jump(GameObject gameObject) : base(gameObject) { }
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
        public Killable(GameObject gameObject) : base(gameObject) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("击杀");
            if (player.IsAlive)
            {
                Destroy(gameObject);
            }
        }
    }
}

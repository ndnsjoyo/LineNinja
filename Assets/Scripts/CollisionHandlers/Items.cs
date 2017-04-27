using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Katana : Handler
    {
        public Katana(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("刀");
            if (!player.WithKatana)
            {
                player.WithKatana = true;

                manager.Destroy();
            }
        }
    }

    public class BambooSupp : Handler
    {
        public BambooSupp(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("补充竹子");

            manager.Destroy();
        }
    }

    public class ThunderSpirit : Handler
    {
        public ThunderSpirit(CollisionHandlerManager manager) : base(manager) { }

        public override void OnEnter(PlayerController player)
        {
            Debug.Log("雷灵");

            player.State.SwitchTo(new PlayerState.Dashing(player));

            manager.Destroy();
        }
    }
}
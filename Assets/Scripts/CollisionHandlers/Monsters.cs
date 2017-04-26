using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Monster : Handler
    {
        public Monster(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("怪兽");
            if (player.IsAlive && player.WithKatana)
            {
                player.WithKatana = false;
                manager.destroyed = true;
                Destroy(manager.managedObject);
            }
        }
    }
}
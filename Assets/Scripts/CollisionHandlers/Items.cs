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

                manager.destroyed = true;
                Destroy(manager.managedObject);
            }
        }
    }
}
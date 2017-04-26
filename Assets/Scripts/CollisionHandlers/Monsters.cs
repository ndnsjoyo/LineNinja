using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Monster : Handler
    {
        private ImageEffectController controller;
        public Monster(CollisionHandlerManager manager) : base(manager)
        {
            controller = GameObject.Find("_Manager").GetComponent<ImageEffectController>();
        }

        public override void OnEnter(PlayerController player)
        {
            Debug.Log("怪兽");
            if (player.IsAlive && player.WithKatana)
            {
                player.WithKatana = false;

                controller.OnChromatic(1.0f, 0.5f);

                manager.Destroyed = true;
                Destroy(manager.managedObject);
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
                Destroy(manager.managedObject);
            }
        }
    }

    public class MistMonster : Handler
    {
        private ImageEffectController controller;
        public MistMonster(CollisionHandlerManager manager) : base(manager)
        {
            controller = GameObject.Find("_Manager").GetComponent<ImageEffectController>();
        }

        public override void OnEnter(PlayerController player)
        {
            Debug.Log("迷雾兽");
            if (player.IsAlive)
            {
                controller.OnMist(1.0f);
            }
        }
    }
}
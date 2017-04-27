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
            Debug.Log("怪物");
            if (player.IsAlive && player.WithKatana)
            {
                // 用掉刀
                player.WithKatana = false;

                // 加点特技
                controller.OnChromatic(1.0f, 0.5f);

                manager.Destroy();
            }
        }
    }

    public class Killable : Handler
    {
        public Killable(CollisionHandlerManager manager) : base(manager) { }
        public override void OnEnter(PlayerController player)
        {
            Debug.Log("可击杀");
            if (player.IsAlive)
            {
                manager.Destroy();
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
                // 特技
                controller.OnMist(1.0f);
            }
        }
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Monster : Handler
    {
        public Monster(CollisionHandlerManager manager) : base(manager) { }

        public override void OnEnter(PlayerController player)
        {
            Debug.Log("怪物");
            if (player.IsAlive && player.WithKatana)
            {
                // 用掉刀
                player.WithKatana = false;

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
            controller = GameObject.Find("_Managers").GetComponent<ImageEffectController>();
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
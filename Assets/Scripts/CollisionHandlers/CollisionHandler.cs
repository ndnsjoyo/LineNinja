using System;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Handler : UnityEngine.Object
    {
        protected CollisionHandlerManager manager;
        public Handler(CollisionHandlerManager manager)
        {
            this.manager = manager;
        }

        public virtual void OnEnter(PlayerController player) { }
        public virtual void OnStay(PlayerController player) { }
        public virtual void OnExit(PlayerController player) { }
    }

}
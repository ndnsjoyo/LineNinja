using System;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class Handler : UnityEngine.Object
    {
        protected GameObject gameObject;
        public Handler(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public virtual void OnEnter(PlayerController player) { }
        public virtual void OnStay(PlayerController player) { }
        public virtual void OnExit(PlayerController player) { }
    }

}
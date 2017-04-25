using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : UnityEngine.Object
{
    protected GameObject gameObject;
    public CollisionHandler(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

    public virtual void OnEnter(PlayerController player) { }
    public virtual void OnStay(PlayerController player) { }
    public virtual void OnExit(PlayerController player) { }
}

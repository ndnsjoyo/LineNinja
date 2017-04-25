using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler
{
    public virtual void OnEnter(PlayerController player) { }
    public virtual void OnStay(PlayerController player) { }
    public virtual void OnExit(PlayerController player) { }
}

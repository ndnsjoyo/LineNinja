using System;
using System.Reflection;
using UnityEngine;

public class PlayerState : UnityEngine.Object
{
    static private Type[] stateCtorParamTypes;
    static PlayerState()
    {
        stateCtorParamTypes = new Type[] { typeof(PlayerController) };
    }

    public PlayerController player;
    public PlayerState(PlayerController player) { this.player = player; }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }

    public void SwitchTo(Type target)
    {
        Exit();

        Debug.Log("switch to " + target);

        ConstructorInfo stateCtor = target.GetConstructor(stateCtorParamTypes);
        PlayerState nextState = stateCtor.Invoke(new[] { player }) as PlayerState;

        player.State = nextState;

        nextState.Enter();
    }
}

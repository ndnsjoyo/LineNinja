using System;
using System.Reflection;
using UnityEngine;

namespace PlayerState
{
    public class State : UnityEngine.Object
    {
        static private Type[] stateCtorParamTypes;
        static State()
        {
            stateCtorParamTypes = new Type[] { typeof(PlayerController) };
        }

        public PlayerController player;
        public State(PlayerController player) { this.player = player; }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }

        public void SwitchTo(Type target)
        {
            Exit();

            UnityEngine.Debug.Log("switch to " + target);

            ConstructorInfo stateCtor = target.GetConstructor(stateCtorParamTypes);
            State nextState = stateCtor.Invoke(new[] { player }) as State;

            player.State = nextState;

            nextState.Enter();
        }
    }
}

using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class CollisionHandlerManager : MonoBehaviour
    {
        public GameObject managedObject;

        static private Type[] buffCtorParamTypes;
        static CollisionHandlerManager()
        {
            buffCtorParamTypes = new Type[] { typeof(GameObject) };
        }

        public string[] handlers;
        private List<Handler> _handlers;

        void Awake()
        {
            if (managedObject == null)
                managedObject = gameObject;

            _handlers = new List<Handler>();

            foreach (string handlerName in handlers)
            {
                Type buffType = Type.GetType("CollisionHandler." + handlerName);
                ConstructorInfo buffCtor = buffType.GetConstructor(buffCtorParamTypes);
                Handler handler = buffCtor.Invoke(new[] { managedObject }) as Handler;
                _handlers.Add(handler);
            }
        }

        void OnCollisionEnter(Collision other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnEnter(player);
                }
            }
        }

        void OnCollisionStay(Collision other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnStay(player);
                }
            }
        }

        void OnCollisionExit(Collision other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnExit(player);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnEnter(player);
                }
            }
        }

        void OnTriggerStay(Collider other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnStay(player);
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnExit(player);
                }
            }
        }
    }
}

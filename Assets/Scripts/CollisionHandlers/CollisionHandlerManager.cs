using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace CollisionHandler
{
    public class CollisionHandlerManager : MonoBehaviour
    {
        // 用于放在独立gameobject的探头collider
        // 若不指定则为挂载脚本的gameobject
        public GameObject managedObject;

        static private Type[] buffCtorParamTypes;
        static CollisionHandlerManager()
        {
            // handler构造函数参数类型列表
            buffCtorParamTypes = new Type[] { typeof(CollisionHandlerManager) };
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
                // 反射动态查找类型
                Type buffType = Type.GetType("CollisionHandler." + handlerName);
                if (buffType == null)
                {
                    Debug.Log("Cannot find handler class for " + handlerName);
                    continue;
                }

                // 获取构造函数并实例化
                ConstructorInfo buffCtor = buffType.GetConstructor(buffCtorParamTypes);
                Handler handler = buffCtor.Invoke(new[] { this }) as Handler;

                _handlers.Add(handler);
            }
        }

        // 中断处理后续handler
        private bool breaked = false;
        public void Destroy()
        {
            Destroy(managedObject);
            breaked = true;
        }

        void OnCollisionEnter(Collision other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                foreach (var buff in _handlers)
                {
                    buff.OnEnter(player);
                    if (breaked) break;
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
                    if (breaked) break;
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
                    if (breaked) break;
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
                    if (breaked) break;
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
                    if (breaked) break;
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
                    if (breaked) break;
                }
            }
        }
    }
}

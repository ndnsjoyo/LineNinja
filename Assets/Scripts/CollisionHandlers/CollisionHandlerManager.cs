﻿using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerManager : MonoBehaviour
{
    public string[] handlers;
    private List<CollisionHandler> _handlers;

    void Awake()
    {
        _handlers = new List<CollisionHandler>();

        foreach (string buffName in handlers)
        {
            Type buffType = Type.GetType(buffName + "CollisionHandler");
            CollisionHandler buff = Activator.CreateInstance(buffType) as CollisionHandler;
            _handlers.Add(buff);
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
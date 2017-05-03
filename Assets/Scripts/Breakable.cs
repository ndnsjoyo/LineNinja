using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject broken;

    void OnDestroy()
    {
        GameObject.Instantiate(broken, transform.position, transform.rotation);
    }
}

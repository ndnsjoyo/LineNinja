using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public void Activate()
    {
        transform.position = new Vector3(
            transform.position.x,
            0.5f,
            transform.position.z
        );
    }
}

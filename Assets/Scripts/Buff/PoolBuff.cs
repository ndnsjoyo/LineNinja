using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PoolBuff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("进入水池");
            player.Speed *= 0.6f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("离开水池");
            player.Speed /= 0.6f;
        }
    }
}

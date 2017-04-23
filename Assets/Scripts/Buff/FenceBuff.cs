using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FenceBuff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("栅栏跳跃");
            player.OnJump();
        }
    }
}

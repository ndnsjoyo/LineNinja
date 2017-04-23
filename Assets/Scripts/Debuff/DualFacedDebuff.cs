using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DualFacedDebuff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("双面兽");
            player.OnDead();
        }
    }
}

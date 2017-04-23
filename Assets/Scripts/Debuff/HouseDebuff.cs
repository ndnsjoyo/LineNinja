using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HouseDebuff : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("房屋");
            player.OnDead();
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Bouncer : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            player.Direction = -other.impulse;
        }
    }
}

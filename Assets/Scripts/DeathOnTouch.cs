using UnityEngine;

public class DeathOnTouch : MonoBehaviour
{
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
            player.OnDead();
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DualFacedBuff : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsAlive)
        {
            Debug.Log("击杀 双面兽");
            Destroy(transform.parent.gameObject);
        }
    }
}

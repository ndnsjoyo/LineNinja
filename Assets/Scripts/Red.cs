using UnityEngine;
using System.Collections;

public class Red : MonoBehaviour
{
    public Object breakdownTo;

    public int breakdownNumber = 8;
    public float breakdownDisplacement = 0.3f;
    public float breakdownSpeed = 3.0f;

    public float breakdownCountdown = 1.0f;
    public float breakdownTrigVelocity = 4.0f;

    private Rigidbody _rigidbody;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > breakdownTrigVelocity)
            StartCoroutine(breakDownAfter(breakdownCountdown));
    }

    IEnumerator breakDownAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (breakdownNumber == 1)
        {
            GameObject black = Instantiate(breakdownTo, transform.position, transform.rotation) as GameObject;
            black.GetComponent<Rigidbody>().velocity = _rigidbody.velocity;
        }
        else
        {
            GameObject[] blacks = new GameObject[breakdownNumber];
            for (int i = 0; i < breakdownNumber; i++)
            {
                Vector3 deltaDirection = Quaternion.Euler(0.0f, i * 360.0f / breakdownNumber, 0.0f) * Vector3.forward;

                GameObject black = Instantiate(
                    breakdownTo,
                    transform.position + deltaDirection * breakdownDisplacement,
                    transform.rotation) as GameObject;
                blacks[i] = black;

                Rigidbody rigidbody = black.GetComponent<Rigidbody>();
                Vector3 relativeVelocity = deltaDirection * breakdownSpeed;
                rigidbody.velocity = _rigidbody.velocity + relativeVelocity;
            }
        }
        Destroy(gameObject);
    }
}

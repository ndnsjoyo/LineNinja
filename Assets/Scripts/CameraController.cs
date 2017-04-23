using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 _cameraBias;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _cameraBias = transform.position;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Vector3 pos = new Vector3(0.0f, 0.0f, playerTransform.position.z);
        transform.position = pos + _cameraBias;
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;

    // 相机位移偏置    
    private Vector3 _cameraBias;

    void Start()
    {
        _cameraBias = transform.position;
    }

    void Update()
    {
        // 取玩家当前position的z分量
        Vector3 pos = new Vector3(0.0f, 0.0f, playerTransform.position.z);
        transform.position = pos + _cameraBias;
    }
}

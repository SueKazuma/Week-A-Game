using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform lookPos;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookPos);
    }
}

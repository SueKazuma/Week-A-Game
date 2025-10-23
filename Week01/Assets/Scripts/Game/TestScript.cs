using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float viewDistance = 10f;
    public int horizontalRayCount = 10;
    public int verticalRayCount = 5;
    public float raySpacing = 0.5f;
    public LayerMask detectableLayers;

    void Update()
    {
        ScanForwardPlane();
    }

    void ScanForwardPlane()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        // ���S���獶�E�E�㉺��Ray����ׂ�
        for (int y = 0; y < verticalRayCount; y++)
        {
            for (int x = 0; x < horizontalRayCount; x++)
            {
                // �O���b�h�̒��S����ɃI�t�Z�b�g���v�Z
                float offsetX = (x - horizontalRayCount / 2f) * raySpacing;
                float offsetY = (y - verticalRayCount / 2f) * raySpacing;

                // ���[�J�����W�ŃI�t�Z�b�g �� ���[���h���W�ɕϊ�
                Vector3 rayOrigin = origin + transform.right * offsetX + transform.up * offsetY;

                if (Physics.Raycast(rayOrigin, forward, out RaycastHit hit, viewDistance, detectableLayers))
                {
                    Debug.DrawRay(rayOrigin, forward * viewDistance, Color.red);
                    Debug.Log("Ray����������: " + hit.collider.name);
                }
                else
                {
                    Debug.DrawRay(rayOrigin, forward * viewDistance, Color.green);
                }
            }
        }
    }
}

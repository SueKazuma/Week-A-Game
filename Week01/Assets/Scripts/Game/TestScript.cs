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

        // 中心から左右・上下にRayを並べる
        for (int y = 0; y < verticalRayCount; y++)
        {
            for (int x = 0; x < horizontalRayCount; x++)
            {
                // グリッドの中心を基準にオフセットを計算
                float offsetX = (x - horizontalRayCount / 2f) * raySpacing;
                float offsetY = (y - verticalRayCount / 2f) * raySpacing;

                // ローカル座標でオフセット → ワールド座標に変換
                Vector3 rayOrigin = origin + transform.right * offsetX + transform.up * offsetY;

                if (Physics.Raycast(rayOrigin, forward, out RaycastHit hit, viewDistance, detectableLayers))
                {
                    Debug.DrawRay(rayOrigin, forward * viewDistance, Color.red);
                    Debug.Log("Rayが当たった: " + hit.collider.name);
                }
                else
                {
                    Debug.DrawRay(rayOrigin, forward * viewDistance, Color.green);
                }
            }
        }
    }
}

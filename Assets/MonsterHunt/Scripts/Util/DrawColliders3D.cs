using UnityEngine;

public class DrawColliders3D : MonoBehaviour
{
    public Color colliderColor = Color.blue;

    void OnDrawGizmos()
    {
        Collider[] colliders = GetComponents<Collider>() ?? GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            Gizmos.color = colliderColor;

            // Tính toán ma trận biến đổi của collider
            Matrix4x4 colliderMatrix = Matrix4x4.TRS(collider.transform.position, collider.transform.rotation, collider.transform.lossyScale);

            // Kiểm tra loại của Collider và vẽ tương ứng
            if (collider is BoxCollider)
            {
                BoxCollider boxCollider = collider as BoxCollider;
                Matrix4x4 boxMatrix = colliderMatrix * Matrix4x4.TRS(boxCollider.center, Quaternion.identity, Vector3.one);
                Gizmos.matrix = boxMatrix;
                Gizmos.DrawWireCube(Vector3.zero, boxCollider.size);
            }
            else if (collider is SphereCollider)
            {
                SphereCollider sphereCollider = collider as SphereCollider;
                Matrix4x4 sphereMatrix = colliderMatrix * Matrix4x4.TRS(sphereCollider.center, Quaternion.identity, Vector3.one);
                Gizmos.matrix = sphereMatrix;
                Gizmos.DrawWireSphere(Vector3.zero, sphereCollider.radius);
            }
            else if (collider is CapsuleCollider)
            {
                CapsuleCollider capsuleCollider = collider as CapsuleCollider;
                Vector3 start = Vector3.up * (capsuleCollider.height / 2 - capsuleCollider.radius) + capsuleCollider.center;
                Vector3 end = Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius) + capsuleCollider.center;
                Matrix4x4 capsuleMatrix = colliderMatrix * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
                Gizmos.matrix = capsuleMatrix;
                Gizmos.DrawWireSphere(start, capsuleCollider.radius);
                Gizmos.DrawWireSphere(end, capsuleCollider.radius);
                Gizmos.DrawLine(start, end);
            }
            else if (collider is MeshCollider)
            {
                // Đối với MeshCollider, vẽ bounding box của mesh
                MeshCollider meshCollider = collider as MeshCollider;
                Matrix4x4 meshMatrix = colliderMatrix;
                Gizmos.matrix = meshMatrix;
                Gizmos.DrawWireMesh(meshCollider.sharedMesh, Vector3.zero);
            }
            else
            {
                // Vẽ bounding box mặc định cho các loại Collider3D không được xử lý trước
                Bounds bounds = collider.bounds;
                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
    }
}

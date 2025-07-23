using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef (örneğin araç)
    public float smoothSpeed = 0.125f; // Kameranın hareket yumuşaklığı
    public Vector3 offset = new Vector3(0, 5, -10); // Araç local space'e göre offset
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Offset'i aracın yönüne göre döndür
        Vector3 desiredPosition = target.TransformPoint(offset); // Local offset world'e çevrildi

        // Kamerayı Smooth şekilde yeni pozisyona getir
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // Kameranın hedefe bakmasını sağla
        transform.LookAt(target);
    }

}

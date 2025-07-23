using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 activeLimit = new Vector2(-30, 30);
    public float sensitivity = 1f;
    public float smooth = 0.1f;
    public Transform verticalRotateTransform;
    public Transform horizontalRotateTransform;
    private Vector3 targetEulerAngles;
    private Vector3 lastEulerAngles;
    private Vector3 rotationSmoothVelocity;

    public Transform cameraTarget;
    public float cameraTargetMaxLimit = 10f;
    public float cameraTargetMinLimit = 2f;
    public float cameraSmoothing = 0.1f;
    public float sphereCastRadius = 0.5f;
    public Vector3 cameraDifference => cameraTarget.position - horizontalRotateTransform.position;


    public Vector3 offset;

    private Vector2 input => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    void FixedUpdate()
    {
        transform.position = target.position + offset;

        if (input != Vector2.zero)
        {
            float dynamicMultiplier = Mathf.Lerp(.5f, 1.5f, input.magnitude / 20); // / canvas.scaleFactor bu kismi incelemek lazim

            targetEulerAngles.x -= sensitivity * dynamicMultiplier * input.y;
            targetEulerAngles.y += sensitivity * dynamicMultiplier * input.x;

            targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, activeLimit.x, activeLimit.y);
        }

        lastEulerAngles = Vector3.SmoothDamp(lastEulerAngles, targetEulerAngles, ref rotationSmoothVelocity, smooth);
        // verticalRotateTransform.eulerAngles = lastEulerAngles;
        //     float target = cameraTargetMaxLimit;

        //     Vector3 A = horizontalRotateTransform.position + Vector3.up / 2;
        //     Vector3 B = cameraTarget.position;

        //     Vector3 dir = B - A;

        //     // if (Physics.SphereCast(A, sphereCastRadius, dir, out RaycastHit hit, cameraDifference.magnitude))
        //     // {
        //     //     if (hit.collider.GetComponent<CameraAvoidObstacle>())
        //     //         target = hit.distance - cameraTargetMinLimit;
        //     // }

        //     if (target < cameraTargetMinLimit)
        //         target = cameraTargetMinLimit;

        //     Vector3 cameraTargetPos = cameraDifference;
        //     cameraTargetPos.z = -target;

        //     cameraTarget.localPosition = Vector3.Lerp(cameraTarget.localPosition, cameraTargetPos, cameraSmoothing * Time.fixedDeltaTime);
        verticalRotateTransform.localEulerAngles = new Vector3(lastEulerAngles.x, lastEulerAngles.y, 0);
        // horizontalRotateTransform.localEulerAngles = new Vector3(0, , 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 minValue, maxValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 desiredPos = lookAt.TransformPoint(offset);
        Vector3 clampPosition = new Vector3(
            Mathf.Clamp(desiredPos.x, minValue.x, maxValue.x),
            Mathf.Clamp(desiredPos.y, minValue.y, maxValue.y),
            Mathf.Clamp(desiredPos.z, minValue.z, maxValue.z)
        );

        Vector3 smoothPos = Vector3.Lerp(
            transform.position,
            clampPosition,
            smoothSpeed*Time.deltaTime);

        transform.position = smoothPos;
        transform.LookAt(lookAt);
    }
}

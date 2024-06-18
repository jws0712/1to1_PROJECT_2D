using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("CameraInfo")]
    [SerializeField] private float followSpeed = default;
    [SerializeField] private float yOffset = default;
    [SerializeField] private Transform followTarget = default;
    [SerializeField] private float minWorldSize = default;
    [SerializeField] private float maxWorldSize = default;
    


    private void LateUpdate()
    {
        if(followTarget != null)
        {
            Vector3 newPos = new Vector3(followTarget.position.x, 0 + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
            float Xpos = transform.position.x;
            Xpos = Mathf.Clamp(transform.position.x, minWorldSize, maxWorldSize);
            transform.position = new Vector3(Xpos, 0 + yOffset, -10f);
        }
    }
}

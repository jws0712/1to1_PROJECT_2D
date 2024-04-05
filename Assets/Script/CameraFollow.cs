using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private Transform player;
    Vector3 cameraPos;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        cameraPos = player.position + (mousePos - player.position) * (5f / 20f);

        Camera.transform.position = new Vector3(cameraPos.x, cameraPos.y, Camera.transform.position.z);
    }
}

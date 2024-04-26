using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera MainCamera;
    [Header("PlayerPosition")]
    [SerializeField] private Transform PlayerPos;

    private Vector3 FollowPoint;
    private Vector3 mousePos;
    void LateUpdate()
    {
        mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        FollowPoint = PlayerPos.position + (mousePos - PlayerPos.position) * (1f/3f);

        MainCamera.transform.position = new Vector3(FollowPoint.x, FollowPoint.y, mousePos.z);
    }
}

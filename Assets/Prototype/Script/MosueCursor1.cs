using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor1 : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject PlayerCursor;
    private Vector2 mousePos;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        PlayerCursor.transform.position = mousePos;
    }
}

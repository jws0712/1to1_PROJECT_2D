using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public static ShootManager Instance = null;
    
    [SerializeField] private Camera Camera;
    private Vector3 mousePos;
    public float rotZ;

    private void Awake()
    {
        Instance = this;
    }
    

    
    void Update()
    {
        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}

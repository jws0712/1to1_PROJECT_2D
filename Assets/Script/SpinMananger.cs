using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMananger : MonoBehaviour
{
    public static SpinMananger Instance = null;
    [SerializeField] private Camera Camera;
    public float rotZ;
    private Vector3 mousePos;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMananger : MonoBehaviour
{
    public static SpinMananger Instance = null;
    [SerializeField] private Camera Camera;
    public float rotZ;
    private Vector3 mousePos;
    public GameObject Hand;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Hand = GameObject.Find("Hand").gameObject;
    }

    
    void Update()
    {
        Hand.transform.rotation = Quaternion.Euler(0, 0, SpinMananger.Instance.rotZ);

        mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rot = mousePos - transform.position;

        rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
    }
}

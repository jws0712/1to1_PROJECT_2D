using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public static ShootManager Instance;
    public GameObject Hand;
    //[SerializeField] private Camera Camera;
    //private Vector3 mousePos;
    //public float rotZ;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hand = GameObject.Find("Hand").gameObject;
    }



    void Update()
    {
        //mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 rot = mousePos - Hand.transform.position;

        //rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        Hand.transform.rotation = Quaternion.Euler(0, 0, SpinMananger.Instance.rotZ);
    }
}

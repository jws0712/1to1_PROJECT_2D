using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public static ShootManager Instance;
    public GameObject Hand;


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
        Hand.transform.rotation = Quaternion.Euler(0, 0, SpinMananger.Instance.rotZ);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public static ShootManager Instance;
    public GameObject FirePos;
    [SerializeField] private float cooltime;
    [SerializeField] private GameObject Bullet;
    private float currentTime;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentTime = cooltime;
        FirePos = GameObject.Find("FirePos").gameObject;
    }



    void Update()
    {
        FirePos.transform.rotation = Quaternion.Euler(0, 0, SpinMananger.Instance.rotZ);

       if(currentTime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                Debug.Log("น฿ป็");
            }
            currentTime = cooltime;
        }
        currentTime -= Time.deltaTime;
        
    }
}

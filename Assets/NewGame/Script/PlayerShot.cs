using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.XR;

public class PlayerShot : MonoBehaviour
{
    public static PlayerShot instance = null;

    [Header("Camera")]
    [SerializeField] private Camera MainCamera;
    [Header("Positions")]
    [SerializeField] private Transform HandPos;
    [SerializeField] private Transform FirePos;
    [SerializeField] private Transform SpinPos;
    [Header("Bullet")]
    [SerializeField] private GameObject Bullet;
    [Header("CoolTime")]
    [SerializeField] private float coolTime;
    private float currentTime = 0f;
    [Header("Spin")]
    public float RotZ;

    private Vector3 mousePos;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        HandPos = GameObject.FindWithTag("HandPos").transform;
        FirePos = GameObject.FindWithTag("FirePos").transform;
        SpinPos = GameObject.FindWithTag("SpinPos").transform;
    }
    private void Update()
    {
        Spin();
        Shot();
        FilpX();
    }

    private void Spin()
    {
        mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - SpinPos.position;

        RotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        HandPos.transform.rotation = Quaternion.Euler(0, 0, RotZ);
    }
    private void Shot()
    {
        if (currentTime <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
                Debug.Log("น฿ป็");
            }
            currentTime = coolTime;
        }
        currentTime -= Time.deltaTime;
    }

    private void FilpX()
    {
        if (Mathf.Abs(RotZ) >= 120f)
        {

        }
        else if (Mathf.Abs(RotZ) >= 60f)
        {

        }
    }
}

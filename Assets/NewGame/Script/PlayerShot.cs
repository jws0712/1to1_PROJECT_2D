using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.XR;

public class PlayerShot : MonoBehaviour
{

    [Header("Camera")]
    [SerializeField] private Camera MainCamera;
    [Header("Positions")]
    public Transform HandPos;
    [SerializeField] private Transform FirePos;
    [SerializeField] private Transform SpinPos;
    [Header("Bullet")]
    [SerializeField] private GameObject Bullet;
    [Header("CoolTime")]
    [SerializeField] private float coolTime;
    [Header("Bullet Spread")]
    [SerializeField] private float maxSpreadAngle;
    [SerializeField] private float minSpreadAngle;
    [Header("Spin")]
    public float RotZ;

    private Quaternion BulletAngle;
    private float currentTime = 0f;
    private Vector3 mousePos;

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
                float SperadAngle = FirePos.rotation.w + Random.Range(minSpreadAngle, maxSpreadAngle);
                BulletAngle = new Quaternion(FirePos.rotation.x, FirePos.rotation.y, FirePos.rotation.z, SperadAngle);
                Instantiate(Bullet, FirePos.transform.position, BulletAngle);
                Debug.Log(FirePos.rotation);
            }
            currentTime = coolTime;
        }
        currentTime -= Time.deltaTime;
    }
}

namespace OTO.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    //UnityEngine
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
        private Vector3 mousePos;
        private bool IsShot = false;

        private void Start()
        {
            HandPos = GameObject.FindWithTag("HandPos").transform;
            FirePos = GameObject.FindWithTag("FirePos").transform;
            SpinPos = GameObject.FindWithTag("SpinPos").transform;
        }
        private void Update()
        {
            Spin();
            Shoot();
        }

        private void Spin()
        {
            mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - SpinPos.position;

            RotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            HandPos.transform.rotation = Quaternion.Euler(0, 0, RotZ);
        }
        private void Shoot()
        {
            if (Input.GetButton("Fire1") && IsShot == false)
            {

                StartCoroutine(Shooting());
            }
        }

        private IEnumerator Shooting()
        {
            float SpreadAngle = RotZ + Random.Range(minSpreadAngle, maxSpreadAngle);
            BulletAngle = Quaternion.Euler(0, 0, SpreadAngle);
            Instantiate(Bullet, FirePos.transform.position, BulletAngle);
            IsShot = true;
            yield return new WaitForSeconds(coolTime);
            IsShot = false;
        }
    }
}

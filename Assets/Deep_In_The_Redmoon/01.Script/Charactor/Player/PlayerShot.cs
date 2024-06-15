namespace OTO.Charactor.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.XR;

    //Project
    using OTO.Bullet;

    public class PlayerShot : MonoBehaviour
    {

        [Header("Camera")]
        [SerializeField] private Camera MainCamera = default;

        [Header("Positions")]
        public Transform HandPos;
        [SerializeField] private Transform FirePos = default;
        [SerializeField] private Transform SpinPos = default;

        [Header("Bullet")]
        [SerializeField] private GameObject Bullet = default;
        [SerializeField] private float bulletDamage = default;

        [Header("CoolTime")]
        [SerializeField] private float coolTime = default;
        [Header("Bullet Spread")]
        [SerializeField] private float maxSpreadAngle = default;
        [SerializeField] private float minSpreadAngle = default;
        [Header("Spin")]
        public float RotZ = default;
        [Header("CameraShake")]
        [SerializeField] private float shakePower = default;
        [Header("Script")]
        [SerializeField] private PlayerMovement playerMovement = null;

        private Quaternion bulletAngle = default;
        private Vector3 mousePos = default;
        private bool isShot = default;

        private void Start()
        {
            isShot = false;
        }
        private void Update()
        {
            HandPos = GameObject.FindWithTag("HandPos").transform;
            FirePos = GameObject.FindWithTag("FirePos").transform;
            SpinPos = GameObject.FindWithTag("SpinPos").transform;

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
            if (playerMovement.isDash)
            {
                return;
            }

            if (Input.GetButton("Fire1") && isShot == false)
            {
                StartCoroutine(Co_Shooting());
            }
        }

        private IEnumerator Co_Shooting()
        {
            float SpreadAngle = RotZ + Random.Range(minSpreadAngle, maxSpreadAngle);
            bulletAngle = Quaternion.Euler(0, 0, SpreadAngle);
            GameObject _bullet = Instantiate(Bullet, FirePos.transform.position, bulletAngle);
            _bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
            StartCoroutine(Co_CameraShake(shakePower));
            isShot = true;
            yield return new WaitForSeconds(coolTime);
            isShot = false;
        }

        private IEnumerator Co_CameraShake(float ShakeIntensity)
        {
            CameraShakeManager.instance.ShakeCamera(ShakeIntensity);
            yield return new WaitForSeconds(0.1f);
            CameraShakeManager.instance.StopShake();
        }
    }
}

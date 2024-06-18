namespace OTO.Charactor.Player
{
    //System
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.XR;
    using UnityEngine.UI;

    //Project
    using OTO.Bullet;
    using TMPro;
    using OTO.Manager;

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
        [SerializeField] private float maxAmmo = default;

        [Header("CoolTime")]
        [SerializeField] private float fireCoolTime = default;
        [SerializeField] private float reloadCoolTime = default;
        [Header("Bullet Spread")]
        [SerializeField] private float maxSpreadAngle = default;
        [SerializeField] private float minSpreadAngle = default;
        [Header("Spin")]
        public float RotZ = default;
        [Header("Script")]
        [SerializeField] private PlayerMovement playerMovement = null;
        [Header("UI")]
        [SerializeField] private Slider reroadTimeSlider = null;
        [SerializeField] private TextMeshProUGUI ammoText = null;

        private Quaternion bulletAngle = default;
        private Vector3 mousePos = default;
        private bool isShot = default;
        private float currentAmmo = default;
        private float currentReroadCoolTIme = default;
        private bool isReload = default;

        private void Start()
        {
            isShot = false;
            currentReroadCoolTIme = 0;
            currentAmmo = maxAmmo;
        }
        private void Update()
        {
            if(GameManager.instance.isGameOver == true)
            {
                return;
            }

            HandPos = GameObject.FindWithTag("HandPos").transform;
            FirePos = GameObject.FindWithTag("FirePos").transform;
            SpinPos = GameObject.FindWithTag("SpinPos").transform;

            reroadTimeSlider.gameObject.SetActive(false);
            ammoText.text = currentAmmo.ToString() + " / ¡Ä";
            reroadTimeSlider.value = currentReroadCoolTIme / reloadCoolTime;

            Spin();
            Shoot();
            Reload();
        }

        private void Reload()
        {
            if (!isShot && currentAmmo != maxAmmo && Input.GetKeyDown(KeyCode.R) && isReload == false && playerMovement.isDash == false)
            {
                isReload = true;
                StartCoroutine(Co_Reload());
            }
        }

        private IEnumerator Co_Reload()
        {
            while(true)
            {
                reroadTimeSlider.gameObject.SetActive(true);
                currentReroadCoolTIme += Time.deltaTime;
                if (currentReroadCoolTIme >= reloadCoolTime)
                {
                    currentAmmo = maxAmmo;
                    currentReroadCoolTIme = 0;
                    isReload = false;

                    yield break;
                }
                yield return null;
            }
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

            if (Input.GetButton("Fire1") && isShot == false && currentAmmo > 0)
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
            currentAmmo -= 1;
            isShot = true;
            yield return new WaitForSeconds(fireCoolTime);
            isShot = false;
        }
    }
}

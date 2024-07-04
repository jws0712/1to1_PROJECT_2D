namespace OTO.Charactor.Player
{
    //System
    using System.Collections;

    //UnityEngine
    using UnityEngine;
    using UnityEngine.XR;
    using UnityEngine.UI;

    //Project
    using OTO.Object;
    using TMPro;
    using OTO.Manager;

    /// <summary>
    /// 플레이어의 공격을 관리하는 클래스
    /// </summary>
    public class PlayerShot : MonoBehaviour
    {

        [Header("Camera")]
        [SerializeField] private Camera mainCamera = default;

        [Header("Positions")]
        public Transform HandPos;
        [SerializeField] private Transform firePos = default;
        [SerializeField] private Transform spinPos = default;

        [Header("Bullet")]
        [SerializeField] private GameObject bullet = default;
        [SerializeField] private GameObject gunSmoke = default;
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

        //private variables
        private Quaternion bulletAngle = default;
        private Vector3 mousePos = default;
        private float currentAmmo = default;
        private float currentReroadCoolTIme = default;
        private bool isShot = default;
        private bool isReload = default;

        /// <summary>
        /// 변수초기화
        /// </summary>
        private void Start()
        {
            isShot = false;
            currentReroadCoolTIme = 0;
            currentAmmo = maxAmmo;
            var canvas = GameObject.FindGameObjectWithTag("MainCanvas");
            ammoText = canvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            mainCamera = Camera.main;
        }

        /// <summary>
        /// 플레이어의 공격을 관리하는 코드
        /// </summary>
        private void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            HandPos = GameObject.FindGameObjectWithTag("HandPos").transform;
            firePos = GameObject.FindGameObjectWithTag("FirePos").transform;
            spinPos = GameObject.FindGameObjectWithTag("SpinPos").transform;

            reroadTimeSlider.gameObject.SetActive(false);
            ammoText.text = currentAmmo.ToString() + " / ∞";
            reroadTimeSlider.value = currentReroadCoolTIme / reloadCoolTime;

            Spin();
            Shoot();
            Reload();
        }

        /// <summary>
        /// 총을 재장전할때 실행되는 함수
        /// </summary>
        private void Reload()
        {
            if (!isShot && currentAmmo != maxAmmo && Input.GetKeyDown(KeyCode.R) && isReload == false && playerMovement.isDash == false)
            {
                AudioManager.instance.PlaySFX("Reload");
                isReload = true;
                StartCoroutine(Co_Reload());
            }
        }

        /// <summary>
        /// 총을 재장전하는 코루틴
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 마우스 코드
        /// </summary>
        private void Spin()
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Vector3 rotation = mousePos - spinPos.position;

            RotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            HandPos.transform.rotation = Quaternion.Euler(0, 0, RotZ);
        }

        /// <summary>
        /// 총알을 발사하는 함수
        /// </summary>
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

        /// <summary>
        /// 총알을 발사하는 코루틴
        /// </summary>
        /// <returns></returns>
        private IEnumerator Co_Shooting()
        {
            float SpreadAngle = RotZ + Random.Range(minSpreadAngle, maxSpreadAngle);
            bulletAngle = Quaternion.Euler(0, 0, SpreadAngle);
            AudioManager.instance.PlaySFX("GunFire");
            CameraShakeManager.instance.PlayShake("GunFire");
            GameObject _bullet = Instantiate(bullet, firePos.transform.position, bulletAngle);
            _bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
            Instantiate(gunSmoke, firePos.transform.position, bulletAngle);
            Animator animator = transform.GetChild(3).transform.GetChild(0).GetComponent<Animator>();
            animator.SetTrigger("Fire");

            

            currentAmmo -= 1;
            isShot = true;
            yield return new WaitForSeconds(fireCoolTime);
            isShot = false;
        }
    }
}

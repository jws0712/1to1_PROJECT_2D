namespace OTO.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 카메라가 타겟을 따라가게 하는 클래스
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("CameraInfo")]
        [SerializeField] private float followSpeed = default;
        [SerializeField] private float yOffset = default;
        [SerializeField] private float minWorldSize = default;
        [SerializeField] private float maxWorldSize = default;

        private GameObject followTarget = default;

        /// <summary>
        /// 팔로우 타겟을 가져오는 코드
        /// </summary>
        private void Update()
        {
            followTarget = GameObject.FindGameObjectWithTag("Player");
        }

        /// <summary>
        /// 팔로우 타겟을 카매라가 쫒아가게하는 함수
        /// </summary>
        private void LateUpdate()
        {

            if (followTarget == null)
            {
                return;
            }
            else
            {
                Vector3 newPos = new Vector3(followTarget.transform.position.x, 0 + yOffset, -10f);
                transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
                float Xpos = transform.position.x;
                Xpos = Mathf.Clamp(transform.position.x, minWorldSize, maxWorldSize);
                transform.position = new Vector3(Xpos, 0 + yOffset, -10f);
            }
        }
    }
}




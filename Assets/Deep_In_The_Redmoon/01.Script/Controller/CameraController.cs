namespace OTO.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// ī�޶� Ÿ���� ���󰡰� �ϴ� Ŭ����
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
        /// �ȷο� Ÿ���� �������� �ڵ�
        /// </summary>
        private void Update()
        {
            followTarget = GameObject.FindGameObjectWithTag("Player");
        }

        /// <summary>
        /// �ȷο� Ÿ���� ī�Ŷ� �i�ư����ϴ� �Լ�
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




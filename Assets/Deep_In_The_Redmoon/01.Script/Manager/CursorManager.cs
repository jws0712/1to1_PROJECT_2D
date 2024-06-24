namespace OTO.Manager
{
    //UnityEngine
    using UnityEngine;

    /// <summary>
    /// ���콺Ŀ���� �����ϴ� Ŭ����
    /// </summary>
    public class CursorManager : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera MainCamera;
        [Header("Cursor Design")]
        [SerializeField] GameObject CursorObject;

        private Vector2 mousePos;

        /// <summary>
        /// �ʱ� ���콺Ŀ���� �� ����
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

        }

        /// <summary>
        /// ���콺Ŀ�� ������Ʈ�� ���콺 ��ġ�� ������� �����ڵ�
        /// </summary>
        private void LateUpdate()
        {
            if(GameManager.instance.isGameOver == true)
            {
                Cursor.visible = true;
                Destroy(CursorObject);
            }

            mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            CursorObject.transform.position = mousePos;
        }
    }
}
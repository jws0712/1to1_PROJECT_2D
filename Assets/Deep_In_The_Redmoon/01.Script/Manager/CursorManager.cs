namespace OTO.Manager
{
    //UnityEngine
    using UnityEngine;

    public class CursorManager : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera MainCamera;
        [Header("Cursor Design")]
        [SerializeField] GameObject CursorObject;

        private Vector2 mousePos;

        /// <summary>
        /// 초기 마우스커서의 값 설정
        /// </summary>
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

        }

        /// <summary>
        /// 마우스커서 오브젝트가 마우스 위치를 따라오게 만든코드
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
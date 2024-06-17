namespace OTO.Cursor
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
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;

        }

        // Update is called once per frame
        void LateUpdate()
        {
            mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            CursorObject.transform.position = mousePos;
        }
    }
}
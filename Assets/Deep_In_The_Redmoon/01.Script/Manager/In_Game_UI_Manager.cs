using OTO.Manager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class In_Game_UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel = null;

    private bool isON = default;

    private void Start()
    {
        isON = false;
    }
    private void Update()
    {
        if(GameManager.instance.isGameOver == true)
        {
            settingPanel.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isON == false)
        {
            settingPanel.SetActive(true);
            Time.timeScale = 0;
            isON = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isON == true)
        {
            settingPanel.SetActive(false);
            Time.timeScale = 1;
            isON = false;
        }
    }
}

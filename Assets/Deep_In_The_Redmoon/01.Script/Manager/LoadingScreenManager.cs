using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using OTO.Manager;

/// <summary>
/// 로딩씬을 관리하는 클래스
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    static string nextScene = null;

    [SerializeField] private Image progreesBar = null;

    /// <summary>
    /// 로딩을 실행하는 코드
    /// </summary>
    private void Start()
    {
        AudioManager.instance.StopMusic();

        StartCoroutine(Co_LoadSceneProgrees());

        Time.timeScale = 1f;
    }

    /// <summary>
    /// 씬을 불러오는 함수
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    /// <summary>
    /// 로딩씬의 기능을 구현한 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Co_LoadSceneProgrees()
    {
       AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.1f)
            {
                progreesBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.deltaTime;
                progreesBar.fillAmount = Mathf.Lerp(0.1f, 1f, timer);
                if(progreesBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

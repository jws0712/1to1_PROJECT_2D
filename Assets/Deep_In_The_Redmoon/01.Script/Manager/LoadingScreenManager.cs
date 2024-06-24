using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �ε����� �����ϴ� Ŭ����
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    static string nextScene = null;

    [SerializeField] private Image progreesBar = null;

    /// <summary>
    /// �ε��� �����ϴ� �ڵ�
    /// </summary>
    private void Start()
    {
        StartCoroutine(Co_LoadSceneProgrees());

        Time.timeScale = 1f;
    }

    /// <summary>
    /// ���� �ҷ����� �Լ�
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    /// <summary>
    /// �ε����� ����� ������ �ڷ�ƾ
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

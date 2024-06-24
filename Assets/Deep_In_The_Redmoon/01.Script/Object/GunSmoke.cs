using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSmoke : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Co_DestroySmoke());
    }

    private IEnumerator Co_DestroySmoke()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}

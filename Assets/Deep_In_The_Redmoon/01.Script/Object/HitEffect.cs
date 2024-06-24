using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private float destoryTime = default;
    private void Start()
    {
        Invoke("DestroyEffect", destoryTime);
    }

    private void DestroyEffect()
    {
        Destroy(gameObject);
    }
    
}

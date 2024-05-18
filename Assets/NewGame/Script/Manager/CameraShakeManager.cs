using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance = null;

    private CinemachineVirtualCamera CinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        instance = this;

        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float ShakeIntensity)
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        Debug.Log("½ÇÇà");
    }

    public void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
    }



}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContloer : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera VirtualCamera;
	private CinemachinePOV m_cinemachinePov;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Awake()
	{
		m_cinemachinePov = VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
		// InputValueGain�ł�m_MaxSpeed�ɑ��
		float wishSens = 2;
		m_cinemachinePov.m_VerticalAxis.m_MaxSpeed = wishSens;
		m_cinemachinePov.m_HorizontalAxis.m_MaxSpeed = wishSens;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}

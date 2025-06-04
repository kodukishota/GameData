using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer: MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI TimeText;
	[SerializeField] private GameObject TimeUpText;
	[SerializeField] private AudioSource EndSE;

	[SerializeField] private static float GameTime = 120;		//ÉQÅ[ÉÄÇÃéûä‘
	private static float TimeUpIntarval = 3;	//ÉQÅ[ÉÄÇ™èIÇÌÇ¡ÇΩå„ÇÃresultâÊñ Ç…çsÇ≠ä‘

	float m_gameTime;
	bool m_gameEnd;

    // Start is called before the first frame update
    void Start()
    {
		m_gameTime = GameTime;

		TimeUpText.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        m_gameTime -= Time.deltaTime;

		if(!m_gameEnd)
		{
			TimeText.SetText(m_gameTime.ToString("F0") + "ïb");

			if (m_gameTime <= 0)
			{
				
				m_gameEnd = true;

				m_gameTime = TimeUpIntarval;
				TimeUpText.SetActive(true);
				EndSE.Play();
			}
		}

		if(m_gameEnd)
		{
			if(m_gameTime <= 0)
			{
				SceneManager.LoadScene("SceneResult");
			}
		}
    }

	public float GetGameTime()
	{
		return m_gameTime;
	}

	public bool GetGameEnd()
	{
		return m_gameEnd;
	}
}

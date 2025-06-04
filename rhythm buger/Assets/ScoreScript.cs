using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
	[SerializeField] private CheckBar checkBar;
	[SerializeField] private TextMeshProUGUI Text;
	[SerializeField] PlayerContoloer playerContoloer;
	[SerializeField] GameTimer gameTimer;

	static int m_score;

	bool m_onBurgerTray;
	int m_burgerPoint = 0;

	bool m_onSideTray;
	int m_sidePoint = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_onBurgerTray = playerContoloer.GetOnBurger();
		m_burgerPoint = checkBar.GetBurgerPoint;

		m_onSideTray = playerContoloer.GetOnSide();
		m_sidePoint = checkBar.GetSidePoint;

		bool gameEnd = gameTimer.GetGameEnd();

		Text.SetText("ÉXÉRÉA" + m_score);

		if(!gameEnd)
		{
			if (m_onBurgerTray)
			{
				m_score += 10 * m_burgerPoint;

				m_burgerPoint = 0;
				checkBar.GetBurgerPoint = m_burgerPoint;
			}

			if (m_onSideTray)
			{
				m_score += 10 * m_sidePoint;

				m_sidePoint = 0;
				checkBar.GetSidePoint = m_sidePoint;
			}
		
		}
	}

	public static int GetScore()
	{
		return m_score;
	}
}

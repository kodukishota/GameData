using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBar : MonoBehaviour
{
	[SerializeField] private Transform checkBar;
	[SerializeField] private Transform MidHandle;
	[SerializeField] private GameObject Handle;
	[SerializeField] private Transform RightHandle;
	[SerializeField] private Transform LeftHandle;

	[SerializeField] private Transform GoodHandle;
	[SerializeField] private Transform GreadHandle;

	bool m_spawnTime = true;

	[SerializeField] PlayerContoloer playerContoloer;

	string m_burgerAward;
	bool m_isTouchBurger;
	bool m_isTouchBurgerEnd;
	int m_burgerPoint;
	int m_endBurgerPoint;

	string m_sideAward;
	bool m_isTouchSide;
	bool m_isTouchSideEnd;
	int m_sidePoint;
	int m_endSidePoint;

	AudioSource m_audioSource;

	// Start is called before the first frame update
	void Start()
    {
		m_audioSource = GetComponent<AudioSource>();
	}

	public int GetBurgerPoint
	{
		get { return m_endBurgerPoint; }
		set { m_endBurgerPoint = value;}
	}

	public int GetSidePoint
	{
		get { return m_endSidePoint; }
		set { m_endSidePoint = value;}
	}


    // Update is called once per frame
    void Update()
    {
		m_isTouchBurger = playerContoloer.GetTouchBurgerBool();
		m_isTouchBurgerEnd = playerContoloer.GetTouchBurgerEnd();
		m_isTouchSide = playerContoloer.GetTouchSideBool();
		m_isTouchSideEnd = playerContoloer.GetTouchSideEnd();
		
		//ハンバーガーを作り終えたらポイントを保存
		if(m_isTouchBurgerEnd)
		{
			m_endBurgerPoint = m_burgerPoint;

			m_burgerPoint = 0;
		}

		if(m_isTouchSideEnd)
		{
			m_endSidePoint = m_sidePoint;

			m_sidePoint = 0;
		}

		if(m_burgerAward != null)
		{
			if (m_burgerAward == "Perfect")
			{
				m_burgerPoint += 3;
			}
			if (m_burgerAward == "Gread")
			{
				m_burgerPoint += 2;
			}
			if (m_burgerAward == "Good")
			{
				m_burgerPoint += 1;
			}
			if (m_burgerAward == "Bad")
			{
				m_burgerPoint -= 1;
			}

			m_burgerAward = null;
		}

		if(m_sideAward != null)
		{
			if (m_sideAward == "Perfect")
			{
				m_sidePoint += 3;
			}
			if (m_sideAward == "Gread")
			{
				m_sidePoint += 2;
			}
			if (m_sideAward == "Good")
			{
				m_sidePoint += 1;
			}
			if (m_sideAward == "Bad")
			{
				m_sidePoint -= 1;
			}

			m_sideAward = null;
		}

		//ハンドルのスポーン
		if (m_spawnTime)
		{
			Instantiate(Handle,
				RightHandle.position,
				Quaternion.identity,
				RightHandle);

			Instantiate(Handle,
				LeftHandle.position,
				Quaternion.identity,
				LeftHandle);

			m_spawnTime = false;			
		}

		Transform[] m_rightHandle = RightHandle.GetComponentsInChildren<Transform>();
		Transform[] m_leftHandle = LeftHandle.GetComponentsInChildren<Transform>();

		//右のハンドル
		for (int i = 1; i < m_rightHandle.Length; i++)
		{
			if (m_rightHandle[i].position.x >= MidHandle.position.x)
			{
				m_rightHandle[i].position -= new Vector3(5f, 0, 0);
			}
			else
			{
				m_audioSource.Play();

				Destroy(m_rightHandle[i].gameObject);
				m_spawnTime = true;
			}
			//ハンバーガーを作るときの判定
			if (m_rightHandle[i].position.x <= MidHandle.position.x + 3 && m_isTouchBurger)
			{
				m_burgerAward = "Perfect";
			}
			else if (m_rightHandle[i].position.x <= GreadHandle.position.x && m_isTouchBurger)
			{
				m_burgerAward = "Gread";
			}
			else if (m_rightHandle[i].position.x <= GoodHandle.position.x && m_isTouchBurger)
			{
				m_burgerAward = "Good";
			}
			else if (m_rightHandle[i].position.x >= GoodHandle.position.x && m_isTouchBurger)
			{
				m_burgerAward = "Bad";
			}
		}

		//左のハンドル
		for (int i = 1; i < m_leftHandle.Length; i++)
		{			
			if (m_leftHandle[i].position.x <= MidHandle.position.x)
			{
				m_leftHandle[i].position += new Vector3(5f, 0, 0);
			}
			else
			{
				Destroy(m_leftHandle[i].gameObject);
			}
			//サイドメニューの判定
			if (m_rightHandle[i].position.x <= MidHandle.position.x + 3 && m_isTouchSide)
			{
				m_sideAward = "Perfect";
			}
			else if (m_rightHandle[i].position.x <= GreadHandle.position.x && m_isTouchSide)
			{
				m_sideAward = "Gread";
			}
			else if (m_rightHandle[i].position.x <= GoodHandle.position.x && m_isTouchSide)
			{
				m_sideAward = "Good";
			}
			else if (m_rightHandle[i].position.x >= GoodHandle.position.x && m_isTouchSide)
			{
				m_sideAward = "Bad";
			}
		}
	}
}

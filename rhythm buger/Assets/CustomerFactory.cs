using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomerFactory : MonoBehaviour
{
	[SerializeField] private GameObject[] CustomerPrefab;
	[SerializeField] private Transform Canvas;
	[SerializeField] private GameTimer gameTimer;

	static Vector3[] SpownPos = { new Vector3(15,0,0),new Vector3(12.5f,0,0) };
	static float SpownIntervalTime = 15;

	float m_spownTime = 0;
	float m_gameTime = 0;
	int m_customerNumder;
	int m_spawnPosNumder;

	// Start is called before the first frame update
	void Start()
    {
		m_customerNumder = Random.Range(0,CustomerPrefab.Length - 1);
		m_spawnPosNumder = Random.Range(0, SpownPos.Length);
    }

    // Update is called once per frame
    void Update()
    {
		m_spownTime -= Time.deltaTime;
		m_gameTime = gameTimer.GetGameTime();

		//スポーンタイムが0になったらスポーン
		if (m_spownTime <= 0)
		{
			GameObject customer = Instantiate(CustomerPrefab[m_customerNumder], SpownPos[m_spawnPosNumder], Quaternion.identity);
			
			CustomerScript customerScript = customer.GetComponent<CustomerScript>();
			customerScript.SetCanvas(Canvas);

			m_customerNumder = Random.Range(0, CustomerPrefab.Length - 1);
			m_spawnPosNumder = Random.Range(0, SpownPos.Length);

			//時間経過でスポーンタイムを縮める
			if(m_gameTime <= 120)
			{
				m_spownTime = SpownIntervalTime;
			}
			else if(m_gameTime <= 90)
			{
				m_spownTime = SpownIntervalTime - 3; 
			}
			else if(m_gameTime <= 60)
			{
				m_spownTime = SpownIntervalTime - 5;
			}
			else if(m_gameTime <= 30)
			{
				m_spownTime = SpownIntervalTime - 8;
			}
		}
		//4人待っていたらスポーンさせるのを止める
		if (GameObject.FindWithTag("Wait"))
		{
			m_spownTime += Time.deltaTime;
		}
	}
}
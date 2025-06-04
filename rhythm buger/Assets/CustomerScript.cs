using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CustomerScript : MonoBehaviour
{
	[SerializeField] private GameObject Tray;
	[SerializeField] private Transform Canvas;
	[SerializeField] private TextMeshProUGUI WanigText;
	[SerializeField] private GameObject Image;

	string[] BugerMenu = { "チーズなしバーガー", "チーズバーガー", "オリジナルバーガー", "野菜バーガー", "トマトバーガー" };
	string[] SideMenu = { "ポテト", "ナゲット", "ホットドック", "クロワッサン" };
	string[] DrinkMenu = { null , "ドリンク" };

	string m_hopeBurger;
	string m_hopeSide;
	string m_hopeDrink;

	bool m_onTray = false;
	bool m_inOder = false;

	bool m_burgerCompletion = false;
	bool m_sideCompletion = false;
	bool m_drinkCompletion = false;

	int giveFoodCount;

	int targetIndex = 0;

	static Vector3[] Target = {new Vector3(-0.5f,0,-15.5f), new Vector3(1f, 0, -15.5f), new Vector3(2.5f, 0, -15.5f),new Vector3(1f,0,-14)};
	static Vector3[] NextTarget = { new Vector3(-9,0,-15.5f)};
	private NavMeshAgent Agent;

	GameObject m_tray;
	private TextMeshProUGUI[] m_text;
	private GameObject m_image;
	private TextMeshProUGUI m_wanigText;

	private Animator Anime;

	// Start is called before the first frame update
	void Start()
    {
		Agent = GetComponent<NavMeshAgent>();
		Anime = GetComponent<Animator>();

		m_image = Instantiate(Image, new Vector3(-1000,-1000,-1000), Quaternion.identity,Canvas);
		m_text = m_image.GetComponentsInChildren<TextMeshProUGUI>();

		WanigText.SetText("");

		m_wanigText = Instantiate(WanigText, new Vector3(-1000,-1000,-1000), Quaternion.identity,Canvas);

		int hopeBurgerNumder = Random.Range(0, BugerMenu.Length);
		m_hopeBurger = BugerMenu[hopeBurgerNumder];

		int hopeSide = Random.Range(0, SideMenu.Length);
		m_hopeSide = SideMenu[1];

		int hopeDrink = Random.Range(0, DrinkMenu.Length);
		m_hopeDrink = DrinkMenu[hopeDrink];

		if (m_hopeDrink != null)
		{
			giveFoodCount = 3;
		}
		else
		{
			giveFoodCount = 2;
		}
	}

    // Update is called once per frame
    void Update()
    {

		Debug.Log(m_sideCompletion);
		SetDestination();

		//注文カウンターが空いていた場合
		if (targetIndex <= 2)
		{ 
			if(gameObject.transform.position.x == Target[targetIndex].x && gameObject.transform.position.z == Target[targetIndex].z) 
			{
				m_inOder = true;

				m_image.transform.position = Camera.main.WorldToScreenPoint(new Vector3(
				gameObject.transform.position.x + 0.4f, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z));

				m_text[0].transform.position = Camera.main.WorldToScreenPoint(new Vector3(
						gameObject.transform.position.x + 0.4f, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z));

				m_wanigText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(
						gameObject.transform.position.x, gameObject.transform.position.y + 1f, gameObject.transform.position.z));

				//トレイを置くのとほしいものをテキストで表示
				if (!m_onTray)
				{
					m_text[0].SetText(m_hopeBurger + "\n" + m_hopeSide + "\n" + m_hopeDrink);

					m_tray = Instantiate(
						Tray, 
						new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.3f, gameObject.transform.position.z + -2),
						Quaternion.identity);

					m_tray.name = "Tray";
					gameObject.tag = "OderCustomer";

					m_onTray = true;
				}

				Anime.SetBool("Walk", false);
				Transform[] givedFood = m_tray.GetComponentsInChildren<Transform>();

				for (int i = 1; i < givedFood.Length; i++)
				{
					//ほしいものが来たらカウントを引く
					//ほしい物と違うのが来たら消す
					if (givedFood[i].tag == "Burger" && !m_burgerCompletion)
					{
						if (givedFood[i].name == m_hopeBurger)
						{
							giveFoodCount--;
							m_burgerCompletion = true;
						}
						else
						{ 
							Destroy(givedFood[i].gameObject);

							WanigText.SetText("違います！");
						}
					}

					if (givedFood[i].tag == "Side" && !m_sideCompletion)
					{
						Debug.Log("!");
						if (givedFood[i].name == m_hopeSide)
						{
							giveFoodCount--;
							m_sideCompletion = true;
						}
						else
						{
							Debug.Log("kesita");
							Destroy(givedFood[i].gameObject);

							WanigText.SetText("違います！");
						}
					}

					if (givedFood[i].tag == "Drink" && !m_drinkCompletion)
					{
						if (givedFood[i].name == m_hopeDrink)
						{
							giveFoodCount--;
							m_drinkCompletion = true;
						}
						else
						{
							Destroy(givedFood[i].gameObject);

							WanigText.SetText("違います！");
						}
					}

					if(WanigText != null)
					{
						Invoke("ResetWanigText", 1f);
					}
				}
			}
			//全部渡し終わったら移動する
			if (giveFoodCount <= 0)
			{
				m_text[0].SetText("ty");
				Agent.SetDestination(NextTarget[0]);
				gameObject.tag = "Completion";

				Anime.SetBool("Walk", true);
				
				Invoke("TrayDestroy", 0.2f);

				Invoke("TextDestroy", 0.1f);
			}
		}
		//注文カウンターが空いてない時
		else if(targetIndex <= 3)
		{
			gameObject.tag = "Wait";

			if (gameObject.transform.position.x == Target[targetIndex].x && gameObject.transform.position.z == Target[targetIndex].z)
			{
				Anime.SetBool("Walk", false);
			}

			if(GameObject.FindGameObjectWithTag("Completion"))
			{
				targetIndex = 0;

				SetDestination();
			}
		}

		if(gameObject.transform.position.x == NextTarget[0].x)
		{
			Destroy(gameObject);
		}
	}

	//目的地の設定
	private void SetDestination()
	{
		//注文カウンターが空いているか、空いていたらそこに入る
		if (!m_inOder)
		{
			if (GameObject.FindGameObjectWithTag("OderCustomer"))
			{
				GameObject[] oderCustomer = GameObject.FindGameObjectsWithTag("OderCustomer");
				for (int i = 0; i < oderCustomer.Length; i++)
				{
					if (oderCustomer[i].transform.position.x == Target[0].x && oderCustomer[i].transform.position.z == Target[0].z
						&& targetIndex == 0)
					{
						targetIndex = 1;
					}
					else if (oderCustomer[i].transform.position.x == Target[1].x && oderCustomer[i].transform.position.z == Target[1].z
						&& targetIndex == 1)
					{
						targetIndex = 2;
					}
					else if (oderCustomer[i].transform.position.x == Target[2].x && oderCustomer[i].transform.position.z == Target[2].z
						&& targetIndex == 2)
					{
						targetIndex = 3;
					}
				}
			}
			Agent.SetDestination(Target[targetIndex]);
		}
	}

	public void SetCanvas(Transform canvas)
	{
		Canvas = canvas;
	}

	private void TrayDestroy()
	{
		Destroy(m_tray.gameObject);
	}

	private void ResetWanigText()
	{
		m_wanigText.SetText("");
	}

	private void TextDestroy()
	{
		Destroy(m_image);
	}
}

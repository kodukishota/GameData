using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContoloer : MonoBehaviour
{
	[SerializeField] private Camera mainCamera; // ���C���J�����̎Q��
	[SerializeField] private GameObject PlayerHands;

	[SerializeField] private GameObject Burger;
	[SerializeField] private GameObject Buns;
	[SerializeField] private GameObject BunsTop;
	[SerializeField] private GameObject Patty;
	[SerializeField] private GameObject Chease;
	[SerializeField] private GameObject Greenies;
	[SerializeField] private GameObject Tomato;

	[SerializeField] private GameObject PotatoBox;
	[SerializeField] private GameObject NuggetBox;

	[SerializeField] private GameObject Potato;
	[SerializeField] private GameObject Nugget;
	[SerializeField] private GameObject Crossiant;
	[SerializeField] private GameObject HotDog;

	[SerializeField] private GameObject CheaseBurger;
	[SerializeField] private GameObject NoCheaseBurger;
	[SerializeField] private GameObject TomatoBurger;
	[SerializeField] private GameObject GreeniesBurger;
	[SerializeField] private GameObject OriginalBurger;

	[SerializeField] private GameObject DrinkCup;
	[SerializeField] private GameObject Drink;

	[SerializeField] private AudioSource[] BurgerSE;
	[SerializeField] private AudioSource[] SideSE;
	[SerializeField] private AudioSource[] DrinkSE;
	[SerializeField] private AudioSource TrashBoxSE;

	private RaycastHit hit; //���C�L���X�g�������������̂��擾������ꕨ

	bool m_isTopBuns = false;		//�o�[�K�[�����I������
	bool m_isCompleted = false;		
	bool m_inHands = false;			//��ɉ��������Ă��邩
	bool m_havePotatoBox = false;	//��Ƀ|�e�g��Box�����Ă邩
	bool m_haveNuggetBox = false;   //��Ƀi�b�Q�g��Box�����Ă邩
	bool m_haveDrinkCup = false;    //��Ƀh�����N�̃J�b�v�����Ă邩

	bool m_touchBurger = false;
	bool m_touchBurgerEnd = false;
	bool m_touchSide = false;
	bool m_touchSideEnd = false;
	bool m_onBurgerTray = false;
	bool m_onSideTray = false;

	// Start is called before the first frame update
	void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		// �J�����̈ʒu�����ʒ����Ɍ������ă��C���΂�
		Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

		if (m_touchSide)
		{
			m_touchSide = false;
		}
		if (m_touchSideEnd)
		{
			m_touchSideEnd = false;
		}
		if (m_touchBurger)
		{
			m_touchBurger = false;
		}
		if (m_touchBurgerEnd)
		{
			m_touchBurgerEnd = false;
		}
		if(m_onBurgerTray)
		{
			m_onBurgerTray = false;
		}

		if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)) 
		{
			string objectName = hit.collider.gameObject.name; 
			Debug.Log(objectName);

			//�o�[�K�[�̋�ނ�����
			if (objectName == "Buns" && !m_isCompleted)
			{
				var underBuns = Instantiate(Buns, Burger.transform.position, Quaternion.identity, Burger.transform);

				underBuns.name = "UnderBuns";

				if (!m_isTopBuns)
				{
					var bunsTop = Instantiate(BunsTop,
						new Vector3(Burger.transform.position.x - 0.5f, Burger.transform.position.y, Burger.transform.position.z),
						Quaternion.identity,
						Burger.transform);

					m_isTopBuns = true;
					bunsTop.name = "BunsTop";

					BurgerSE[0].Play();
				}
				m_touchBurger = true;
			}

			if(objectName == "Patty")
			{
				var patty = Instantiate(Patty, Burger.transform.position, Quaternion.identity, Burger.transform);

				patty.name = "Patty";
				m_touchBurger = true;

				BurgerSE[1].Play();
			}

			if (objectName == "Chease")
			{
				var chease = Instantiate(Chease, Burger.transform.position, Quaternion.identity, Burger.transform);

				chease.name = "Chease";
				m_touchBurger = true;

				BurgerSE[2].Play();
			}

			if (objectName == "Greenies")
			{
				var greenies = Instantiate(Greenies, Burger.transform.position, Quaternion.identity, Burger.transform);

				greenies.name = "Greenies";
				m_touchBurger = true;

				BurgerSE[3].Play();
			}

			if (objectName == "Tomato")
			{
				var tomato = Instantiate(Tomato, Burger.transform.position, Quaternion.identity, Burger.transform);

				tomato.name = "Tomato";
				m_touchBurger = true;

				BurgerSE[4].Play();
			}

			if(objectName == "BunsTop")
			{
				var bunsTop = Instantiate(BunsTop, Burger.transform.position, Quaternion.identity, Burger.transform);

				bunsTop.name = "BunsTop";

				Destroy(hit.collider.gameObject);
				m_isTopBuns = false;
				m_isCompleted = true;
				Burger.tag = "Completed";
				m_touchBurger = true;

				BurgerSE[0].Play();
			}

			//�|�e�g
			if(objectName == "PotatoBox" && !m_inHands)
			{
				Instantiate(PotatoBox, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);
				m_havePotatoBox = true;
				m_inHands = true;
				m_touchSide = true;

				SideSE[0].Play();
			}
			if(objectName == "Potato" && m_havePotatoBox)
			{
				Transform[] haveBox = PlayerHands.gameObject.GetComponentsInChildren<Transform>();

				var potato = Instantiate(Potato, PlayerHands.transform.position, Quaternion.Euler(-90,0,-90), PlayerHands.transform);

				potato.name = "�|�e�g";
				Destroy(haveBox[1].gameObject);
				m_touchSide = true;
				m_touchSideEnd = true;

				SideSE[1].Play();
			}

			//�i�b�Q�g
			if(objectName == "NuggetBox" && !m_inHands)
			{
				Instantiate(NuggetBox, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);
				m_haveNuggetBox = true;
				m_inHands = true;
				m_touchSide = true;

				SideSE[0].Play();
			}
			if(objectName == "Nugget" && m_haveNuggetBox)
			{
				Transform[] haveBox = PlayerHands.gameObject.GetComponentsInChildren<Transform>();

				var nugget =  Instantiate(Nugget, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

				nugget.name = "�i�Q�b�g";
				Destroy(haveBox[1].gameObject);
				m_touchSide = true;
				m_touchSideEnd = true;

				SideSE[1].Play();
			}

			//�N�����b�T��
			if(objectName == "Crossiant" && !m_inHands)
			{
				var crossiant = Instantiate(Crossiant, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

				crossiant.name = "�N�����b�T��";
				m_inHands = true;
				m_touchSide = true;
				m_touchSideEnd = true;

				SideSE[2].Play();
			}

			//�z�b�g�h�b�O
			if(objectName == "HotDog" && !m_inHands)
			{
				var hotDog = Instantiate(HotDog, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

				hotDog.name = "�z�b�g�h�b�N";
				m_inHands = true;
				m_touchSide = true;
				m_touchSideEnd = true;

				SideSE[2].Play();
			}
		
			//���������n���o�[�K�[�����ʂ��Ď�Ɏ���
			if (objectName == "Burger" && m_isCompleted && !m_inHands) 
			{
				int BurgerIngredientsCount = hit.collider.gameObject.transform.childCount;
				Transform[] Ingredients = hit.collider.gameObject.GetComponentsInChildren<Transform>();

				m_isCompleted = false;
				m_inHands = true;

				m_touchBurgerEnd = true;

				bool createOriginal = false;
				bool createChease = false;
				bool createGreenies = false;
				bool createTomato = false;

				for(int i = 1; i <= BurgerIngredientsCount; i++)
				{
					Destroy(Ingredients[i].gameObject);
				}

				//�I���W�i���o�[�K�[
				if (Ingredients[1].name == "UnderBuns")
				{
					if (Ingredients[2].name == "Patty")
					{
						if (Ingredients[3].name == "Greenies")
						{
							if (Ingredients[4].name == "Chease")
							{
								if (Ingredients[5].name == "Tomato")
								{
									var originalBurger = Instantiate(OriginalBurger, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

									createOriginal = true;
									originalBurger.name = "�I���W�i���o�[�K�[";
								}
							}
						}
					}
				}
			
				//�`�[�Y�o�[�K�[
				if (Ingredients[1].name == "UnderBuns")
				{
					if (Ingredients[2].name == "Patty")
					{
						if (Ingredients[3].name == "Chease" && !createOriginal)
						{
							var cheaseBurger = Instantiate(CheaseBurger, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

							createChease = true;
							cheaseBurger.name = "�`�[�Y�o�[�K�[";
						}
					}
				}
			
				//�g�}�g�o�[�K�[
				if (Ingredients[1].name == "UnderBuns")
				{
					if (Ingredients[2].name == "Patty")
					{
						if (Ingredients[3].name == "Tomato")
						{
							if (Ingredients[4].name == "Tomato")
							{
								var tomatoBurger = Instantiate(TomatoBurger, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

								createTomato = true;
								tomatoBurger.name = "�g�}�g�o�[�K�[";
							}
						}
					}
				}

				//���^�X�o�[�K�[
				if (Ingredients[1].name == "UnderBuns")
				{
					if (Ingredients[2].name == "Patty")
					{
						if (Ingredients[3].name == "Greenies")
						{
							if (Ingredients[4].name == "Greenies")
							{
								var greeniesBurger = Instantiate(GreeniesBurger, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

								createGreenies = true;
								greeniesBurger.name = "��؃o�[�K�[";
							}
						}
					}
				}

				//�p�e�B�����o�[�K�[
				if (Ingredients[1].name == "UnderBuns")
				{
					if (Ingredients[2].name == "Patty" && !createOriginal && !createChease && !createGreenies && !createTomato)
					{
						var noCheaseBurger = Instantiate(NoCheaseBurger, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

						noCheaseBurger.name = "�`�[�Y�Ȃ��o�[�K�[";
					}
				}
			}

			//�h�����N
			if(objectName == "DrinkCup" && !m_inHands)
			{
				Instantiate(DrinkCup, PlayerHands.transform.position, Quaternion.identity,PlayerHands.transform);

				m_inHands = true;
				m_haveDrinkCup = true;
				m_touchSide = true;

				DrinkSE[0].Play();
			}
			if(objectName == "DrinkMachine" && m_haveDrinkCup)
			{
				Transform[] haveCup = PlayerHands.gameObject.GetComponentsInChildren<Transform>();
				var drink =  Instantiate(Drink, PlayerHands.transform.position, Quaternion.identity, PlayerHands.transform);

				drink.name = "�h�����N";
				m_haveDrinkCup = false;
				Destroy(haveCup[1].gameObject);
				m_touchSide = true;
				m_touchSideEnd = true;

				DrinkSE[1].Play();
			}

			//�g���C�ɕ����悹��
			if (objectName == "Tray")
			{
				Transform tray = hit.collider.gameObject.transform;
				Transform[] haveFood = PlayerHands.GetComponentsInChildren<Transform>();
			
				if (m_inHands)
				{
					if (haveFood[1].tag == "Burger")
					{
						haveFood[1].position = new Vector3(tray.position.x, tray.position.y + 0.1f, tray.position.z);
						m_onBurgerTray = true;
					}
					if (haveFood[1].tag == "Side")
					{
						Debug.Log(haveFood[1]);
						haveFood[1].position = new Vector3(tray.position.x + -0.3f, tray.position.y + 0.05f, tray.position.z + -0.2f);
						m_onSideTray = true;
					}
					if (haveFood[1].tag == "Drink")
					{
						haveFood[1].position = new Vector3(tray.position.x + 0.23f, tray.position.y, tray.position.z + 0.05f);
						m_onSideTray = true;
					}
					//haveFood[1].rotation = Quaternion.identity;
					haveFood[1].SetParent(tray);

					m_inHands = false;
				}
				m_touchSide = true;
			}

			//�S�~���Ɏ̂Ă�
			if(objectName == "TrashBox" && m_inHands)
			{
				Transform[] haveFood = PlayerHands.gameObject.GetComponentsInChildren<Transform>();

				m_inHands = false;
				TrashBoxSE.Play();

				Destroy(haveFood[1].gameObject);
			}
		}
	}

	public bool GetTouchBurgerBool()
	{
		return m_touchBurger;
	}
	public bool GetTouchBurgerEnd()
	{
		return m_touchBurgerEnd;
	}
	public bool GetOnBurger()
	{
		return m_onBurgerTray;
	}
	public bool GetTouchSideBool()
	{
		return m_touchSide;
	}
	public bool GetTouchSideEnd()
	{
		return m_touchSideEnd;
	}
	public bool GetOnSide()
	{
		return m_onSideTray;
	}
}

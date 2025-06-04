using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScript : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI ScoreText;
	[SerializeField] TextMeshProUGUI HiScoreText;

	int Score;
	static int HiScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = ScoreScript.GetScore();

		ScoreText.SetText("スコア\n" + Score);

		if(Score >= HiScore)
		{
			HiScore = Score;
		}

		HiScoreText.SetText("ハイスコア\n" + HiScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

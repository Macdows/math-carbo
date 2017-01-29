using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MathsAndAnswerScript : MonoBehaviour {

    public static MathsAndAnswerScript instance;
    private int currentMode;
	[HideInInspector]public float timeForQuestion;
	[HideInInspector]public int score;
	private float a, b, c, d, e;
    public GameObject valueA , valueB, valueC, valueD, valueE, valueAnswer;
    private float scoreMileStone;
    public float scoreMileStoneCount;
	public Generator generator;


    void Awake()
    {
		generator = gameObject.GetComponent<Generator>();
		valueA = GameObject.Find("ValueAText");
		valueB = GameObject.Find("ValueBText");
		valueC = GameObject.Find("ValueCText");
		valueD = GameObject.Find("ValueDText");
		valueE = GameObject.Find("ValueEText");
		valueAnswer = GameObject.Find("AnswerText");
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	void Start ()
    {
        scoreMileStone = scoreMileStoneCount;

//		GameManager.singleton.timeForQuestion = timeForQuestion;

        if (GameManager.singleton != null)
        {
            currentMode = GameManager.singleton.currentMode;
        }

        SetDifficulty();

    }

	void Update ()
    {
        MileStoneProcess();
    }

    void MileStoneProcess()
    {
        if (scoreMileStone < GameManager.singleton.currentScore)
        {
            scoreMileStone += scoreMileStoneCount;

            timeForQuestion += 0.02f;

            if (timeForQuestion >= 0.2f)
            {
                timeForQuestion = 0.2f;
            }

        }
    }

    public void SetDifficulty()
    {
        switch (currentMode)
        {
            case (1):
				StartGameMode(0.01f);
                break;
            case (2):
				StartGameMode(0.05f);
                break;
            case (3):
				StartGameMode(0.1f);
                break;
        }
    }

	public void StartGameMode(float time)
	{
		List<int> list = generator.RandomList();
		int answer = generator.PerformRandomOperations (list);
		GameManager.singleton.timeForQuestion = timeForQuestion = time;

		valueA.GetComponent<Text> ().text = "" + list [0];
		valueB.GetComponent<Text> ().text = "" + list [1];
		valueC.GetComponent<Text> ().text = "" + list [2];
		valueD.GetComponent<Text> ().text = "" + list [3];
		valueE.GetComponent<Text> ().text = "" + list [4];
		valueAnswer.GetComponent<Text> ().text = "" + answer;
        
     }
}

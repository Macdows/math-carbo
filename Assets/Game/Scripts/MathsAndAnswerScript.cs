using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MathsAndAnswerScript : MonoBehaviour {

    //we make this script instance
    public static MathsAndAnswerScript instance;

    //its an enum which we help use to identify the current mode of game 
    public enum MathsType
    {
        addition,
        subtraction,
        multiplication,
        division,
        mix
    }

    //we make a variable of MathsType
    public MathsType mathsType;

    //2 private floats this are the question values a and b
    private float a, b, c, d, e;
    //the variable for answer value
    [HideInInspector] public float answer;
    //varible whihc will assign ans to any one of the 4 answer button
    private float locationOfAnswer;
    //ref to the button
    public GameObject[] ansButtons;
    //ref to image symbol so player can know which operation is to be done
    public Image mathSymbolObject;
    //ref to all the symbol sprites whihc will be used in above image
    public Sprite[] mathSymbols;
    //get the tag of button 
    public string tagOfButton;

    //varible to check whihc mode is this
    private int currentMode;

    //ref to the time for each question
    public float timeForQuestion;

    //score vairable
    [HideInInspector]public int score;

    //ref to text in scene where we will assign a and b values of question
    public Text valueA , valueB, valueC, valueD, valueE, valueAnswer;

    //this is to check the progress of player so we can decrease the time with increase in score
    private float scoreMileStone;
    public float scoreMileStoneCount;


    void Awake()
    {
		valueA = GameObject.Find("ValueAText").GetComponent<Text>();
		valueB = GameObject.Find("ValueBText").GetComponent<Text>();
		valueC = GameObject.Find("ValueCText").GetComponent<Text>();
		valueD = GameObject.Find("ValueDText").GetComponent<Text>();
		valueE = GameObject.Find("ValueEText").GetComponent<Text>();
		valueAnswer = GameObject.Find("AnswerText").GetComponent<Text>();
        MakeInstance();
    }

    //method whihc make this object instance
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //at start we need to do few basic setups
	void Start ()
    {
        //we put the location value in tag of button variable
        tagOfButton = locationOfAnswer.ToString();

		valueA.text = "" + Random.Range(1, 12);
		valueB.text = "" + Random.Range(1, 24);
		valueC.text = "" + Random.Range(1, 24);
		valueD.text = "" + Random.Range(1, 12);
		valueE.text = "" + Random.Range(1, 12);
		valueAnswer.text = "" + Random.Range (1, 12);

        //at start the mileSton value is equal to mile stone count
        scoreMileStone = scoreMileStoneCount;

        //get the time value
        GameManager.singleton.timeForQuestion = timeForQuestion;

        if (GameManager.singleton != null)
        {
            //get whihc mode is selected
            currentMode = GameManager.singleton.currentMode;
        }

        MathsProblem();

    }
	
	// Update is called once per frame
	void Update ()
    {
        tagOfButton = locationOfAnswer.ToString();

        MileStoneProcess();
    }

    //this method reduces the time with increase in score
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


    //Below code is for maths calculation
    //this methode calls the respective method for the respective mode
    //eg for addition mode it will only call addition method
    public void MathsProblem()
    {
        //switch case is used to assign method
        switch (currentMode)
        {
            case (1):
                MediumGameMode();
                break;
            case (2):
				MediumGameMode();
                break;
            case (3):
				MediumGameMode();
                break;
        }
    }


	void MediumGameMode()
    {
        mathSymbolObject.sprite = mathSymbols[0];

        //now we assign the values to the ans buttons
        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {
                //we check for location value and the assign it to the corresponding ans button 
                ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;

            }
            else
            {
                //for other ans button we assign random values
                ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1,41);

                while (ansButtons[i].GetComponentInChildren<Text>().text == "" + answer)
                {
                    //we make sure that only one button has answer values 
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 41);
                }
            }

        }
        
     }
}

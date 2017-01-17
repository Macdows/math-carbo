using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CheckButtonPress : MonoBehaviour {

    private int score, hiScore;
	public List<GameObject> selected = new List<GameObject>();
	public string _operator;
	public int answer;
	public Image backgroundSprite;

    void Start()
    {
		_operator = null;
        score = 0;
		hiScore = GameManager.singleton.hiScore;
    }

    void Update()
    {
        score = GameManager.singleton.currentScore;
        if (hiScore < score)
        {
            hiScore = score;
            GameManager.singleton.hiScore = hiScore;
            GameManager.singleton.Save();
        }

		if (selected.Count == 2  && _operator != null) {
			Operation();
		}
    }

	void Awake()
	{
		backgroundSprite = GameObject.Find("BackgroundImage").GetComponent<Image>();
	}

	public void selectOperator(GameObject __operator)
    {
		_operator = __operator.tag;
        
    }

	public void selectButton(GameObject button) {
		if (selected.Count == 0)
		{
			selected.Add(button);
			ButtonColorChange (button);
		}
		else if (selected.Contains(button))
		{
			selected.Remove (button);
			ButtonColorReset (button);
		}
		else if (selected.Count == 1)
		{
			selected.Add (button);
			ButtonColorChange (button);
		}
	}

	public void Confirm() {
		if (selected.Count != 0) {
			if (selected.Count == 1) {
				int value;
				string valueText = selected [0].transform.GetChild (0).GetComponent<Text>().text;
				string answerText = GameObject.Find("AnswerText").GetComponent<Text>().text;
				System.Int32.TryParse (valueText, out value);
				System.Int32.TryParse(answerText, out answer);

				if (value == answer) {
					score++;
					Debug.Log (score);
				}
			}

			for (int i = 0; i < selected.Count; i++) {
				ButtonColorReset (selected [i]);
			}
			selected.Clear ();
			_operator = null;
			StartCoroutine(ScreenFlash());
		}
		StartCoroutine(ScreenFlash());
	}

	private void Operation() {
		int a, b;
		int c = -1;

		Text aText = selected[0].transform.GetChild(0).GetComponent<Text>();
		Text bText = selected[1].transform.GetChild(0).GetComponent<Text>();
		System.Int32.TryParse(aText.text, out a);
		System.Int32.TryParse(bText.text, out b);

		switch (_operator)
		{
			case "0":
				c = a + b;
				aText.text = "" + c.ToString ();
				GameObject.Destroy (selected [1]);
				break;

			case "1":
				if (a > b)
				{
					c = a - b;
					aText.text = "" + c.ToString();
					GameObject.Destroy (selected [1]);
				}
				else
				{
					c = -1;
					ButtonColorReset (selected [1]);
					StartCoroutine(ScreenFlash());
				}
				break;

			case "2":
				c = a * b;
				aText.text = "" + c.ToString();
				GameObject.Destroy (selected [1]);
				break;

			case "3":
				if (a % b == 0)
				{
					c = a / b;
					aText.text = "" + c.ToString();
					GameObject.Destroy (selected [1]);
				}
				else
				{
					c = -1;
					ButtonColorReset (selected [1]);
					StartCoroutine(ScreenFlash());
				}
				break;
		}

		ButtonColorReset (selected [0]);
		selected.Clear();
		_operator = null;
	}

	public void ButtonColorChange (GameObject button) {
		Image image = button.GetComponent<Image>();
		image.color = new Color32(89, 198, 134, 255);
	}

	public void ButtonColorReset (GameObject button) {
		Image image = button.GetComponent<Image>();
		image.color = new Color32(255, 255, 255, 255);
	}

	IEnumerator ScreenFlash()
	{
		backgroundSprite.color = new Color32(255, 0, 0, 255);
		yield return new WaitForSeconds(0.1f);
		backgroundSprite.color = new Color32(255, 255, 255, 255);
	}
}

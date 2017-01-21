
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public int step;
	public GameObject player;
	public GameObject rightText;
	public GameObject leftText;
	public GameObject middleText;
	public GameObject lastText;
	public GameObject textGroup;
	public GameObject ButtonGroup;
	public bool middleDown;
	public GameObject jumpButton;
	public GameObject punchButton;

	// Use this for initialization
	void Start () {
		step = 1;
		middleDown = false;
		jumpButton.GetComponent<Image>().enabled = false;
		punchButton.GetComponent<Image>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (step == 1 && player.GetComponent<GeneralControl> ().currentAnimationState == "Move" && player.GetComponent<Transform>().localScale.x == 1f) {
			step = 2;
			rightText.gameObject.SetActive(true);
			leftText.gameObject.SetActive(false);
		} 

		else if (step == 2 && player.GetComponent<GeneralControl> ().currentAnimationState == "Move" && player.GetComponent<Transform>().localScale.x == -1f) {
			step = 3;


			rightText.gameObject.SetActive(false);
			/*
			middleText.gameObject.SetActive(true);
			*/

		} 

		else if (step == 3 && middleDown) {
			step = 4;
		
			middleText.gameObject.SetActive(false);
			lastText.gameObject.SetActive(true);

		} 

		else if (step == 4 && countBeganPhaseTouch() != 0) {
			step = 0;
			textGroup.GetComponent<CanvasGroup>().alpha = 0;
			ButtonGroup.GetComponent<CanvasGroup>().alpha = 0;
			jumpButton.GetComponent<Image>().enabled = true;
			punchButton.GetComponent<Image>().enabled = true;
		}

		middleDown = false;
	}

	public void middleIsDown(){
		middleDown = true;
	}

	public int countBeganPhaseTouch(){
		int returnValue = 0;
		for (int i = 0; i<Input.touchCount; i++) {
			if(Input.touches[i].phase == TouchPhase.Began){
				returnValue +=1;
			}
		}
		return returnValue;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GeneralControl : MonoBehaviour {

	public string currentAnimationState;
	public string currentMoveState;
	public string intendedAnimationState;
	public string intendedMoveState;
	public float jumpTime;
	public Dictionary<int,Vector2> PositionDict;
	public Dictionary<int,float> TimeDict;
	public Animator myAnimator;
	public Transform myTrans;
	public float speed;
	public float jumpHeight;
	public GameObject rightButton;
	public GameObject leftButton;
	public bool rightButtonPress;
	public bool leftButtonPress;


	// Use this for initialization
	void Start () {
		currentMoveState = "Idle";
		currentAnimationState = "Idle";
		intendedMoveState = "";
		intendedAnimationState = "";
		myAnimator = this.GetComponent<Animator> ();
		myTrans = this.GetComponent<Transform> ();
		jumpTime = 0f;
		rightButtonPress = false;
		leftButtonPress = false;
	}
	
	// Update is called once per frame
	void Update () {
		//button input
		if (leftButtonPress) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(1f,1f,1f);
				this.transform.position -= new Vector3 (speed * Time.deltaTime, 0f, 0f);
				intendedAnimationState = "move";
			}
		}
		if (rightButtonPress) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(-1f,1f,1f);
				this.transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
				intendedAnimationState = "move";
			}
		}

		//change intended state
		for (int i=0; i<Input.touchCount; i++) {
			if (Input.touches [i].phase == TouchPhase.Began) {
				PositionDict.Add (Input.touches [i].fingerId, Input.touches [i].position);
				TimeDict.Add (Input.touches [i].fingerId, 0f);
			} else if (Input.touches [i].phase == TouchPhase.Ended) {
				if (Mathf.Abs (Input.touches [i].position.x - PositionDict [Input.touches [i].fingerId].x) > 230f) {
					intendedAnimationState = "special";
				} else if (Input.touches [i].position.y - PositionDict [Input.touches [i].fingerId].y > 150f) {
					intendedMoveState = "jump";
				} else if (TimeDict [Input.touches [i].fingerId] < 0.5f && Mathf.Abs (Input.touches [i].position.y - PositionDict [Input.touches [i].fingerId].y) < 30f && Mathf.Abs (Input.touches [i].position.x - PositionDict [Input.touches [i].fingerId].x) < 30f && Input.touches [i].position.x > 240f && Input.touches [i].position.x < 1037f) {
					intendedAnimationState = "punch";
				}
				PositionDict.Remove (Input.touches [i].fingerId);
				TimeDict.Remove (Input.touches[i].fingerId);
			}else{
				TimeDict [Input.touches [i].fingerId] += Time.deltaTime;
			}

		}


		// switch between states
		switch (intendedAnimationState) {
		case "":
			break;
		case "punch":
			if (currentAnimationState != "Impact") {
				myAnimator.Play ("Punch");
				currentAnimationState = "Punch";
			}
			break;
		case "move":
			if (currentAnimationState == "Idle") {
				myAnimator.Play ("Move");
				currentAnimationState = "Move";
			}
			break;
		case "special":
			if (currentAnimationState != "Impact") {
				myAnimator.Play ("Special");
				currentAnimationState = "Special";
			}
			break;
		case "impact":
			if(currentAnimationState != "Impact"){
				myAnimator.SetTrigger ("Impact");
				currentAnimationState = "Impact";
			}
			break;
		default:
			break;
		}


		if (intendedMoveState == "jump" && currentAnimationState != "Impact") {
			currentMoveState = "Jump";
			jumpTime+= Time.deltaTime;
		}
		if (jumpTime >0f && jumpTime <= 0.5f) {
			jumpTime += Time.deltaTime;
			this.transform.position += new Vector3(0f,Time.deltaTime*jumpHeight,0f);
		}
		if (jumpTime > 0.5f) {
			jumpTime = 0f;
			currentMoveState = "Idle";
		}

		intendedMoveState = "";
		intendedAnimationState = "";
	}

	public void leftButtonDown(){
		leftButtonPress = true;
		if (rightButtonPress) {
			rightButtonPress = false;
		}
	}

	public void rightButtonDown(){
		rightButtonPress = true;
		if (leftButtonPress) {
			leftButtonPress = false;
		}
	}
	public void leftButtonUp(){
		leftButtonPress = false;
	}
	public void rightButtonUp(){
		rightButtonPress = false;
	}

	//temporary input code
	public void jumpFucntion(){
		intendedMoveState = "jump";
	}
	public void punchFunction(){
		intendedAnimationState = "punch";
	}
		
}

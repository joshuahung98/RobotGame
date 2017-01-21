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
	public GameObject myCamera;
	public Transform cameraTrans;
	public float health;
	public float intentionHealth;
	public GameObject healthBar;
	public float comboTime;
	public bool canHit;
	public int punchNumber;


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
		PositionDict = new Dictionary<int, Vector2> ();
		TimeDict = new Dictionary<int , float > ();
		cameraTrans = myCamera.GetComponent<Transform> ();
		health = 100f;
		intentionHealth = 100f;
		comboTime = 0f;
		canHit = true;
	}
	
	// Update is called once per frame
	void Update () {
		//fresh intention
		intendedMoveState = "";
		intendedAnimationState = "";

		if (health ==
		    0) {
			Application.LoadLevel (3);
		}

		//button input
		if (leftButtonPress && this.transform.position.x > -3.6f) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(1f,1f,1f);
				this.transform.position -= new Vector3 (speed * Time.deltaTime, 0f, 0f);
				if(cameraMove() == "left"){
					cameraTrans.position -= new Vector3(speed*Time.deltaTime,0f,0f);
				}
				if(currentAnimationState == "Idle"){
					intendedAnimationState = "move";
				}
			}
		}
		if (rightButtonPress && this.transform.position.x <3.6f) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(-1f,1f,1f);
				this.transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
				if(cameraMove() == "right"){
					cameraTrans.position += new Vector3(speed*Time.deltaTime,0f,0f);
				}
				if(currentAnimationState == "Idle"){
					intendedAnimationState = "move";
				}
			}
		}

		if ((!leftButtonPress || !rightButtonPress) && currentAnimationState == "Move") {
			currentAnimationState = "Idle";
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			intendedAnimationState = "special";
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			intendedAnimationState = "punch";
		}

		//change intended state
		for (int i=0; i<Input.touchCount; i++) {
			if (Input.touches [i].phase == TouchPhase.Began) {
				PositionDict.Add (Input.touches [i].fingerId, Input.touches [i].position);
				TimeDict.Add (Input.touches [i].fingerId, 0f);
			} else if (Input.touches [i].phase == TouchPhase.Ended) {
				if (Mathf.Abs (Input.touches [i].position.x - PositionDict [Input.touches [i].fingerId].x) > 100f) {
					intendedAnimationState = "special";
				} else if (Input.touches [i].position.y - PositionDict [Input.touches [i].fingerId].y > 100f) {
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
			if (currentAnimationState != "Impact" && currentAnimationState != "Punch" && currentAnimationState != "PunchLast" && canHit) {
				myAnimator.Play ("Punch");
				currentAnimationState = "PunchBegan";
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
		if (currentAnimationState == "Idle" && this.GetComponent<AudioSource> ().isPlaying == false) {
			this.GetComponent<AudioSource>().volume = 0.25f;
			this.GetComponent<AudioSource>().Play();
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
		//health
		if (healthBar.GetComponent<Slider> ().value > 0f) {
			healthBar.GetComponent<Slider> ().value = health / 100f; 
		} else {
			healthBar.GetComponent<Slider>().value = 0f;
		}

		//calculate when robot can hit again
		if (!canHit) {
			comboTime+=Time.deltaTime;
			if(comboTime > 0.3f){
				comboTime = 0f;
				canHit = true;
			}
		}

		//calculate health
		if (intentionHealth < health) {
			health -= Time.deltaTime*(health-intentionHealth);
		}

	}

	public void leftButtonDown(){
		leftButtonPress = true;
		if (rightButtonPress) {
			rightButtonPress = false;
		} else {
			if(currentAnimationState != "Impact"){
				this.GetComponent<AudioSource>().volume = 0.5f;
				this.GetComponent<AudioSource>().Play();
			}
		}
	}
	
	public void rightButtonDown(){
		rightButtonPress = true;
		if (leftButtonPress) {
			leftButtonPress = false;
		}else {
			if(currentAnimationState != "Impact"){
				this.GetComponent<AudioSource>().volume = 0.5f;
				this.GetComponent<AudioSource>().Play();
			}
		}
	}
	public void leftButtonUp(){
		leftButtonPress = false;
		if (rightButtonPress == false) {
			this.GetComponent<AudioSource>().Stop();
		}
	}
	public void rightButtonUp(){
		rightButtonPress = false;
		if (leftButtonPress == false) {
			this.GetComponent<AudioSource> ().Stop ();
		}
	}

	public string cameraMove(){
		if (this.transform.position.x < cameraTrans.position.x - 0.92f) {
			return "left";
		} else if (this.transform.position.x > cameraTrans.position.x + 0.92f) {
			return "right";
		} else {
			return "";
		}
	}
	
}

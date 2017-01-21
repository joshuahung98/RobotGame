
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
	
	public string currentAnimationState;
	public string currentMoveState;
	public string intendedAnimationState;
	public string intendedMoveState;
	public float jumpTime;
	public Animator myAnimator;
	public Transform myTrans;
	public float speed;
	public float jumpHeight;
	public bool rightButtonPress;
	public bool leftButtonPress;
	public float health;
	public float intentionHealth;
	public GameObject healthBar;
	
	
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
		health = 100f;
		intentionHealth = 100f;
	}
	
	// Update is called once per frame
	void Update () {
		//fresh intention


		if (health == 0) {
			Application.LoadLevel (2);
		}

		intendedMoveState = "";
		if (intendedAnimationState != "impact") {
			intendedAnimationState = "";
		}
		
		//button input
		if (leftButtonPress && this.transform.position.x > -3.6f) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(1f,1f,1f);
				this.transform.position -= new Vector3 (speed * Time.deltaTime, 0f, 0f);
				if(currentAnimationState == "Idle"){
					intendedAnimationState = "move";
				}
			}
		}
		if ((!leftButtonPress || !rightButtonPress) && currentAnimationState == "Move") {
			currentAnimationState = "Idle";
		}
		if (rightButtonPress && this.transform.position.x <3.6f) {
			if (currentAnimationState != "Impact") {
				this.transform.localScale = new Vector3(-1f,1f,1f);
				this.transform.position += new Vector3 (speed * Time.deltaTime, 0f, 0f);
				if(currentAnimationState == "Idle"){
					intendedAnimationState = "move";
				}
			}
		}

		
		//change intended state
	
		
		
		// switch between states
		switch (intendedAnimationState) {
		case "":
			break;
		case "punch":
			if (currentAnimationState != "Impact" && currentAnimationState != "Punch") {
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

		if (intendedAnimationState == "impact") {
			intendedAnimationState = "";
		}

		if (intentionHealth < health) {
			health -= Time.deltaTime*(health-intentionHealth)*5;
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
	
}

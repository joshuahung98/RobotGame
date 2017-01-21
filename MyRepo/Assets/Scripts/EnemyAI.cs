using UnityEngine;
using System.Collections;


public class EnemyAI : MonoBehaviour
{
	public float movSpeed;
	public float padding;
	Transform player; 
	Transform thisTransform;
	bool gettingPunched = false; 
	float time = 0f; 
	public static int numberOfPunches = 0; 
	public string currentAnimationStateAI;
	public string currentMoveStateAI;
	public string intendedAnimationStateAI;
	public string intendedMoveStateAI;
	public Animator myAnimatorAI;
	public Transform myTransAI;
	public float jumpTimeAI;
	public float jumpHeightAI;
	
	void Start() { 
		currentMoveStateAI = "Idle";
		currentAnimationStateAI = "Idle";
		intendedMoveStateAI = "";
		intendedAnimationStateAI = "";
		myAnimatorAI = this.GetComponent<Animator> ();
		myTransAI = this.GetComponent<Transform> ();
		jumpTimeAI = 0f;
		thisTransform = this.GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> (); 
	}
	
	void FixedUpdate () {
		Debug.Log ("fixed update is being called"); 
		if (thisTransform.position.x < player.position.x-padding &&
		    Mathf.Abs (this.transform.position.x - player.position.x-padding) < 2) {
			intendedAnimationStateAI = "Move"; 
			Vector3 temp = thisTransform.position;
			temp.x += movSpeed;
			thisTransform.position = temp;
		}
		else if (thisTransform.position.x > player.position.x+padding &&
		         Mathf.Abs (this.transform.position.x - player.position.x -padding) < 2) {
			intendedAnimationStateAI = "Move"; 
			Vector3 temp = thisTransform.position;
			temp.x -= movSpeed;
			thisTransform.position = temp;
		}
	}

	
	void Update () {
		time += Time.deltaTime; 
		Random randomNumber = new Random ();
		float isJump;
		isJump = Random.value;
		isJump *= 100;
		if (Mathf.Abs (this.transform.position.x - player.position.x) <= 3 && Mathf.Abs (this.transform.position.y - player.position.y) < 2) {
			if (isJump > 1 && isJump < 2) {
				Debug.Log ("Jumping");
				Vector3 temp = thisTransform.position;
				temp.y += movSpeed * 2f;
				thisTransform.position = temp;
			}
		}
		
		if (Mathf.Abs (this.transform.position.x - player.position.x) <= 0.3f) {
			intendedAnimationStateAI = "punch"; 
		}

		if (time > 1f) {
			if (numberOfPunches > 3) {
				Debug.Log ("AI is punched"); 
				intendedAnimationStateAI = "Move";  
				if (this.transform.position.x < player.position.x) {
					Vector3 temp = thisTransform.position;
					temp.x -= movSpeed * 50; 
					thisTransform.position = temp;
				}  else {
					Vector3 temp = thisTransform.position;
					temp.x += movSpeed * 50;
					thisTransform.position = temp; 
				}
				numberOfPunches = 0;
				intendedAnimationStateAI = "Idle"; 
			}
			numberOfPunches = 0; 
			time = 0; 
		}
		
		
		// switch between states
		switch (intendedAnimationStateAI) {
		case "":
			break;
		case "punch":
			if (currentAnimationStateAI != "Impact") {
				myAnimatorAI.Play ("Punch");
				currentAnimationStateAI = "Punch";
			}
			break;
		case "move":
			if (currentAnimationStateAI == "Idle") {
				myAnimatorAI.Play ("Move");
				currentAnimationStateAI = "Move";
			}
			break;
		case "special":
			if (currentAnimationStateAI != "Impact") {
				myAnimatorAI.Play ("Special");
				currentAnimationStateAI = "Special";
			}
			break;
		case "impact":
			if (currentAnimationStateAI != "Impact") {
				myAnimatorAI.SetTrigger ("Impact");
				currentAnimationStateAI = "Impact";
			}
			break;
		default:
			break;
		}
		
		if (intendedMoveStateAI == "jump" && currentAnimationStateAI != "Impact") {
			currentMoveStateAI= "Jump";
			jumpTimeAI += Time.deltaTime;
		}
		if (jumpTimeAI > 0f && jumpTimeAI <= 0.5f) {
			jumpTimeAI += Time.deltaTime;
			this.transform.position += new Vector3 (0f, Time.deltaTime * jumpHeightAI, 0f);
		}
		if (jumpTimeAI > 0.5f) {
			jumpTimeAI = 0f;
			currentMoveStateAI = "Idle";
		}
		
		intendedMoveStateAI = "";
		intendedAnimationStateAI = "";
	}
	
}


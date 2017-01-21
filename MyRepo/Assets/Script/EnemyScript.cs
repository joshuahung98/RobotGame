using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject player;
	public string currentAnimationState;
	public string currentMoveState;
	public string intendedAnimationState;
	public string intendedMoveState;
	public float jumpTime;
	public Animator myAnimator;
	public Transform myTrans;
	public float speed;
	public float jumpHeight;

	// Use this for initialization
	void Start () {
		currentMoveState = "Idle";
		currentAnimationState = "Idle";
		intendedMoveState = "";
		intendedAnimationState = "";
		myAnimator = this.GetComponent<Animator> ();
		myTrans = this.GetComponent<Transform> ();
		jumpTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
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
}

using UnityEngine;
using System.Collections;

public class EnemyPunchScript : StateMachineBehaviour {

	public GameObject robot;
	public GameObject fist;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot = GameObject.Find ("Enemy");
		fist = GameObject.Find ("/Enemy/Fist");
		robot.GetComponent<EnemyScript>().currentAnimationState = "Punch";
		fist.GetComponent<AudioSource>().Play();
	}
	
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot.GetComponent<EnemyScript> ().currentAnimationState = "Idle";
		fist.GetComponent<AudioSource>().Stop();
	}
}

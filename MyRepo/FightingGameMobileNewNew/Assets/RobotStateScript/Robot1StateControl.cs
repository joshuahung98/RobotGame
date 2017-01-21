using UnityEngine;
using System.Collections;

public class Robot1StateControl : StateMachineBehaviour {

	public GameObject robot;
	public int frameNumber;
	public GameObject missile;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot = GameObject.Find ("/Robot1");
		frameNumber = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		frameNumber += 1;
		if (frameNumber == 30) {
			if(robot.transform.localScale.x == 1f){
				Instantiate (missile, robot.GetComponent<Transform> ().position + new Vector3 (0f, 0.2f, 0f), new Quaternion(0f,0f,0f,0f));
			}else{
				Instantiate (missile, robot.GetComponent<Transform> ().position + new Vector3 (0f, 0.2f, 0f), new Quaternion(0f,180f,0f,0f));
			}
		}
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot.GetComponent<GeneralControl>().currentAnimationState = "Idle";
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

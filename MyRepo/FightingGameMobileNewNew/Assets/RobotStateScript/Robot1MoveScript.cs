using UnityEngine;
using System.Collections;

public class Robot1MoveScript : StateMachineBehaviour {
	public GameObject robot;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot = GameObject.Find("Robot1");
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (robot.GetComponent<GeneralControl> ().rightButtonPress || robot.GetComponent<GeneralControl> ().leftButtonPress) {
			if(robot.GetComponent<GeneralControl>().currentAnimationState == "Move"){
				robot.GetComponent<Animator> ().Play ("Move");
			}
		} else {
			robot.GetComponent<GeneralControl>().currentAnimationState = "Idle";
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here


	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

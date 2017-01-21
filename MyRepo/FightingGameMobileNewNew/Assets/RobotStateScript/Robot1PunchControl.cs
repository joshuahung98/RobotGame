using UnityEngine;
using System.Collections;

public class Robot1PunchControl : StateMachineBehaviour {
	public GameObject robot;
	public GameObject fist;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		robot = GameObject.Find ("Robot1");
		fist = GameObject.Find ("/Robot1/pPlane3");
		robot.GetComponent<Animator> ().SetBool ("PunchAgain", false);
		robot.GetComponent<GeneralControl>().currentAnimationState = "Punch";
		fist.GetComponent<AudioSource>().Play();
		robot.GetComponent<GeneralControl> ().punchNumber += 1;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (robot.GetComponent<GeneralControl> ().intendedAnimationState == "punch" && robot.GetComponent<GeneralControl>().punchNumber <6 ) {
			robot.GetComponent<Animator>().SetBool("PunchAgain",true);
			robot.GetComponent<GeneralControl>().punchNumber+=1;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (robot.GetComponent<Animator> ().GetBool ("PunchAgain") == false) {
			robot.GetComponent<GeneralControl> ().currentAnimationState = "PunchLast";
			fist.GetComponent<AudioSource>().Stop();
		}
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

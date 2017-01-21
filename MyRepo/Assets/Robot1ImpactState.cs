using UnityEngine;
using System.Collections;

public class Robot1ImpactState : StateMachineBehaviour {

	public GameObject robot;
	public GameObject enemyRobot;
	float direction; // Face Right: -1f   Face Left: 1f


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemyRobot = GameObject.Find ("/Robot1 (1)");
		robot = GameObject.Find ("/Robot1");
		direction = enemyRobot.transform.localScale.x;
		if(direction == robot.transform.localScale.x){
			robot.transform.localScale = new Vector3(-1*robot.transform.localScale.x, robot.transform.localScale.y, robot.transform.localScale.z);
		}

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(direction == 1f){
			robot.transform.position -= new Vector3(0.01f, 0f,0f);
		}else{
			robot.transform.position += new Vector3(0.01f,0f,0f);
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

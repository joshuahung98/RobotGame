using UnityEngine;
using System.Collections;

public class BodyScriptPlayer : MonoBehaviour {
	public GameObject player;
	public GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerStay(Collider other){
		if(enemy.GetComponent<EnemyScript>().currentAnimationState == "Punch"){
			if(other.gameObject.tag == "Hand"){
				player.GetComponent<GeneralControl>().intendedAnimationState = "impact";
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

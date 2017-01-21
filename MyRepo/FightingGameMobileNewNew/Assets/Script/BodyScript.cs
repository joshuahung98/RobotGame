using UnityEngine;
using System.Collections;

public class BodyScript : MonoBehaviour {
	public GameObject player;
	public GameObject enemy;
	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerStay(Collider other){
		if(player.GetComponent<GeneralControl>().currentAnimationState == "Punch"){
			if(other.gameObject.tag == "Hand"){
				enemy.GetComponent<EnemyScript>().intendedAnimationState = "impact";
			}
		}
		if (other.gameObject.tag == "Rocket") {
			enemy.GetComponent<EnemyScript>().intendedAnimationState = "impact";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

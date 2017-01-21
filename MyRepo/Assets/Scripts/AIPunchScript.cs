using UnityEngine;
using System.Collections;

public class AIPunchScript: MonoBehaviour {

	public GameObject player;
	public GameObject enemy;
	public int characterHealth = 100; 
	void Start () {
	}

	void OnTriggerEnter(Collider other){
		if(enemy.GetComponent<EnemyAI>().currentAnimationStateAI == "Punch"){
			if(other.gameObject.tag == "Body"){
				player.GetComponent<GeneralControl>().intendedAnimationState = "impact";
				characterHealth = characterHealth - 10; 
				Debug.Log ("Character Health: " + characterHealth); 
			}
		}
	}

	void Update () {
		if(characterHealth <= 0)
		{
			Destroy(GameObject.FindWithTag("Player"));
		}
	}
	
}
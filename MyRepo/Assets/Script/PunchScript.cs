using UnityEngine;
using System.Collections;

public class PunchScript : MonoBehaviour {

	public GameObject player;
	public GameObject enemy;
	public static int EnemyHealth = 100; 
	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider other){
		if(player.GetComponent<GeneralControl>().currentAnimationState == "Punch"){
			if(other.gameObject.tag == "Body"){
				enemy.GetComponent<EnemyScript>().intendedAnimationState = "impact";
				EnemyHealth = EnemyHealth - 10; 
				Debug.Log (EnemyHealth); 
				EnemyAI.numberOfPunches++; 
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if(EnemyHealth <= 0)
		{
			Destroy(GameObject.FindWithTag("Enemy"));
		 }

	}
}

using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {

	private Transform myTransform;
	public float timer = 0f;
	public float destroyTime; 
	private float movSpeed = 0.07f; 
	public GameObject explosion;
	public GameObject enemy;
	
	// Use this for initialization
	void Start () {
		myTransform = GetComponent <Transform> (); 
		enemy = GameObject.Find ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 
		if (timer > destroyTime) {
			Destroy (this.gameObject);
		}
		if (myTransform.rotation.y == 180f) {
			Vector3 move = new Vector3 (movSpeed, 0, 0); 
			myTransform.Translate (move);
		} else {
			Vector3 move = new Vector3 (-movSpeed, 0, 0); 
			myTransform.Translate (move);
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Body") {
			enemy.GetComponent<EnemyScript>().intentionHealth -= 5f;
			Instantiate (explosion,myTransform.position + new Vector3(0.7f,0f,0f),myTransform.rotation);
			Destroy(this.gameObject);
		}
	}

}

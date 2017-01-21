using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour {

	public int characterHealth = 1000; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (characterHealth <= 0) {
			Destroy (this.gameObject);
		}
	}
	void OnTriggerEnter(Collider col)
	{
		characterHealth = characterHealth-20; 
	}
}

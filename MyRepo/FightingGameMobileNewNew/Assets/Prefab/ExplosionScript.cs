using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	public float time;
	// Use this for initialization
	void Start () {
		time = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 0.2f) {
			Destroy(this.gameObject);
		}
	}
}

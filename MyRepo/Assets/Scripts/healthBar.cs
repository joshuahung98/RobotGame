using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthBar : MonoBehaviour {
	public Slider playerHealth;
	public Slider enemyHealth;

	public int eHPInit;
	public int pHPInit;
	// Use this for initialization
	void Start () {
		playerHealth.value = 1;
		enemyHealth.value = 1;
		eHPInit = 100;
		eHPInit = PunchScript.EnemyHealth;
	}
	
	// Update is called once per frame
	void Update () {
		enemyHealth.value = PunchScript.EnemyHealth/eHPInit;
	}
}

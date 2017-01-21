using UnityEngine;
using System.Collections;

public class uiFunctions : MonoBehaviour {

	public int hTP;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void startGame() {
		Application.LoadLevel (2);
	}

	public void howToPlay() {
		Application.LoadLevel (1);

	}

	public void quitToMain () {
		Application.LoadLevel (0);

	}

	public void quit () {
		Application.Quit ();
	}
}

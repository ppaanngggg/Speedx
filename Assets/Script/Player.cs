using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	static public int score = 0;
	public Text text;

	int life = 4;

	// Use this for initialization
	void Start () {
		text.text = "Score: " + score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score.ToString ();
	}

	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject);
		if (life > 0) {
			Creater.speed -= 0.01f;
			life -= 1;
		} else {
			Application.LoadLevel(1);
		}
	}
}

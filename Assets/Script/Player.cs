using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	static public int score = 0;
	public Text text;

	Slider healthSlider;

	int life = 4;

	// Use this for initialization
	void Start () {
		text.text = "Score: " + score.ToString ();

		// Init the health indicator(slider) on the HUD to 'life'
		healthSlider = GameObject.FindWithTag ("HealthSlider").GetComponent<Slider>();
		healthSlider.maxValue = life;
		healthSlider.value = life;
		Debug.Log ("I have " + healthSlider.maxValue.ToString() + " life now");
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score.ToString ();
	}

	void OnTriggerEnter(Collider other) {
		Destroy (other.gameObject);
		if (life > 1) {
			Creater.speed -= 0.01f;
			life -= 1;
			healthSlider.value -= 1;
			Debug.Log("Bang! you ran into the cube, you have " + life.ToString() + " life left.");
		} else {
			Application.LoadLevel(1);
		}

		iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.1f, 0.1f, 0.1f), 1.5f);
	}
}

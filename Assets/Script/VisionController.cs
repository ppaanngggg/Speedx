using UnityEngine;
using System.Collections;

public class VisionController : MonoBehaviour {
	public float blinkSpeed;

	Renderer r;

	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Color color = r.material.color;
		color.a = (Mathf.Sin (Time.time * blinkSpeed) + 1)/2;
		r.material.color = color;
	}
}

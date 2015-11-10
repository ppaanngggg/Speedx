using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Creater : MonoBehaviour {

	public GameObject Quad;
	public GameObject Cube;
	public Renderer visionRenderer;
//	public Renderer crackRenderer;

	public static float speed = 0.12f;
	public static float angle = 0.0f;

	// params for game
	const int lenBatch = 7;
	const int paddingBatch = 0;
	const int totalBatch = lenBatch + 2 * paddingBatch;
	const int centerBatch = totalBatch / 2;
	const int initBatch = 5;
	const int roundBatch = 12;	// number of quads in a circle
	const int hinderMod = 5;
	const float angleSpeed = 2.0f;
	const float disQuad = 1.86f;
	const float disCube = 1.43f;
	Color[] colorList = {
		new Color (1.0f, 0.5f, 0.5f),
		new Color (0.5f, 1.0f, 0.5f),
		new Color (0.5f, 0.5f, 1.0f)
	};
	const float speedAdd = 0.002f;
	
	private float lastQuadZ = 0.0f;
	private bool firstBatch = true;

	double timePassed = 0.0f;

	 
	void Start () {
		// set the vision to transparent.
		Color color = visionRenderer.material.color;
		color.a = 0;
		visionRenderer.material.color = color;

//		color = crackRenderer.material.color;
//		color.a = 0.3f;
//		crackRenderer.material.color = color;


		for (int i = 0; i < initBatch; i++)
			create ();
	}
	
	void Update () {
		lastQuadZ -= speed;
		if (lastQuadZ < initBatch * totalBatch)
			create ();

		// use Input.GetAxis instead, to improve control accuracy.
		angle -= angleSpeed * Input.GetAxis ("Horizontal");

		trigSomethingHard ();
	}


	bool isBlinking = false;
	float blinkSpeed = 2;
	float blink_start_time = -1f;	//-1 mark the start of blink

	bool isBlind = false;
	float blind_start_time = -1f;

	void trigSomethingHard(){
		// trigger blinking.
		if (!isBlinking && !isBlind && Random.Range(0, 1000) < 1) {
			isBlinking = true;

			blink_start_time = Time.time;
		}

		if (isBlinking) {
			blink ();
			warning(blink_start_time);


			if (Time.time - blink_start_time > 8) {
				isBlinking = false;
				blink_start_time = -1f;
			}
		}

		if (!isBlinking && !isBlind && Random.Range (0, 1000) > 998) {
			isBlind	= true;

			blind_start_time = Time.time;
		}

		if (isBlind) {
			warning (blind_start_time);

			if(Time.time - blind_start_time > 20){
				isBlind = false;
				blind_start_time = -1f;
			}
		}
	}

	public Text warningText;


	void blinkingWarning(){
		warningText.text = "Warning!";

		/* end warning after 3s */
		if (Time.time - blink_start_time >= 3) {
			warningText.text = "";
		}
	}

	void warning(float start_time){
		warningText.text = "Warning!";
		
		/* end warning after 3s */
		if (Time.time - start_time >= 3) {
			warningText.text = "";
		}
	}

	void blink(){
		Color color = visionRenderer.material.color;
		color.a = (Mathf.Sin (Time.time * blinkSpeed) + 1)/2;
		visionRenderer.material.color = color;
	}

	// create a batch of quads
	void create () {
		int hinder = Random.Range (0, roundBatch);	//the offset of a group of hinders	
		int colorInd = Random.Range (0, colorList.Length);	// color index
		int mod = roundBatch / Random.Range (1, hinderMod + 1);	// one hinder for every [mod] quads in a circle.

		for (int i = 0; i < totalBatch; i++) {
			for (int j = 0; j < roundBatch; j++) {
				// create quads of batch
				Vector3 pos = new Vector3 (0, disQuad, lastQuadZ + i);
				Vector3 rotate = new Vector3 (-90, 0, 0);
				GameObject tmpQuad = (GameObject)Instantiate (Quad);
				// set special color
				if (!firstBatch && (j % mod == hinder % mod) && i >= paddingBatch && i < paddingBatch + lenBatch) {
					float rate = Mathf.Abs(i - centerBatch) / (float)centerBatch / 10;
					if(!isBlind)
						tmpQuad.GetComponent<Renderer>().material.SetColor("_Color", colorList[colorInd] * (1.0f - rate) + Color.white * rate);
					else
						tmpQuad.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
				} else {
					tmpQuad.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
				}
				tmpQuad.transform.Translate (pos);
				tmpQuad.transform.Rotate (rotate);
				tmpQuad.transform.RotateAround (new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), j * 30);

				// create hinder of batch
				if (!firstBatch && (j % mod == hinder % mod) && i == centerBatch) {
					GameObject tmpCube = (GameObject)Instantiate(Cube);
					if(!isBlind)
						tmpCube.GetComponent<Renderer>().material.SetColor("_Color", colorList[colorInd]);
					else
						tmpCube.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
					pos.y = disCube;
					tmpCube.transform.Translate (pos);
					tmpCube.transform.RotateAround (new Vector3(0,0,0), new Vector3(0,0,1), j* 30);
				}
			}
		}
		firstBatch = false;
		lastQuadZ += totalBatch;

		Player.score += 1;
		speed += speedAdd;
	}
}

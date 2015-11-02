using UnityEngine;
using System.Collections;


public class Quad : MonoBehaviour {

	Vector3 savedPosition = new Vector3();
	Quaternion savedRotation = new Quaternion();

	void Start () {
		// save init position and rotation
		Vector3 tmpPosition = this.transform.position;
		savedPosition.x = tmpPosition.x; savedPosition.y = tmpPosition.y; savedPosition.z = tmpPosition.z;
		Quaternion tmpRotation = this.transform.rotation;
		savedRotation.x = tmpRotation.x; savedRotation.y = tmpRotation.y; savedRotation.z = tmpRotation.z; savedRotation.w = tmpRotation.w;
	}
	
	void Update () {
		// move closer
		savedPosition.z -= Creater.speed;
		// set saved pos and rot
		this.transform.position = savedPosition;
		this.transform.rotation = savedRotation;
		// rotate to current angle
		this.transform.RotateAround (new Vector3(0,0,0), new Vector3(0,0,1), Creater.angle);

		// out of screen
		if (this.transform.position.z < -1)
			Destroy (this);
	}
}

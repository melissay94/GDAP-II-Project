using UnityEngine;
using System.Collections;

public class alpacaController : MonoBehaviour {

	private Animator animator;
	int currentDir = 0;
	Vector3 position;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		var horizontal = Input.GetAxis ("Horizontal");

		if (horizontal > 0) {
			animator.SetInteger ("Direction", 3);
			currentDir = 0;

			position = this.transform.position;
			position.x+= 0.05f;
			this.transform.position = position;

		} else if (horizontal < 0) {
			animator.SetInteger ("Direction", 1);
			currentDir = 2;

			position = this.transform.position;
			position.x-= 0.05f;
			this.transform.position = position;
		} 

		if (horizontal == 0 && currentDir == 0) {
			animator.SetInteger ("Direction", 0);
		} 
		else if (horizontal == 0 && currentDir == 2) {
			animator.SetInteger("Direction", 2);
		}
	}
}

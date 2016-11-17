using UnityEngine;
using System.Collections;

public class ButtonAnim : MonoBehaviour {

	void OnMouseDown(){
		transform.localScale =Vector3.one*0.9f;
	}

	void OnMouseUp(){
		transform.localScale =Vector3.one;
	}
}

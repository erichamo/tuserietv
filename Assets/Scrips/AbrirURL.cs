using UnityEngine;
using System.Collections;

public class AbrirURL : MonoBehaviour {

	public string link;
	public Sprite pressOn, pressOff;
	
	void OnMouseDown(){
		this.GetComponent<SpriteRenderer>().sprite = pressOn;
	}
	
	void OnMouseUp(){
		this.GetComponent<SpriteRenderer>().sprite = pressOff;
			Application.OpenURL(link);
	}
}

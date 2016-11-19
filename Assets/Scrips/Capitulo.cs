using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Capitulo : MonoBehaviour {
	public Text nombre;
	internal string linkURL = "https://openload.co/f/GA6SxHBTxMc";

	void OnMouseUpAsButton(){
		Application.OpenURL(linkURL);
	}
}

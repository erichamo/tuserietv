using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Capitulo : MonoBehaviour {
	public Text nombre;
	internal string linkURL;
	internal string linkDownload;

	void OnMouseUpAsButton(){
		Application.OpenURL(linkURL);
	}
}

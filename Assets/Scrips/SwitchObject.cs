using UnityEngine;
using System.Collections;

public class SwitchObject : MonoBehaviour {

	public GameObject[] objectx;

	void Start () {
	
	}

	void OnMouseUpAsButton(){
		foreach(GameObject objecx in objectx){
			objecx.SetActive(!objecx.activeSelf);
		}
	}
}

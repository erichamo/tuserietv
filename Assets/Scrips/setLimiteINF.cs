using UnityEngine;
using System.Collections;

public class setLimiteINF : MonoBehaviour {

	public Lean.Touch.LeanSideCamera2D leanCamera;
	public float limiteINF;

	void OnMouseUpAsButton(){
		leanCamera.limINF = limiteINF;
	}
}

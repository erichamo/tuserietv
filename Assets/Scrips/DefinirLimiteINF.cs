using UnityEngine;
using System.Collections;

public class DefinirLimiteINF : MonoBehaviour {
	public Lean.Touch.LeanSideCamera2D leanCamera;

	internal float limiteINF;

	void Start(){
		limiteINF = 0;
	}

	void OnMouseUpAsButton(){
		leanCamera.limINF = limiteINF;
	}
}

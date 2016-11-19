using UnityEngine;
using System.Collections;

public class setLimiteINF : MonoBehaviour {

	public Lean.Touch.LeanSideCamera2D leanCamera;
	public float limiteINFSerie;

	void OnMouseUpAsButton(){
		leanCamera.limINF = limiteINFSerie;
	}
}

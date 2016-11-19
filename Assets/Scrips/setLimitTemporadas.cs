using UnityEngine;
using System.Collections;

public class setLimitTemporadas : MonoBehaviour {

	public Lean.Touch.LeanSideCamera2D leanCamera;

	internal float limiteINFseries;

	void OnMouseUpAsButton(){
		leanCamera.limINF = limiteINFseries;
	}
}

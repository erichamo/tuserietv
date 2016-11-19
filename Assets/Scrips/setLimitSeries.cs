using UnityEngine;
using System.Collections;

public class setLimitSeries : MonoBehaviour {

	public Lean.Touch.LeanSideCamera2D leanCamera;

	internal float limiteINFseries;

	void OnMouseUpAsButton(){
		leanCamera.limINF = limiteINFseries;
	}
}

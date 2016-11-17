using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IrPantalla : MonoBehaviour {

	public GameObject pantalla; 
	public bool MovIzq, MovDer;

	internal bool mover;

	private float posFinalX;

	void Start(){
		mover = false;
	}

	public void OnMouseUpAsButton(){
		mover = true;
	}

	void Update(){
		if(mover && MovDer){
			if(pantalla.transform.position.x>Camera.main.transform.position.x){
				Camera.main.transform.position += 0.5f*Vector3.right;
			}else{
				if(Camera.main.transform.position.x >= pantalla.transform.position.x){
					Camera.main.transform.position = new Vector3(pantalla.transform.position.x,pantalla.transform.position.y,-10); 
					mover = false;
				}
			}
		}

		if(mover && MovIzq){
			if(pantalla.transform.position.x<Camera.main.transform.position.x){
				Camera.main.transform.position += 0.5f*Vector3.left;
			}else{
				if(Camera.main.transform.position.x <= pantalla.transform.position.x){
					Camera.main.transform.position = new Vector3(pantalla.transform.position.x,pantalla.transform.position.y,-10); 
					mover = false;
				}
			}
		}
	}
}

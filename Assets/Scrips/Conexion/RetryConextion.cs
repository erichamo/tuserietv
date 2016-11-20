using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetryConextion : MonoBehaviour {

	TestConection conexion;
	public Text textoRetry, textoCancel;

	void Start () {
		conexion = (TestConection)(FindObjectOfType(typeof(TestConection)));
	}

	void OnMouseUpAsButton(){
		foreach(Transform boton in conexion.botones.transform){
			if(boton.GetComponent<BoxCollider2D>()!=null) boton.GetComponent<BoxCollider2D>().enabled = false;
			if(boton.GetComponent<SpriteRenderer>()!=null) boton.GetComponent<SpriteRenderer>().enabled = false;
		}
		conexion.textConection.text = "Loading...";
		textoRetry.gameObject.SetActive(false);
		textoCancel.gameObject.SetActive(false);
		conexion.loadingImage.SetActive(true);
		conexion.Start();
	}
}

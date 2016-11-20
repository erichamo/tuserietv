using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.UI;

public class TestConection : MonoBehaviour {

	public Text textConection;
	public GameObject botones,loadingImage;
	public Text textoRetry, textoCancel;

	public GameObject fondo;
	public GameObject GoApp;

	WWW conexion;

	public void Start(){
		conexion = new WWW("https://docs.google.com/uc?export=download&id=0BwymD5zXtSN5dERHc3ZGOXNYWlE");
		StartCoroutine(TestConnection());
	}


	public IEnumerator TestConnection(){
		yield return conexion;

		if (conexion.error != null)
		{
			yield return new WaitForSeconds(1.5f);
			textConection.text = "Faild to connect to internet";
			botones.SetActive(true);
			foreach(Transform boton in botones.transform){
				if(boton.GetComponent<BoxCollider2D>()!=null) boton.GetComponent<BoxCollider2D>().enabled = true;
				if(boton.GetComponent<SpriteRenderer>()!=null) boton.GetComponent<SpriteRenderer>().enabled = true;
			}
			textoRetry.gameObject.SetActive(true);
			textoCancel.gameObject.SetActive(true);
			loadingImage.SetActive(false);
			//yield return new WaitForSeconds(2);// trying again after 2 sec
			//StartCoroutine(TestConnection());
		}
		else
		{
			yield return new WaitForSeconds(1);
			loadingImage.SetActive(false);
			textConection.text = "Connected";
			while (!conexion.isDone)
			{
				textConection.text = "Connected";
				yield return null;
			}
			GetComponent<Animator>().SetBool("complete",true);
			fondo.GetComponent<SpriteRenderer>().sprite = Sprite.Create(conexion.texture,new Rect(0,0, 720, 1280),new Vector2(0.5f,0.5f));
			GoApp.SetActive(true);
			// do somthing, play sound effect for example
			//yield return new WaitForSeconds(5);// recheck if the internet still exists after 5 sec
			//StartCoroutine(TestConnection());

		}
	}
}

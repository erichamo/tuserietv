using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Serie : MonoBehaviour {
	public Text nombre;

	void OnMouseUpAsButton(){
		SerieController serieControl = (SerieController) FindObjectOfType(typeof(SerieController));
		serieControl.seriePlay = this;

		TemporadasController temporadaControl = (TemporadasController)(FindObjectOfType(typeof(TemporadasController)));
		temporadaControl.screenLoading.gameObject.SetActive(true);
		StartCoroutine(temporadaControl.descargaFileTemporadas(nombre.text.ToString()));
	}
}

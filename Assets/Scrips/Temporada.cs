using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Temporada : MonoBehaviour {
	public Text nombre;
	internal int numTemporada;
	internal int numCapitulos;

	void OnMouseUpAsButton(){
		SerieController serieControl = (SerieController) FindObjectOfType(typeof(SerieController));

		CapitulosController capituloControl = (CapitulosController)(FindObjectOfType(typeof(CapitulosController)));
		capituloControl.crear_ListaCapitulos(serieControl.seriePlay.nombre.text,numTemporada,numCapitulos);
	}
}

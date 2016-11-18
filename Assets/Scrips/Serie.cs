using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Serie : MonoBehaviour {
	public Text nombre;

	void OnMouseUpAsButton(){
		TemporadasController temporadaControl = (TemporadasController)(FindObjectOfType(typeof(TemporadasController)));
		temporadaControl.crear_ListaTemporadas(nombre.text);
	}
}

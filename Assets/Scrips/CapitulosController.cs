using UnityEngine;
using System.Collections;

public class CapitulosController : MonoBehaviour {

	public Capitulo Capitulo_Base;

	private int num_capitulos;

	Lean.Touch.LeanSideCamera2D limiteINF;

	setLimitTemporadas limitCapitulos;

	void Start () {
		num_capitulos = 0;
		limiteINF = ( Lean.Touch.LeanSideCamera2D)(FindObjectOfType(typeof( Lean.Touch.LeanSideCamera2D)));
		limitCapitulos = (setLimitTemporadas)(FindObjectOfType(typeof(setLimitTemporadas)));
	}

	public void crear_ListaCapitulos(string name_capitulo, int numCapitulos){
		limpiarLista();
		num_capitulos = numCapitulos;

		for(int i= 1; i<= num_capitulos; i++){			
			GameObject capituloTemp = (GameObject)(Instantiate(Capitulo_Base.gameObject,transform));
			capituloTemp.name = "Capitulo "+i.ToString();
			capituloTemp.GetComponent<Capitulo>().nombre.text = capituloTemp.name;
			capituloTemp.transform.localPosition = new Vector3(Capitulo_Base.gameObject.transform.localPosition.x,9f-1.5f*i,Capitulo_Base.gameObject.transform.localPosition.z);
			capituloTemp.GetComponent<BoxCollider2D>().enabled = true;
			capituloTemp.GetComponent<SpriteRenderer>().enabled = true;

			limiteINF.limINF = capituloTemp.transform.localPosition.y;
			limitCapitulos.limiteINFseries = capituloTemp.transform.localPosition.y;

		}
		if(limiteINF.limINF<=-1.5f) limiteINF.limINF+=1.5f;
	}

	private void limpiarLista(){
		foreach(Transform temp in transform){
			if(temp.gameObject.name!="Capitulo_X")	Destroy(temp.gameObject);
		}
	}
}

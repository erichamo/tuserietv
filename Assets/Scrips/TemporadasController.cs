using UnityEngine;
using System.Collections;

public class TemporadasController : MonoBehaviour {

	public Temporada Temporada_Base;

	private TextAsset lista_temporadas;
	private int num_temporadas;

	Lean.Touch.LeanSideCamera2D limiteINF;

	setLimitTemporadas limitTemporadas;

	void Start () {
		num_temporadas = 0;
		limiteINF = ( Lean.Touch.LeanSideCamera2D)(FindObjectOfType(typeof( Lean.Touch.LeanSideCamera2D)));
		limitTemporadas = (setLimitTemporadas)(FindObjectOfType(typeof(setLimitTemporadas)));
	}

	public void crear_ListaTemporadas(string name_serie){
		limpiarLista();
		string nameSerieTemp = name_serie.Remove(name_serie.Length-1,1);
		lista_temporadas = Resources.Load<TextAsset>("Files/Series/"+nameSerieTemp+"/"+nameSerieTemp);
		string content = lista_temporadas.text;
		string valor = "";

		while(valor!=null){
			GameObject temporadaTemp = (GameObject)(Instantiate(Temporada_Base.gameObject,transform));
			temporadaTemp.name = "Temporada "+(content.Split("\n"[0])[num_temporadas].ToString()).Split(","[0])[0].ToString();
			temporadaTemp.GetComponent<Temporada>().numCapitulos = int.Parse((content.Split("\n"[0])[num_temporadas].ToString()).Split(","[0])[1].ToString());
			temporadaTemp.GetComponent<Temporada>().nombre.text = temporadaTemp.name;
			temporadaTemp.transform.localPosition = new Vector3(Temporada_Base.gameObject.transform.localPosition.x,7.5f-1.5f*num_temporadas,Temporada_Base.gameObject.transform.localPosition.z);
			temporadaTemp.GetComponent<BoxCollider2D>().enabled = true;
			temporadaTemp.GetComponent<SpriteRenderer>().enabled = true;

			limiteINF.limINF = temporadaTemp.transform.localPosition.y;
			limitTemporadas.limiteINFseries = temporadaTemp.transform.localPosition.y;

			num_temporadas++;
			if(content.Split("\n"[0])[num_temporadas] == "*") break;
		}
		if(limiteINF.limINF<=-1.5f) limiteINF.limINF+=1.5f;
	}

	private void limpiarLista(){
		num_temporadas = 0;
		foreach(Transform temp in transform){
			if(temp.gameObject.name!="Temporada_X")	Destroy(temp.gameObject);
		}
	}
}

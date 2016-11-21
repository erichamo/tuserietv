using UnityEngine;
using System.Collections;

public class CapitulosController : MonoBehaviour {

	public Capitulo Capitulo_Base;

	private int num_capitulos;

	Lean.Touch.LeanSideCamera2D limiteINF;

	setLimitTemporadas limitCapitulos;

	public LoadingScreen screenLoading;

	private TemporadasController temporadaControl;

	void Start () {
		temporadaControl = (TemporadasController)(FindObjectOfType(typeof(TemporadasController)));

		num_capitulos = 0;
		limiteINF = ( Lean.Touch.LeanSideCamera2D)(FindObjectOfType(typeof( Lean.Touch.LeanSideCamera2D)));
		limitCapitulos = (setLimitTemporadas)(FindObjectOfType(typeof(setLimitTemporadas)));
	}

	public void crear_ListaCapitulos(string name_capitulo, int numTemporada ,int numCapitulos){
		limpiarLista();
		num_capitulos = numCapitulos;

		for(int i= 1; i<= num_capitulos; i++){			
			GameObject capituloTemp = (GameObject)(Instantiate(Capitulo_Base.gameObject,transform));
			if(i<10){
				capituloTemp.name = "Capitulo 0"+i.ToString();
			}else{
				capituloTemp.name = "Capitulo "+i.ToString();
			}
			capituloTemp.GetComponent<Capitulo>().nombre.text = capituloTemp.name;
			capituloTemp.transform.localPosition = new Vector3(Capitulo_Base.gameObject.transform.localPosition.x,9f-1.5f*i,Capitulo_Base.gameObject.transform.localPosition.z);
			capituloTemp.GetComponent<BoxCollider2D>().enabled = true;
			capituloTemp.GetComponent<SpriteRenderer>().enabled = true;

			asignarLink(capituloTemp.GetComponent<Capitulo>(),numTemporada,i);

			limiteINF.limINF = capituloTemp.transform.localPosition.y;
			limitCapitulos.limiteINFseries = capituloTemp.transform.localPosition.y;

		}
		if(limiteINF.limINF<=-1.5f) limiteINF.limINF+=1.5f;
	}

	void asignarLink(Capitulo capitulo, int numTemporada, int numCapitulo){
		string nameCapituloTemp;
		if(numCapitulo<10){
			nameCapituloTemp = numTemporada.ToString()+"x0"+numCapitulo.ToString();
		}else{
			nameCapituloTemp = numTemporada.ToString()+"x"+numCapitulo.ToString();
		}
		//print(nameCapituloTemp);
		string content = temporadaControl.lista_capitulos_series;
		string valor = "";
		int i = 0;

		while(valor!=null){
			
			if(nameCapituloTemp == (content.Split("\n"[0])[i].ToString()).Split(","[0])[0].ToString()) {
				capitulo.linkURL = (content.Split("\n"[0])[i].ToString()).Split(","[0])[1].ToString();
				capitulo.linkDownload = (content.Split("\n"[0])[i].ToString()).Split(","[0])[2].ToString();
				break;
			}

			i++;
			if(content.Split("\n"[0])[i] == "*") break;
		}
	}

	private void limpiarLista(){
		foreach(Transform temp in transform){
			if(temp.gameObject.name!="Capitulo_X")	Destroy(temp.gameObject);
		}
	}
}

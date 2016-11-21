using UnityEngine;
using System.Collections;

public class TemporadasController : MonoBehaviour {

	public Temporada Temporada_Base;

	private string lista_temporadas;
	internal string textoInfoTemporadas;
	private int num_temporadas;

	Lean.Touch.LeanSideCamera2D limiteINF;

	setLimitTemporadas limitTemporadas;
	public LoadingScreen screenLoading;

	void Start () {
		num_temporadas = 0;
		limiteINF = ( Lean.Touch.LeanSideCamera2D)(FindObjectOfType(typeof( Lean.Touch.LeanSideCamera2D)));
		limitTemporadas = (setLimitTemporadas)(FindObjectOfType(typeof(setLimitTemporadas)));

	}


	public IEnumerator descargaFileTemporadas(string name_serie)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW("https://docs.google.com/uc?export=download&id=0BwymD5zXtSN5MmRhUWdkODRVX2M");
		yield return www;

		if(www.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(descargaFileTemporadas(name_serie));
		}
		else
		{
			print("connected to internet");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(1);// recheck if the internet still exists after 5 sec
			while (!www.isDone)
			{
				yield return null;
			}
			screenLoading.gameObject.SetActive(false);
			textoInfoTemporadas = www.text;
			cargarInfoTemporada(name_serie);
		}

	}
		

	void cargarInfoTemporada(string name_serie){
		string urlInfoTEmp = "";
		string nameSerieTemp = name_serie.Remove(name_serie.Length-1,1);
		string content = textoInfoTemporadas;
		string valor = "";
		int i = 0;

		while(valor!=null){
			if(nameSerieTemp == (content.Split("\n"[0])[i].ToString()).Split(","[0])[0].ToString()) {
				urlInfoTEmp = (content.Split("\n"[0])[i].ToString()).Split(","[0])[1].ToString();
				break;
			}

			i++;
			if(content.Split("\n"[0])[i] == "*") break;
		}
		StartCoroutine(descargaInfoTemporadas(urlInfoTEmp.Remove(urlInfoTEmp.Length-1,1)));
	}


	IEnumerator descargaInfoTemporadas(string urlInfoTempSerie)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW(urlInfoTempSerie);
		yield return www;

		if(www.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(1);// trying again after 2 sec
			StartCoroutine(descargaInfoTemporadas(urlInfoTempSerie));
		}
		else
		{
			print("connected to internet");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(2);// recheck if the internet still exists after 5 sec
			while (!www.isDone)
			{
				yield return null;
			}
			screenLoading.gameObject.SetActive(false);
			lista_temporadas = www.text;
			crear_ListaTemporadas();
		}

	}




	public void crear_ListaTemporadas(){
		limpiarLista();
		string content = lista_temporadas;
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

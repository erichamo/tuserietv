using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TemporadasController : MonoBehaviour {

	public Temporada Temporada_Base;

	private string lista_temporadasINFO;
	internal string textoInfoTemporadas;
	internal string lista_capitulos_seriesINFO,lista_capitulos_series;
	private int num_temporadas;

	internal List<List<List<string>>> temporadasXcapitulos;

	Lean.Touch.LeanSideCamera2D limiteINF;

	setLimitTemporadas limitTemporadas;
	public LoadingScreen screenLoading;

	void Start () {
		temporadasXcapitulos = new List<List<List<string>>>();
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
			print("no se encontro: ubicacion de la informacion de las temporadas de la series");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(descargaFileTemporadas(name_serie));
		}
		else
		{
			print("Descargando... ubicacion de la informacion de las temporadas de la series");
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
		StartCoroutine(descargaInfoTemporadas(urlInfoTEmp.Remove(urlInfoTEmp.Length-1,1),name_serie));
	}


	IEnumerator descargaInfoTemporadas(string urlInfoTempSerie, string name_serie)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW(urlInfoTempSerie);
		yield return www;

		if(www.error != null)
		{
			print("no se encontro: archivo de la temporada de la serie");
			yield return new WaitForSeconds(1);// trying again after 2 sec
			StartCoroutine(descargaInfoTemporadas(urlInfoTempSerie,name_serie));
		}
		else
		{
			print("Descargando... archivo de la temporada de la serie");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(2);// recheck if the internet still exists after 5 sec
			while (!www.isDone)
			{
				yield return null;
			}
			screenLoading.gameObject.SetActive(false);
			lista_temporadasINFO = www.text;
			StartCoroutine(descargaFileCapitulos(name_serie));
			//crear_ListaTemporadas();
		}

	}


	public IEnumerator descargaFileCapitulos(string name_serie)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW("https://docs.google.com/uc?export=download&id=0BwymD5zXtSN5bnBYYU14ZkpoTUU");
		yield return www;

		if(www.error != null)
		{
			print("no se encontro: ubicacion de la informacion de los capitulos de las temporadas");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(descargaFileCapitulos(name_serie));
		}
		else
		{
			print("Descargando... ubicacion de la informacion de los capitulos de las temporadas");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(1);// recheck if the internet still exists after 5 sec
			while (!www.isDone)
			{
				yield return null;
			}
			screenLoading.gameObject.SetActive(false);
			lista_capitulos_seriesINFO = www.text;
			cargarInfoCapitulos(name_serie);
		}

	}

	void cargarInfoCapitulos(string name_serie){
		string urlInfoTEmp = "";
		string nameSerieTemp = name_serie.Remove(name_serie.Length-1,1);
		string content = lista_capitulos_seriesINFO;
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
		StartCoroutine(descargaInfoCapitulos(urlInfoTEmp.Remove(urlInfoTEmp.Length-1,1)));
	}


	IEnumerator descargaInfoCapitulos(string urlInfoTempSerie)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW(urlInfoTempSerie);
		yield return www;

		if(www.error != null)
		{
			print("no se encontro: informacion de los capitulos de las temporadas");
			yield return new WaitForSeconds(1);// trying again after 2 sec
			StartCoroutine(descargaInfoCapitulos(urlInfoTempSerie));
		}
		else
		{
			print("Descargando... informacion de los capitulos de las temporadas");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(2);// recheck if the internet still exists after 5 sec
			while (!www.isDone)
			{
				yield return null;
			}
			screenLoading.gameObject.SetActive(false);
			lista_capitulos_series = www.text;
			crear_ListaTemporadas(lista_capitulos_series);
		}
	}


	public void crear_ListaTemporadas(string lista_capitulosX){
		limpiarLista();
		string content = lista_temporadasINFO;
		string valor = "";

		while(valor!=null){
			GameObject temporadaTemp = (GameObject)(Instantiate(Temporada_Base.gameObject,transform));
			temporadaTemp.GetComponent<Temporada>().numTemporada =int.Parse((content.Split("\n"[0])[num_temporadas].ToString()).Split(","[0])[0].ToString());

			if(temporadaTemp.GetComponent<Temporada>().numTemporada<10){
				temporadaTemp.name = "Temporada 0"+temporadaTemp.GetComponent<Temporada>().numTemporada.ToString();
			}else{
				temporadaTemp.name = "Temporada "+temporadaTemp.GetComponent<Temporada>().numTemporada.ToString();
			}

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

		llenarList(num_temporadas,lista_capitulosX);
	}
		

	void llenarList(int numTemporadas, string contentX){
		string content = contentX;
		temporadasXcapitulos = new List<List<List<string>>>();
		int numCapitulosTemp = 1;
		for(int tempActual = 1; tempActual<=numTemporadas; tempActual++){
			List<List<string>> temporadaListTemp = new List<List<string>>();
			string numCapitulo = (content.Split("\n"[0])[numCapitulosTemp-1].ToString().Split(","[0])[0].ToString());
			numCapitulo = numCapitulo.Remove(numCapitulo.Length-3,3);

			while(int.Parse(numCapitulo)==tempActual){
				List<string> view_Download = new List<string>();
				view_Download.Add(content.Split("\n"[0])[numCapitulosTemp-1].ToString().Split(","[0])[1].ToString());
				view_Download.Add(content.Split("\n"[0])[numCapitulosTemp-1].ToString().Split(","[0])[2].ToString());
				temporadaListTemp.Add(view_Download);

				numCapitulosTemp++;
				numCapitulo = (content.Split("\n"[0])[numCapitulosTemp-1].ToString().Split(","[0])[0].ToString());
				numCapitulo = numCapitulo.Remove(numCapitulo.Length-3,3);

				if(content.Split("\n"[0])[numCapitulosTemp] == "*") break;
			}
			temporadasXcapitulos.Add(temporadaListTemp);
		}
	}


	private void limpiarLista(){
		num_temporadas = 0;
		foreach(Transform temp in transform){
			if(temp.gameObject.name!="Temporada_X")	Destroy(temp.gameObject);
		}
	}
}
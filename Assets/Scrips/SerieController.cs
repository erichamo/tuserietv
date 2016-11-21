using UnityEngine;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class SerieController : MonoBehaviour {

	public Serie Serie_Base;
	//internal TextAsset lista_series;

	internal string lista_series,listaImagesSeries;

	DefinirLimiteINF limiteINF;
	setLimitSeries limiteSeries;
	private int num_Series;

	internal Serie seriePlay;

	public LoadingScreen screenLoading;

	private Sprite imageSerieTemp = null;

	void Start () {
		limiteINF = (DefinirLimiteINF)(FindObjectOfType(typeof(DefinirLimiteINF)));
		limiteSeries = (setLimitSeries)(FindObjectOfType(typeof(setLimitSeries)));
		num_Series = 0;

		StartCoroutine(DownloadSeriesInfo());

		//crear_ListaSeries();
	}

	IEnumerator DownloadSeriesInfo()
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW("https://docs.google.com/uc?export=download&id=0BwymD5zXtSN5c3FYLTJnWHdRRU0");
		yield return www;

		if(www.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(DownloadSeriesInfo());
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
			lista_series = www.text;
			screenLoading.gameObject.SetActive(false);
			StartCoroutine(DownloadInfoImagesSeries());
		}

	}

	IEnumerator DownloadInfoImagesSeries()
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW www = new WWW("https://docs.google.com/uc?export=download&id=0BwymD5zXtSN5Z3dSZzh2Ry1PQ2M");
		yield return www;

		if(www.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(2);// trying again after 2 sec
			StartCoroutine(DownloadInfoImagesSeries());
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
			listaImagesSeries = www.text;
			screenLoading.gameObject.SetActive(false);
			crear_ListaSeries();
		}

	}


	private void crear_ListaSeries(){
		string content = lista_series;
		string valor = "";

		while(valor!=null){
			GameObject serieTemp = (GameObject)(Instantiate(Serie_Base.gameObject,transform));
			serieTemp.name = (content.Split("\n"[0])[num_Series].ToString());
			serieTemp.GetComponent<Serie>().nombre.text = serieTemp.name;
			serieTemp.transform.localPosition = new Vector3(Serie_Base.gameObject.transform.localPosition.x,4.5f-1.5f*num_Series,Serie_Base.gameObject.transform.localPosition.z);
			extrerSprite(serieTemp.name.Remove(serieTemp.name.Length-1,1),serieTemp);

			limiteINF.limiteINF = serieTemp.transform.localPosition.y+3f;
			limiteSeries.limiteINFseries = serieTemp.transform.localPosition.y+3f;

			num_Series++;
			if(content.Split("\n"[0])[num_Series] == "*") break;
		}
		Destroy(Serie_Base.gameObject);
	}

	public void extrerSprite(string nameSerie, GameObject spriteImageX){
		string urlInfoImageTEmp = "";
		string content = listaImagesSeries;
		string valor = "";
		int i = 0;

		while(valor!=null){
			if(nameSerie == (content.Split("\n"[0])[i].ToString()).Split(","[0])[0].ToString()) {
				urlInfoImageTEmp = (content.Split("\n"[0])[i].ToString()).Split(","[0])[1].ToString();
				break;
			}

			i++;
			if(content.Split("\n"[0])[i] == "*") break;
		}
		StartCoroutine(DownloadImagesSeries(urlInfoImageTEmp.Remove(urlInfoImageTEmp.Length-1,1),spriteImageX));
	}
		
	IEnumerator DownloadImagesSeries(string urlImage, GameObject spriteImage)
	{
		screenLoading.gameObject.SetActive(true);
		//link del archivo
		WWW imageDownload = new WWW(urlImage);
		yield return imageDownload;

		if(imageDownload.error != null)
		{
			print("faild to connect to internet, trying after 2 seconds.");
			yield return new WaitForSeconds(1);// trying again after 2 sec
			StartCoroutine(DownloadImagesSeries(urlImage,spriteImage));
		}
		else
		{
			print("connected to internet");
			// do somthing, play sound effect for example
			yield return new WaitForSeconds(1);// recheck if the internet still exists after 5 sec
			while (!imageDownload.isDone)
			{
				yield return null;
			}
			spriteImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(imageDownload.texture,new Rect(0,0, 128, 128),new Vector2(0.5f,0.5f));
			screenLoading.gameObject.SetActive(false);
		}

	}

}

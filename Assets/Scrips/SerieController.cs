using UnityEngine;
using System.Collections;

public class SerieController : MonoBehaviour {

	public Serie Serie_Base;
	public TextAsset lista_series;

	private int num_Series;

	void Start () {
		num_Series = 0;
		crear_ListaSeries();
	}

	private void crear_ListaSeries(){
		string content = lista_series.text;
		string valor = "";

		while(valor!=null){
			GameObject serieTemp = (GameObject)(Instantiate(Serie_Base.gameObject,transform));
			serieTemp.name = (content.Split("\n"[0])[num_Series].ToString());
			serieTemp.GetComponent<Serie>().nombre.text = serieTemp.name;
			serieTemp.transform.localPosition = new Vector3(Serie_Base.gameObject.transform.localPosition.x,4.5f-1.5f*num_Series,Serie_Base.gameObject.transform.localPosition.z);
			serieTemp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Series_Images/"+serieTemp.name.Remove(serieTemp.name.Length-1,1));

			DefinirLimiteINF limiteINF = (DefinirLimiteINF)(FindObjectOfType(typeof(DefinirLimiteINF)));
			limiteINF.limiteINF = serieTemp.transform.localPosition.y+3f;

			num_Series++;
			if(content.Split("\n"[0])[num_Series] == "*") break;
		}
		Destroy(Serie_Base.gameObject);
	}

}

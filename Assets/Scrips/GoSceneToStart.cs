using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoSceneToStart : MonoBehaviour {

	void Start () {
		StartCoroutine(GoScene());
	}

	IEnumerator GoScene(){
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(1);
	}
}

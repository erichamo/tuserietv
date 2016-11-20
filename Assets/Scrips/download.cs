using UnityEngine;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class download : MonoBehaviour {
    public Text progress;
    Texture2D img;

    void Start()
    {
        StartCoroutine(DownloadAndCache());
    }

    IEnumerator DownloadAndCache()
    {
        //link del archivo
        WWW www = new WWW("https://docs.google.com/uc?export=download&id=0B3qlOaKY2f9MVk1Uck5ZX0xtOEU");
        yield return www;

        if(www.error != null)
        {
            print("faild to connect to internet, trying after 2 seconds.");
            yield return new WaitForSeconds(2);// trying again after 2 sec
            StartCoroutine(DownloadAndCache());
        }
        else
        {
            print("connected to internet");
            // do somthing, play sound effect for example
            yield return new WaitForSeconds(2);// recheck if the internet still exists after 5 sec
            while (!www.isDone)
            {
                progress.text = "downloaded " + (www.progress * 100).ToString() + "%...";
                yield return null;
            }
            string fullPath = Application.persistentDataPath + "/series.txt";
            byte[] bytes = www.bytes;
            print(www.text);
            File.WriteAllBytes(fullPath, bytes);
            print(fullPath);
            progress.text = "downloaded, unzipping...";
            //Application.OpenURL(fullPath);
            //StartCoroutine(DownloadAndCache());
        }

    }

/*
    void AllDone()
    {
        Debug.Log("Download Complete");
    }

    ////This is the coroutine I created int he hopes that I could get the download to run in the background.
    IEnumerator downlodTexture()
    {
        yield return 0;
        WWW www = new WWW("https://openload.co/stream/SQHJpXRLjtc~1479708081~190.238.0.0~Rrt1i6TL?mime=true");
        yield return www;
        img = www.texture;
    }
    
    void OnGUI ()
    {
        GUILayout.Label(img);
    }
    
    IEnumerator TestConnection()
    {
        yield return www;

        if (www.error != null)
        {
            print("faild to connect to internet, trying after 2 seconds.");
            yield return new WaitForSeconds(2);// trying again after 2 sec
            StartCoroutine(TestConnection());
        }
        else
        {
            print("connected to internet");
            // do somthing, play sound effect for example
            yield return new WaitForSeconds(5);// recheck if the internet still exists after 5 sec
            StartCoroutine(TestConnection());

        }
    }
*/
}

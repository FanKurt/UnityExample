using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class qwe : MonoBehaviour {
	void Start(){
		guiText.text="1235";
	}
	void OnGUI(){  
		
		if(GUI.Button(new Rect(50,80,500,90), "show AlertDialog")) {
			// 新增一個 AndroidJavaClass, 屬性預設就為 com.unity3d.player.UnityPlayer 即可
			using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
				// 得到目前的 Activity
				AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
				// 呼叫剛剛我們自己寫的 showAlertDialog, 並帶入參數
				jo.Call("OpenPhoto");
			}
		}
	}

	void GetPath(string path){
		guiText.text=path;
		StartCoroutine ("Download",path);
	}

	IEnumerator Download(string path){
//		Application.CaptureScreenshot("/mnt/sdcard/DCIM/Camera/screenshot.jpg");
		WWW.LoadFromCacheOrDownload ("file:///"+Application.persistentDataPath+"/20140623_133003.jpg",0);
		string [] imageName =path.Split('/');
		WWW www = new WWW ("file:///"+Application.persistentDataPath+"/"+imageName[imageName.Length-1]);
		Debug.Log ("www  :"+www);
		yield return www;
		GameObject.Find("Label").GetComponent<UILabel>().text =Application.persistentDataPath;
		Debug.Log ("texelSize  :"+www.texture.texelSize);
		Debug.Log ("name  :"+www.texture.name);
		GameObject.Find ("Texture").GetComponent<UITexture> ().mainTexture = www.texture;

	}
}

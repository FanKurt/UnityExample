using UnityEngine;
using System.Collections;

public class call_android : MonoBehaviour {
	public GameObject scene ,Draw;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}
	
	void to_android( ){
		string scene_num =_01_scene.Scene_Num;
		Debug.Log("1    "+Game.fir+Game.sec+Game.thr+scene_num);
		#if UNITY_ANDROID
		 using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using( AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
				{
					jo.Call("StartSpenActivity",Game.fir+Game.sec+Game.thr+scene_num);
				}
			}
		#endif
	}

	void from_android(){
		scene.SetActive (false);
		Draw.SetActive (true);
	}
	
	
}

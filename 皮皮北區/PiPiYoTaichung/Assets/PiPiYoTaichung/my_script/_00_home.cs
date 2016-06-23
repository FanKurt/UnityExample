using UnityEngine;
using System.Collections;

public class _00_home : MonoBehaviour {
	public GameObject cloud;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
		float x =cloud.GetComponent<UITexture>().uvRect.x;
		cloud.GetComponent<UITexture>().uvRect = new Rect(x+0.0005f , 0f,1f,1f);
	}
	void show_URL(){
		Application.OpenURL ("http://www.north.taichung.gov.tw/lp.asp?CtNode=1689&CtUnit=117&BaseDSD=7&mp=132010");


	}
}

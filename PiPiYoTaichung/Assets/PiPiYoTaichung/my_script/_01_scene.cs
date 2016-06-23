using UnityEngine;
using System.Collections;

public class _01_scene : MonoBehaviour {
	public GameObject title , _02_pic , _02_title ;
	public GameObject [] btn = new GameObject[3];
	public static string Scene_Num;
	public GameObject draw_TextureA;
	public GameObject draw_TextureB;
	public Texture [] textureA = new Texture[16];
	public Texture [] textureB = new Texture[16];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		title.GetComponent<UISprite>().fillAmount += 0.005f;
		if (title.GetComponent<UISprite> ().fillAmount == 1) 
			title.GetComponent<BoxCollider>().enabled = false;
	}
	
	void SelectScene(GameObject gb){
		string i = gb.name.Substring(3,2);
		Debug.Log (i);
		Scene_Num=i;
		draw_TextureA.GetComponent<UITexture>().mainTexture = textureA[int.Parse(i)];
		draw_TextureB.GetComponent<UITexture>().mainTexture = textureB[int.Parse(i)];
//		_02_pic.GetComponent<UISprite>().spriteName = "02_pic"+i;
//		_02_title.GetComponent<UISprite>().spriteName = "02_title"+i;
//		resetWord();
//		int array_num = int.Parse(i);
//		word[array_num-1].SetActive(true);
	}
//	void resetWord(){
//		for(int j=1;j<=6;j++){
//			word[j-1].SetActive(false);
//		}
//	}

	void resetTitle(){
		title.GetComponent<UISprite>().fillAmount = 0f;
	}
}

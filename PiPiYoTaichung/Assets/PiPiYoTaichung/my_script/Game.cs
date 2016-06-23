using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject [] a = new GameObject[5];
	public GameObject [] b = new GameObject[5];
	public GameObject [] c = new GameObject[5];
	public GameObject [] life = new GameObject[5];
	public GameObject [] Level = new GameObject[3];
	public GameObject pass_pic , pass , game , fail;
	public static int correct=0, TIME;
	public static bool t_star = false;
	public static string fir="0",sec="0",thr="0";
	public GameObject Level1 , Level2 , Level3  ; 
	public GameObject stamp1,stamp2,stamp3;
	// Use this for initialization
	void Start () {
		TIME = 60+Mathf.FloorToInt(Time.time);

	}
	
	// Update is called once per frame
	void Update () {
		if(t_star){
			int t = TIME-Mathf.FloorToInt(Time.time);
			GameObject.Find("time").GetComponent<UILabel>().text=t+"";
			if(GameObject.Find("time").GetComponent<UILabel>().text=="0"){
				game.SetActive(false);
				fail.SetActive(true);
			}
		}
	
		
	}
	void A(GameObject gb){
		int i = int.Parse(gb.name);
		a[i-1].SetActive(true);	
		correct++;
		GameObject.Find(""+i).GetComponent<BoxCollider>().enabled = false;
		life[correct-1].SetActive(true);
		if(correct == 5){
			StartCoroutine("A_delay");
			stamp1.SetActive(true);
		}
	}
	
	void B(GameObject gb){
		Debug.Log("1");
		int i = int.Parse(gb.name);
		b[i-1].SetActive(true);	
		correct++;
		GameObject.Find(""+i).GetComponent<BoxCollider>().enabled = false;
		life[correct-1].SetActive(true);
		if(correct == 5){
			StartCoroutine("B_delay");
			stamp2.SetActive(true);
		}
	}
	
	void C(GameObject gb){
		Debug.Log("1");
		int i = int.Parse(gb.name);
		c[i-1].SetActive(true);	
		correct++;
		GameObject.Find(""+i).GetComponent<BoxCollider>().enabled = false;
		life[correct-1].SetActive(true);
		if(correct == 5){
			StartCoroutine("C_delay");
			stamp3.SetActive(true);
		}
	}
	
	IEnumerator A_delay(){
		yield return new WaitForSeconds(1.5f);
		GameObject aa =Resources.Load("main_game")as GameObject;
		Level[0].GetComponent<UISprite>().atlas =aa.GetComponent<UIAtlas>() ;
		Level[0].GetComponent<UISprite>().spriteName = "level1_done";
		pass_pic.GetComponent<UISprite>().spriteName = "rabbit";
		GameObject.Find("A").SetActive(false);
		gameObject.SetActive(false);
		pass.SetActive(true);
		fir = "1";
	}
	
	IEnumerator B_delay(){
		yield return new WaitForSeconds(1.5f);
		GameObject aa =Resources.Load("main_game")as GameObject;
		Level[1].GetComponent<UISprite>().atlas =aa.GetComponent<UIAtlas>() ;
		Level[1].GetComponent<UISprite>().spriteName = "level2_done";
		pass_pic.GetComponent<UISprite>().spriteName= "frog";
		GameObject.Find("B").SetActive(false);
		gameObject.SetActive(false);
		pass.SetActive(true);
		sec = "1";
	}
	
	IEnumerator C_delay(){
		yield return new WaitForSeconds(1.5f);
		GameObject aa =Resources.Load("main_game")as GameObject;
		Level[2].GetComponent<UISprite>().atlas =aa.GetComponent<UIAtlas>() ;
		Level[2].GetComponent<UISprite>().spriteName = "level3_done";
		pass_pic.GetComponent<UISprite>().spriteName= "dog";
		GameObject.Find("C").SetActive(false);
		gameObject.SetActive(false);
		pass.SetActive(true);
		thr = "1";
	}
	
	void resetFail(){
		Debug.Log("11234");
	}

	void close(){
		Debug.Log ("a");
		int i = main_game.LV;
		Debug.Log (i);
		if(i==1){
			Level1.SetActive(false);
		}
		else if(i==2){
			Level2.SetActive(false);
		}
		else{
			Level3.SetActive(false);
		}
	}
}

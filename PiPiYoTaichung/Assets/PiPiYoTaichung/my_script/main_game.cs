using UnityEngine;
using System.Collections;

public class main_game : MonoBehaviour {
	public GameObject game , Level1 , Level2 , Level3  ; 
	public GameObject [] life = new GameObject[5];
	static bool reset= false;
    public static int LV;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SelectLevel(GameObject gb){
		string i =gb.name.Substring(5,1);
		Game.TIME =  60+Mathf.FloorToInt(Time.time);
		Game.correct = 0;
		LV = int.Parse(i);
		if(i=="1"){
			gameObject.SetActive(false);
			game.SetActive(true);
			Level1.SetActive(true);
			Game.t_star = true;
			ResetTweenScale("A");
			reset = true;
			
		}
		else if(i=="2"){
			gameObject.SetActive(false);
			game.SetActive(true);
			Level2.SetActive(true);
			Game.t_star = true;
			ResetTweenScale("B");
			reset = true;
		}
		else{
			gameObject.SetActive(false);
			game.SetActive(true);
			Level3.SetActive(true);
			Game.t_star = true;
			ResetTweenScale("C");
			reset = true;
		}
	}
	
	void ResetTweenScale(string lv){
		if(reset){
			for(int j=1;j<=5;j++){
				life[j-1].GetComponent<TweenScale>().enabled = true;
				life[j-1].transform.localScale = new Vector3(1f,1f,1f);
				life[j-1].GetComponent<TweenScale>().Reset();
				life[j-1].SetActive(false);
				GameObject.Find(""+j).GetComponent<BoxCollider>().enabled = true;
				if(GameObject.Find(lv+"_circle"+j))
				GameObject.Find(lv+"_circle"+j).SetActive(false);
			}
		}
	}
	
	
}

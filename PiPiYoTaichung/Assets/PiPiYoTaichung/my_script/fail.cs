using UnityEngine;
using System.Collections;

public class fail : MonoBehaviour {
	public 	GameObject game,A,B,C ;
	public GameObject [] life = new GameObject[5];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void again(){
		int lv = main_game.LV;
		gameObject.SetActive(false);
		game.SetActive(true);
		Game.correct = 0;
		Game.TIME =  60+Mathf.FloorToInt(Time.time);
		if(lv == 1){
			A.SetActive(true);
			ResetTweenScale("A");
		}
		else if(lv == 2){
			B.SetActive(true);
			ResetTweenScale("B");
		}
		else{
			C.SetActive(true);
			ResetTweenScale("C");
		}
	}
	
	
	void ResetTweenScale(string lv){
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

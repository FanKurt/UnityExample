using UnityEngine;
using System.Collections;
using System.IO;
public class DrawLine : MonoBehaviour {
	// The DrawLinesTouch script adapted to work with mouse input
	public GameObject [] setContent= new GameObject[5];
	private bool [] Bcontent= new bool[5];
	public Material lineMaterial ;
	public int maxPoints = 1000;
	public float lineWidth = 10.0f;
	public int minPixelMove = 5;	// Must move at least this many pixels per sample for a new segment to be recorded
	public Material[] lineMaterials;
	private Vector2[] linePoints ;
	private VectorLine line ;
	private int lineIndex = 0;
	private Vector2 previousPosition ;
	private int sqrMinPixelMove ;
	private bool canDraw = false;
	int draw_line_index = 1;
	bool stamp_show = false;
	public GameObject stampview;
	public GameObject label , scene;
	public GameObject [] stamp_prefab = new GameObject[6];
	bool Mouse_state = false;
	// Use this for initialization
	void Start () {
		init();
		for(int i =0;i<=4;i++){
			Bcontent[i] = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePos = Input.mousePosition;
		Ray ray2 = Camera.main.ScreenPointToRay (Input.mousePosition);
	    	//hit用来存储碰撞物体的信息
		RaycastHit hit2;
		Debug.DrawRay(transform.position, Vector3.forward, Color.green);
	   	 //ray表示射线，hit存储物体的信息,1000为设定射线发射的距离
	 	if(Physics.Raycast (ray2, out hit2,1000)){
//			Debug.Log(hit2.transform.gameObject.name);
			if( hit2.transform.gameObject.name == "draw_area"){
				if (Input.GetMouseButtonUp(0) ) {
					hit2.transform.GetComponent<BoxCollider>().center = new Vector3(0,0,0);
				}
				if (Input.GetMouseButtonDown(0) ) {
					hit2.transform.GetComponent<BoxCollider>().center = new Vector3(0,0,-1);
					if(GameObject.Find("Vector DrawLine"+draw_line_index)){
						Destroy(GameObject.Find("Vector DrawLine"+draw_line_index));
					}
					init();
					line.name = "DrawLine"+draw_line_index.ToString();
					line.ZeroPoints();
					line.minDrawIndex = 0;
					line.Draw();
					GameObject.Find("Vector DrawLine" + (draw_line_index)).tag = "line";
					Debug.Log(GameObject.Find("Vector DrawLine" + (draw_line_index)).tag);
					previousPosition = linePoints[0] = mousePos;
					lineIndex = 0;
					canDraw = true;
					draw_line_index++ ;
				}

		//		 && (mousePos - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw
				else if (Input.GetMouseButton(0)&& (mousePos - previousPosition).sqrMagnitude  > sqrMinPixelMove && canDraw ) {
					previousPosition = linePoints[++lineIndex] = mousePos;
					line.minDrawIndex = lineIndex-1;
					line.maxDrawIndex = lineIndex;
					if (lineIndex >= maxPoints-1) canDraw = false;
					line.Draw();
				}
		 	}
		}
		
		
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			draw_previous();
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			draw_next();
		}
		if(Input.GetKeyDown(KeyCode.Delete)){
			draw_delete();
		}
		
	}
	void init(){
		linePoints = new Vector2[maxPoints];
		line = new VectorLine("DrawnLine", linePoints, lineMaterial, lineWidth, LineType.Continuous);
		sqrMinPixelMove = minPixelMove*minPixelMove;
		line.joins = Joins.Weld;
	}
	void MouseOver(){
		Mouse_state = true;
	}
	void MouseOut(){
		Mouse_state = false;
	}
	void draw_previous(){
		if((draw_line_index-1) >0){
			GameObject.Find("Vector DrawLine" + (draw_line_index-1)).renderer.enabled = false ;
			draw_line_index --;
		}
	}
	void draw_next(){
		if(GameObject.Find("Vector DrawLine" + (draw_line_index))){
			GameObject.Find("Vector DrawLine" + (draw_line_index)).renderer.enabled = true ;
			draw_line_index ++;
		}
	}
	void draw_delete(){
		Debug.Log("del");
		for (int i = 1; i <= draw_line_index; i++)
        {
			GameObject line = GameObject.Find("Vector DrawLine" + i);
            Destroy(line);
        }
		draw_line_index = 1;
	}
	void set_meatrial(GameObject obj){
		setContent [0].SetActive (false);
		lineMaterial = lineMaterials[int.Parse( obj.name)-1];
	}

	void setColorView(){
		Debug.Log (Bcontent [0]);
		if (Bcontent [0]) {
			setContent [0].SetActive (true);
			Bcontent [0]=false;
		}
		else{
			setContent [0].SetActive (false);
			Bcontent [0]=true;
		}
	}

	void eraser(){
		Debug.Log (1);
		for(int i= 1;i<=draw_line_index;i++){
			Destroy(GameObject.Find("Vector DrawLine"+i));
		}
		draw_line_index = 1;
	}
	void stamp(){
		if (stamp_show) {
			stampview.SetActive(false);
			stamp_show =false;
		} else {
			stampview.SetActive(true);
			stamp_show =true;
		}
	}
	void creat_stamp( GameObject gb){
		string i = gb.name.Substring(5,1);
		GameObject stamp_clone = Instantiate ( stamp_prefab[int.Parse(i)-1] ) as GameObject;
		stamp_clone.transform.parent = gameObject.transform;
		stamp_clone.transform.localScale = stamp_prefab [int.Parse (i) - 1].transform.localScale;
		stamp_clone.transform.localPosition = new Vector3 (256, 0, -3);
		stampview.SetActive(false);
		stamp_show =false;

	}
	void save(){
		StartCoroutine ("GetCapture");
	}

	void exit(){
		GameObject [] line = GameObject.FindGameObjectsWithTag("line");
		for(int i=0 ;i<line.Length ;i++){
			Destroy(line[i]);
		}
		
		GameObject [] stamp = GameObject.FindGameObjectsWithTag("stamp");
		for(int i=0 ;i<stamp.Length ;i++){
			Destroy(stamp[i]);
		}

		gameObject.SetActive (false);
		scene.SetActive (true);

	}

	IEnumerator GetCapture()
		
	{
		
		yield return new WaitForEndOfFrame();
		Debug.Log (123);
		int width = Screen.width;
		
		int height = Screen.height;
		
		Texture2D tex = new Texture2D(width /100 * 44,height /10 *8,TextureFormat.RGB24,false);
		
		tex.ReadPixels(new Rect( width/2 ,height /10,width/100 * 94,height/10*9),0,0,true);
		tex.Apply ();
		byte[] imagebytes = tex.EncodeToPNG();//转化为png图
		
		tex.Compress(false);//对屏幕缓存进行压缩

		//image.mainTexture = tex;//对屏幕缓存进行显示（缩略图）
		
		File.WriteAllBytes( Application.persistentDataPath  + "/pipi.png",imagebytes);//存储png图




		
	}
}

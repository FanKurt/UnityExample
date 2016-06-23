using UnityEngine;
using System.Collections;

class WebViewCallbackTest : Kogarasi.WebView.IWebViewCallback
{
	public void onLoadStart( string url )
	{
		Debug.Log( "call onLoadStart : " + url );
	}
	public void onLoadFinish( string url )
	{
		Debug.Log( "call onLoadFinish : " + url );
	}
	public void onLoadFail( string url )
	{
		Debug.Log( "call onLoadFail : " + url );
	}
}

public class WebViewTest : MonoBehaviour
{

	WebViewCallbackTest m_callback;
	WebViewBehavior webview ;
	void Start () {
		Debug.Log ("width  :" + Screen.width);
		Debug.Log ("height  :" + Screen.height);
	}
	//1,920  1080
	void OnGUI() {
		if (GUI.Button (new Rect (50, 50, 200, 200), "Taipei"))
			LoadUrl ("http://ems.nutc-imac.tk/map/index.php?long=121.5615012&lat=25.0854062&zoom=9&mark=121.5615012,25.0854062");
		
		if (GUI.Button(new Rect(250, 250, 200, 200), "Taichung"))
			LoadUrl ("http://ems.nutc-imac.tk/map/index.php?long=120.9558744&lat=24.220103&zoom=9&mark=120.9558744,24.220103");

	}

	void LoadUrl(string url){
		m_callback = new WebViewCallbackTest();
		webview = GetComponent<WebViewBehavior>();
		if( webview != null )
		{
			webview.LoadURL( url );
			webview.SetVisibility( true );
			webview.SetMargins(0,500,0,0);
			webview.setCallback( m_callback );
		}
	}
	
}

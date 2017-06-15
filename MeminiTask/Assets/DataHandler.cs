using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DataHandler : MonoBehaviour {

	string loginURL = "https://dgrzlyzw4nhan.cloudfront.net/auth/login/";
	string revisionListURL = "https://dgrzlyzw4nhan.cloudfront.net/profile/due/";

	public Text emailText, passwordText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SubmitPOST()
	{
		//creating the new jsonObject with the email and password inputed by user
		JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
		jsonObject.AddField ("email", emailText.text);
		jsonObject.AddField ("password", passwordText.text);

		StartCoroutine (Post(loginURL, jsonObject.ToString()));
	}

	//Function to post the json file to the url for authentication
	IEnumerator Post(string url, string jsonBodyText)
	{
		//creates new request and new body of data to then post
		var request = new UnityWebRequest(url, "POST");
		byte[] bodyRawText = new System.Text.UTF8Encoding().GetBytes(jsonBodyText);

		//initialize the upload handler and download handler
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw (bodyRawText);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();

		//set the header 
		request.SetRequestHeader("Content-Type", "application/json");

		//send the request
		yield return request.Send();	

		Debug.Log ("Reponse: " + request.downloadHandler.text);
	}

}

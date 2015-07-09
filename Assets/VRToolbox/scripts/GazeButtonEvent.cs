using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GazeButtonEvent : MonoBehaviour {
	public float timeToClick = 2.0f;
	public Image progressImage;
	public GameObject targetObject;
	public string eventName;
	bool isGazing = false;
	float startTime = 0f;
	
	public void OnPointerEnter() {
		isGazing = true;
		startTime = Time.time;
		progressImage.fillAmount = 0f;
	}
	
	public void OnPointerExit() {
		isGazing = false;
		progressImage.fillAmount = 0f;
	}	
	
	void Update () {
		if (isGazing) {
			float progress = (Time.time - startTime) / timeToClick;
			progressImage.fillAmount = progress;
			if (progress >= 1.0f) {
				if (targetObject!=null) targetObject.SendMessage (eventName, SendMessageOptions.DontRequireReceiver);
				isGazing = false;
				progressImage.fillAmount = 0f;
			}
		}
	}
	
	public void Test ()
	{ Debug.Log ("YEEESS");}
}

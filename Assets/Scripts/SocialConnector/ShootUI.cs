using UnityEngine;

public class ShootUI : MonoBehaviour
{
	GUIStyle style;

	void Start()
	{
		style = new GUIStyle ();
		style.fontSize = 100;
	}

	int count = 0;
	void OnGUI()
	{
		Rect rect = new Rect (10, 100, 400, 300);
		GUI.Label (rect, count.ToString (), style);
		if (GUILayout.Button ("<size=30><b>CaptureAndShareImage</b></size>", GUILayout.Height (60))) {
			GetComponent<CaptureAndShareImage>().Shoot ();
		}
		count++;
	}
}

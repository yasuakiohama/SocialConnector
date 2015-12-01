using UnityEngine;
using System;
using System.Collections;

public class CaptureAndShareImage : MonoBehaviour
{
	private const string URL = "http://www.desunoya.sakura.ne.jp/product/panda/";
	private const string MESSAGE = "#一撃入魂！ぱんだボール";
	private const string SCREENSHOTS = "screenshots/";
	private const string EXTENSION = ".png";
	private string fileName = "";

	void Awake()
	{
		DirectoryUtils.SafeCreateDirectory (GetImgPath);
		DirectoryUtils.DeleteDirectoryFiles (GetImgPath, "*" + EXTENSION);
	}

	private string GetImgPath {
		get {
			#if UNITY_EDITOR
			return System.IO.Directory.GetCurrentDirectory () + "/" + SCREENSHOTS;
			#elif UNITY_IPHONE
			return Application.persistentDataPath + "/" + SCREENSHOTS;
			#elif UNITY_ANDROID
			return Application.persistentDataPath + "/" + SCREENSHOTS;
			#else
			return Application.persistentDataPath + "/" + SCREENSHOTS;
			#endif
		}
	}

	/// <summary>
	/// Shoot this instance.
	/// </summary>
	public void Shoot ()
	{
		CreateScreenshotsFileName ();
		Application.CaptureScreenshot (SCREENSHOTS + "/" + fileName);
		StartCoroutine (CaptureImageWriteWaiting ());
	}

	/// <summary>
	/// スクリーンショットを撮ったファイル名を時間を利用して作成する
	/// </summary>
	private void CreateScreenshotsFileName()
	{
		fileName = DateTime.Now.ToString ("yyyyMMddhhmmssfff") + ".png";
	}

	/// <summary>
	/// キャプチャーの画像が作成されるまでまつ
	/// </summary>
	/// <returns>The image write waiting.</returns>
	/// <param name="imgPath">Image path.</param>
	/// <param name="message">Message.</param>
	IEnumerator CaptureImageWriteWaiting()
	{
		while (!System.IO.File.Exists (GetImgPath + fileName)) {
			//Debug.Log ("Temporary ScreenShot hav not been written yet. " + GetImgPath + fileName);
			yield return null;
		}

		SocialConnector.Share (MESSAGE, URL, GetImgPath + fileName);
		yield break;
	}
}
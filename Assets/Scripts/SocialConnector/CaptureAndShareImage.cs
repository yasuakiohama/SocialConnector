using UnityEngine;
using System;
using System.Collections;

public class CaptureAndShareImage : MonoBehaviour
{
	private const string URL = "http://www.desunoya.sakura.ne.jp/product/panda/";
	private const string MESSAGE = "#ぱんだボール";
	private const string SCREENSHOTS = "screenshots/";
	private const string EXTENSION = ".png";
	private string fileName = "";
	private bool isShoot = false;

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
		if (!isShoot) {
			isShoot = true;
			CreateScreenshotsFileName ();
			StartCoroutine (CaptureImageWriteWaiting ());
		}
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
		// レイアウト設定のために1フレーム待つ
		yield return new WaitForEndOfFrame ();

		//写真撮影
		Application.CaptureScreenshot (SCREENSHOTS + "/" + fileName);

		do {
			//Debug.Log ("Temporary ScreenShot hav not been written yet. " + GetImgPath + fileName);
			//書込み処理のために1フレーム待つ
			yield return new WaitForEndOfFrame ();
		} while (!System.IO.File.Exists (GetImgPath + fileName));

		// キャプチャを保存する処理として１フレーム待つ
		yield return new WaitForEndOfFrame ();

		SocialConnector.Share (MESSAGE, URL, GetImgPath + fileName);
		isShoot = false;
		yield break;
	}
}
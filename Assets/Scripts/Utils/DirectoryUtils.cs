using System;
using System.IO;
using System.Collections;

/// <summary>
/// Directory クラスに関する汎用関数を管理するクラス
/// </summary>
public static class DirectoryUtils
{
	/// <summary>
	/// 指定したパスにディレクトリが存在しない場合
	/// すべてのディレクトリとサブディレクトリを作成します
	/// </summary>
	public static DirectoryInfo SafeCreateDirectory(string path)
	{
		if (Directory.Exists (path)) {
			return null;
		}
		return Directory.CreateDirectory (path);
	}

	/// <summary>
	/// 指定されたフォルダ以下にあるすべてのファイルを取得する
	/// </summary>
	/// <param name="folder">ファイルを検索するフォルダ名。</param>
	/// <param name="searchPattern">ファイル名検索文字列
	/// ワイルドカード指定子(*, ?)を使用する。</param>
	/// <param name="files">見つかったファイル名のリスト</param>
	public static void GetAllFiles(string folder, string searchPattern, ref ArrayList files)
	{
		//folderにあるファイルを取得する
		string[] fs = System.IO.Directory.GetFiles (folder, searchPattern);
		//ArrayListに追加する
		files.AddRange (fs);

		//folderのサブフォルダを取得する
		string[] ds = System.IO.Directory.GetDirectories (folder);
		//サブフォルダにあるファイルも調べる
		foreach (string d in ds) {
			GetAllFiles (d, searchPattern, ref files);
		}
	}

	/// <summary>
	/// 指定されたフォルダ以下にあるすべてのファイルを取得する（サブフォルダは含まれない）
	/// </summary>
	/// <param name="folder">ファイルを検索するフォルダ名。</param>
	/// <param name="searchPattern">ファイル名検索文字列
	/// ワイルドカード指定子(*, ?)を使用する。</param>
	/// <param name="files">見つかったファイル名のリスト</param>
	public static void GetDirectoryFiles(string folder, string searchPattern, ref ArrayList files)
	{
		//folderにあるファイルを取得する
		string[] fs = System.IO.Directory.GetFiles (folder, searchPattern);
		//ArrayListに追加する
		files.AddRange (fs);
	}

	/// <summary>
	/// 指定されたフォルダ以下にあるすべてのファイルを削除する（サブフォルダは含まれない）
	/// </summary>
	/// <param name="folder">ファイルを検索するフォルダ名。</param>
	/// <param name="searchPattern">ファイル名検索文字列
	/// ワイルドカード指定子(*, ?)を使用する。</param>
	public static void DeleteDirectoryFiles(string folder, string searchPattern)
	{
		ArrayList files = new ArrayList ();
		DirectoryUtils.GetDirectoryFiles (folder, searchPattern, ref files);
		foreach (string name in files) {
			System.IO.File.Delete (name);
		}
	}

	/// <summary>
	/// 指定されたフォルダ以下にあるすべてのファイルを削除する
	/// </summary>
	/// <param name="folder">ファイルを検索するフォルダ名。</param>
	/// <param name="searchPattern">ファイル名検索文字列
	/// ワイルドカード指定子(*, ?)を使用する。</param>
	public static void DeleteAllFiles(string folder, string searchPattern)
	{
		ArrayList files = new ArrayList ();
		DirectoryUtils.GetAllFiles (folder, searchPattern, ref files);
		foreach (string name in files) {
			System.IO.File.Delete (name);
		}
	}
}
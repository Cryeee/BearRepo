using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
	private static string savepath = "/fatbear.save";

	public static void Save(PlayerData data)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + savepath, FileMode.Create);

		bf.Serialize(stream, data);
		stream.Close();
	}

	public static PlayerData Load()
	{
		if(File.Exists(Application.persistentDataPath + savepath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + savepath, FileMode.Open);
			PlayerData data = bf.Deserialize(stream) as PlayerData;
			stream.Close();
			return data;
		} else
		{
			Debug.LogError("NO SAVE DATA FOUND!");
			return new PlayerData();
		}
	}
}

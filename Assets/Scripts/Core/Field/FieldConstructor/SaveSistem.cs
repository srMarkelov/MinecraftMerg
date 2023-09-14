using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Characters;
using Unity.VisualScripting;

public class SaveSistem
{
    /*public static void SaveFieldConstructorDatabase(Dictionary<(int, int), CharacterType> character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "character.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        FieldConstructorData data = new FieldConstructorData(character);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static FieldConstructorData LoadFieldConstructorDatabase()
    {
        string path = Application.persistentDataPath + "character.test";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            FieldConstructorData data = formatter.Deserialize(stream) as FieldConstructorData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file nit found in" + path);
            return null;
        }
    }*/
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public static class SaveController
{
    private static readonly string _saveFolderPath = Application.dataPath + "/Saves";

    public static void Save(SaveData objectToSave, string fileName)
    {
        StringWriter textStream = new StringWriter();

        var serializer = new XmlSerializer(typeof(SaveData));

        serializer.Serialize(textStream, objectToSave);

        if (!Directory.Exists(_saveFolderPath))
        {
            Directory.CreateDirectory(_saveFolderPath);
        }

        File.WriteAllText(_saveFolderPath + "/" + fileName + ".save", textStream.ToString());
    }

    public static SaveData Load(string fileName)
    {
        string text = File.ReadAllText(_saveFolderPath + "/" + fileName + ".save");

        var serializer = new XmlSerializer(typeof(SaveData));

        return (SaveData)serializer.Deserialize(new StringReader(text));
    }
}

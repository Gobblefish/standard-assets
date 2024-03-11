// System.
using System.IO;
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;

namespace Gobblefish {

    public abstract class Settings<TSettings> where TSettings : Settings<TSettings> {

        public static string DirectoryPath => "/settings/";
        public static string FilePath => DirectoryPath + typeof(TSettings).Name.ToLower() + ".json"; 
        
        void CheckDirectory() {
            if (!Directory.Exists(Application.persistentDataPath + DirectoryPath)) {
                Directory.CreateDirectory(Application.persistentDataPath + DirectoryPath);
            }
        }

        void CheckFile() {
            if (!File.Exists(Application.persistentDataPath + FilePath)) {
                Save();
            }
        }

        public void Save() {
            string settingsJson = ToJson();
            System.IO.File.WriteAllText(Application.persistentDataPath + FilePath, settingsJson);
        }

        public TSettings Read() {
            CheckDirectory();
            CheckFile();
            string settingsJson = System.IO.File.ReadAllLines(Application.persistentDataPath + FilePath)[0];
            TSettings data = JsonUtility.FromJson<TSettings>(settingsJson);
            return data;
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }

    }
    
}

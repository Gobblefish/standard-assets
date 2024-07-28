// System.
using System.IO;
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
using UnityEngine.Events;

namespace GobbleFish {

    public abstract class Settings<TSettings> where TSettings : Settings<TSettings> {

        public static string DirectoryPath => "/settings/";

        public string FilePath => DirectoryPath + GetFileName(); 

        protected virtual string GetFileName() {
            return typeof(TSettings).Name.ToLower() + ".json";
        }
        
        private void CheckDirectory() {
            if (!Directory.Exists(Application.persistentDataPath + DirectoryPath)) {
                Directory.CreateDirectory(Application.persistentDataPath + DirectoryPath);
            }
        }

        private void CheckFile() {
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

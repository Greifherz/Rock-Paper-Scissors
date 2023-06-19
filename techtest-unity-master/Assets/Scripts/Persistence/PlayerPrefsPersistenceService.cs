using UnityEngine;


namespace Persistence
{
    public class PlayerPrefsPersistenceService : IPersistenceService
    {
        public void Save(string data,string key)
        {
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }

        public string Load(string key)
        {
            return PlayerPrefs.GetString(key,"");
        }

        public bool HasData(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void ClearPersistence(string key = "")
        {
            if(key == "")
                PlayerPrefs.DeleteAll();
            else
            {
                PlayerPrefs.DeleteKey(key);
            }
            PlayerPrefs.Save();
        }
    }
}

using GameItems;
using Persistence;
using UnityEngine;

namespace Extensions
{
    public static class PersistenceExtensions
    {
        private const string KEY_PLAYERDATA = "playerData";

        public static void SavePlayerData(this IPersistenceService self, PlayerData data)
        {
            var serializedPlayerData = JsonUtility.ToJson(data);
            self.Save(serializedPlayerData,KEY_PLAYERDATA);
        }

        public static PlayerData LoadPlayerData(this IPersistenceService self)
        {
            var unserializedPlayerData = self.Load(KEY_PLAYERDATA);
            return JsonUtility.FromJson<PlayerData>(unserializedPlayerData);
        }

        public static void ClearPlayerData(this IPersistenceService self)
        {
            self.ClearPersistence(KEY_PLAYERDATA);
        }

        public static bool HasPlayerData(this IPersistenceService self)
        {
            return self.HasData(KEY_PLAYERDATA);
        }
    }
}


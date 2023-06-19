
    namespace Persistence
    {
        public interface IPersistenceService //Made an interface of this because for this test i'm using the player prefs. Later I might need to store elsewhere, say, a server or local txt file. The interface will be the same, the implementation not.
        {
            void Save(string data, string key);
            string Load(string key);
            bool HasData(string key);
            void ClearPersistence(string key = "");
        }
    }



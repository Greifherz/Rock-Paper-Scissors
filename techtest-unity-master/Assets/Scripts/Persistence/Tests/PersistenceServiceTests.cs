using NUnit.Framework;

namespace Persistence.Tests
{
    public class PersistenceServiceTests
    {
        [Test]
        public void TEST_PlayerPrefsPersistenceDoesNotCorruptPersistedString()
        {
            IPersistenceService service = new PlayerPrefsPersistenceService();
            var dataToPersist = "testData";
            service.Save(dataToPersist,"testKey");

            var dataFromPersistence = service.Load("testKey");
            
            service.ClearPersistence("testKey");
            
            Assert.AreEqual(dataToPersist,dataFromPersistence);
        }
        
        [Test]
        public void TEST_PlayerPrefsPersistenceDoesNotCorruptPersistedInt()
        {
            IPersistenceService service = new PlayerPrefsPersistenceService();
            var dataToPersist = 1;
            service.Save(dataToPersist.ToString(),"testKey");

            var dataFromPersistence = System.Int32.Parse(service.Load("testKey"));
            
            service.ClearPersistence("testKey");
            
            Assert.AreEqual(dataToPersist,dataFromPersistence);
        }
        
        [Test]
        public void TEST_PlayerPrefsPersistenceDoesNotCorruptPersistedBigString() //This test took 16seconds when I first ran but passed
        {
            IPersistenceService service = new PlayerPrefsPersistenceService();
            var dataToPersist = "";
            for (int i = 0; i < 50000; i++)
                dataToPersist += "ASD";
            service.Save(dataToPersist.ToString(),"testKey");

            var dataFromPersistence = service.Load("testKey");
            
            service.ClearPersistence("testKey");
            
            Assert.AreEqual(dataToPersist,dataFromPersistence);
        }

        [Test]
        public void TEST_PlayerPrefsRemoveDataWorks()
        {
            IPersistenceService service = new PlayerPrefsPersistenceService();
            
            service.Save("data","testKey");
            
            service.ClearPersistence("testKey");

            var loadedData = service.Load("testKey");
            
            Assert.IsEmpty(loadedData);
        }
    }
}

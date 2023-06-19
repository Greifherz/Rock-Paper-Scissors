using Extensions;
using GameItems;
using Persistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private GameObject continueButton;
        [SerializeField] private GameObject nameInputPanel;
        [SerializeField] private InputField nameInputField;
    
        private IPersistenceService _persistenceService;
    
        // Start is called before the first frame update
        void Start()
        {
            _persistenceService = new PlayerPrefsPersistenceService();
        
            if(!_persistenceService.HasPlayerData())
                continueButton.SetActive(false);
        }

        public void OnContinueClicked()
        {
            SceneManager.LoadScene(1);
        }

        public void OnNewGameClicked()
        {
            nameInputPanel.SetActive(true);//Maybe some animations later?
            nameInputField.text = "";
        }

        public void OnNewGameConfirmed()
        {
            var newPlayerData = new PlayerData
            {
                UserId = 1,
                Name = nameInputField.text,
                Coins = 50
            };
        
            _persistenceService.SavePlayerData(newPlayerData);
            SceneManager.LoadScene(1);
        }

        public void OnCloseNameInputPanelClicked()
        {
            nameInputPanel.SetActive(false);
        }
    }
}

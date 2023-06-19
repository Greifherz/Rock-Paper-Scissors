using UnityEngine;

//The bootstrapper handles initialization of objects. It should be run in the loading phase of the game, while the scene is being loaded.
//Without Dependency Injection, we might need one for each scene in the game

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            InitializeSingletons();

            Debug.Log("Bootstrap finished, destroying self"); //A brief comment on logs: I love them but I hate the unity's default logger on builds.

            Destroy(gameObject);
        }

        private void InitializeSingletons()
        {
            EventPipelineSystem.EventPipelineSystem.CreateEventPipelineInstance(this);
        }
    }
}

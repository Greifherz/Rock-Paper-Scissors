using System;
using System.Collections;
using System.Collections.Generic;
using EventPipelineSystem.Interfaces;
using UnityEngine;

namespace EventPipelineSystem
{
    public class EventPipelineSystem
    { 
        public static EventPipelineSystem Instance { get; private set; }

        public static void CreateEventPipelineInstance(MonoBehaviour coroutineProcessor)
        {
            if (Instance == null)
            {
                Instance = new EventPipelineSystem(coroutineProcessor);
            }
        }
    
        private event Action<IGameEvent> _eventPipeline = (gameEvent) => { };

        private Queue<IGameEvent> _eventPool = new Queue<IGameEvent>();

        private MonoBehaviour _coroutineProcessor;

        private bool _poolingRunning = false;

        private EventPipelineSystem(MonoBehaviour coroutineProcessor)
        {
            _coroutineProcessor = coroutineProcessor;
            Instance = this;
        }

        public void RegisterListener(Action<IGameEvent> listenAction)
        {
            _eventPipeline += listenAction;
        }

        public void UnregisterListener(Action<IGameEvent> listenAction)
        {
            _eventPipeline -= listenAction;
        }

        public void Raise(IGameEvent gameEvent)
        {
            _eventPool.Enqueue(gameEvent);
            if (!_poolingRunning)
                _coroutineProcessor.StartCoroutine(Pooling());
        }

        private IEnumerator Pooling()
        {
            _poolingRunning = true; //Don't like flag control, but the coroutine state is annoying to track
            while(_eventPool.Count > 0)
            {
                for (int i = 0; i < _eventPool.Count; i++)
                {
                    _eventPipeline(_eventPool.Dequeue());
                }
                yield return new WaitForEndOfFrame(); 
            }
            _poolingRunning = false;
        }
    }
}

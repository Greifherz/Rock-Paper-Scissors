using EventPipelineSystem.Interfaces;
using UnityEngine;

// I've taken liberty in adding this Adapter so it would lessen how much I would write on each event handler
// Usually I need 2 of these, one for MonoBehaviours and one for common objects. The one for MonoBehaviours shouldn't be like this one.

namespace EventPipelineSystem
{
    public abstract class MonoEventHandlerAdapter : MonoBehaviour, IEventHandler
    {
        public abstract void Visit(IGameEvent gameEvent);

        public virtual void Handle(IGameEvent gameEvent)
        {
            Debug.Log("Raw event type, nothing to do");
        }

        public virtual void Handle(IPlayerInfoEvent playerInfoEvent)
        {
            //Nothing to do on abstract handler level
        }

        public virtual void Handle(IGameOverEvent gameOverEvent)
        {
            //Nothing
        }
    }
}

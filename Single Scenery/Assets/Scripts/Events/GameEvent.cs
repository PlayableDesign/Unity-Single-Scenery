using System;
using UnityEngine;

namespace SingleScenery
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Single Scenery/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private Action gameEvent;

        public void Invoke()
        {
            gameEvent?.Invoke();
        }

        public void AddListener(Action callback)
        {
            gameEvent += callback;
        }

        public void RemoveListener(Action callback)
        {
            gameEvent -= callback;
        }
    }
}

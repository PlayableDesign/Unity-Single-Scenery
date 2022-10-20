using UnityEngine;

namespace SingleScenery
{
    public class GameOverController : UIController
    {
        [SerializeField] private GameEvent onPlayEvent;

        private void OnEnable()
        {
            onPlayEvent.AddListener(Hide);
        }

        private void OnDisable()
        {
            onPlayEvent.RemoveListener(Hide);
        }
    }
}

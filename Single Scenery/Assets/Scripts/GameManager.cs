
using System.Collections;
using UnityEngine;

namespace SingleScenery
{
    public class GameManager : MonoBehaviour
    {

        // Configuration

        [Header("Events")]
        [Space(10)]

        [SerializeField] private GameEvent onPlayEvent;

        [Space(10)]
        [Header("Managers")]
        [Space(10)]

        [SerializeField] private UIManager uiManager;

        // Unity Events

        private void OnEnable()
        {
            onPlayEvent.AddListener(OnPlay);
        }

        private void OnDisable()
        {
            onPlayEvent.RemoveListener(OnPlay);
        }

        private void Start()
        {
            StartCoroutine(LoadGameWorkflow());
        }

        // Game Event Handlers

        private void OnPlay()
        {
            Debug.Log("Play!!!!");
        }

        // Game Workflows

        private IEnumerator LoadGameWorkflow()
        {
            yield return StartCoroutine(uiManager.LoadAssets());
            uiManager.ShowLoading();

            // Pretend we're loading more stuff here...
            yield return new WaitForSeconds(3f);

            uiManager.ShowMenu();
        }



    }
}

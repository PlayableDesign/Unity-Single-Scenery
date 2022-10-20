
using System.Collections;
using UnityEngine;

namespace SingleScenery
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] private LoadingController loadingController;
        [SerializeField] private MenuController menuController;
        [SerializeField] private GameOverController gameOverController;

        public void ShowLoading()
        {
            if (loadingController.Ready)
            {
                HideAll();
                loadingController.Show();
            }
        }

        public void ShowMenu()
        {
            if (menuController.Ready)
            {
                HideAll();
                menuController.Show();
            }
        }

        public void ShowGameOver()
        {
            if (gameOverController.Ready)
            {
                HideAll();
                gameOverController.Show();
            }
        }

        public void HideAll()
        {
            loadingController.Hide();
            menuController.Hide();
            gameOverController.Hide();
        }

        public IEnumerator LoadAssets()
        {
            yield return StartCoroutine(loadingController.LoadAsync());

            yield return StartCoroutine(menuController.LoadAsync());

            yield return StartCoroutine(gameOverController.LoadAsync());

        }

        public void UnloadAssets()
        {
            loadingController.Unload();
            menuController.Unload();
            gameOverController.Unload();
        }

    }
}

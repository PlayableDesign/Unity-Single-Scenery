using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SingleScenery
{
    public class LoadingController : MonoBehaviour
    {
        [SerializeField] private AssetReference loadingCanvasPrefab;

        public bool Ready => _ready;
        private bool _ready;

        private GameObject _loadingCanvas;

        public void Load()
        {
            StartCoroutine(LoadAsync());
        }

        public IEnumerator LoadAsync()
        {
            var handle = Addressables.InstantiateAsync(loadingCanvasPrefab, transform);

            yield return handle;  // wait for async call completion

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadingCanvas = handle.Result;
                _loadingCanvas.SetActive(false);
                _ready = true;
            }
        }

        public void Unload()
        {
            _ready = false;
            Addressables.ReleaseInstance(_loadingCanvas); // will decrement refence count
        }

        public void Show()
        {
            if (_ready)
            {
                _loadingCanvas.SetActive(true);
            }
        }

        public void Hide()
        {
            if (_ready)
            {
                _loadingCanvas.SetActive(false);
            }
        }

    }
}

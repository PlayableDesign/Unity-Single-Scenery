using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SingleScenery
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private AssetReference canvasPrefab;

        public bool Ready => _ready;
        private bool _ready;

        private GameObject _canvas;

        public void Load()
        {
            StartCoroutine(LoadAsync());
        }

        public IEnumerator LoadAsync()
        {
            var handle = Addressables.InstantiateAsync(canvasPrefab, transform);

            yield return handle;  // wait for async call completion

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _canvas = handle.Result;
                _canvas.SetActive(false);
                _ready = true;
            }
        }

        public void Unload()
        {
            _ready = false;
            Addressables.ReleaseInstance(_canvas); // will decrement refence count
        }

        public void Show()
        {
            if (_ready)
            {
                _canvas.SetActive(true);
            }
        }

        public void Hide()
        {
            if (_ready)
            {
                _canvas.SetActive(false);
            }
        }
    }
}


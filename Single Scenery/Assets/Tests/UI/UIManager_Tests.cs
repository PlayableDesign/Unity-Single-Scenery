using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace SingleScenery
{
    public class UIManager_Tests
    {
        // Test Settings

        const string ADDRESS = "UIManager";
        const float DELAY = 3f;

        WaitForSeconds delay;
        UIManager manager;
        bool _setup;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            if (GameObject.FindObjectOfType<Camera>() == null)
            {
                new GameObject("Camera").AddComponent<Camera>();
            }

            delay = new WaitForSeconds(DELAY);
        }

        [UnitySetUp]
        public IEnumerator Setup()
        {

            if (_setup) yield break;

            var handle = Addressables.InstantiateAsync(ADDRESS);
            yield return handle;  // wait for async call completion

            Assert.That(handle.Status == AsyncOperationStatus.Succeeded);
            Assert.IsNotNull(handle.Result);

            manager = handle.Result.GetComponent<UIManager>();

            _setup = true;

            Debug.Log("Setup: manager is instantiated in test scene");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (manager != null)
            {
                Addressables.ReleaseInstance(manager.gameObject);
            }

            Debug.Log("Teardown: reference is released");
        }

        [UnityTest]
        public IEnumerator Show_And_Hide_Loading()
        {

            yield return manager.StartCoroutine(manager.LoadAssets());

            var canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.That(canvases.Length == 3);

            manager.ShowLoading();

            var canvas = manager.GetComponentInChildren<Canvas>();

            Assert.IsNotNull(canvas);
            Assert.IsTrue(canvas.gameObject.activeInHierarchy);

            yield return delay;

            manager.HideAll();

            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            manager.UnloadAssets();

            yield return delay;

            canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.IsEmpty(canvases);

            Debug.Log("Test: manager show and hide loading passed");

        }

        [UnityTest]
        public IEnumerator Show_And_Hide_Menu()
        {

            yield return manager.StartCoroutine(manager.LoadAssets());

            var canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.That(canvases.Length == 3);

            manager.ShowMenu();

            var canvas = manager.GetComponentInChildren<Canvas>();
            var controller = manager.GetComponentInChildren<MenuController>();

            Assert.IsNotNull(canvas);
            Assert.IsTrue(canvas.gameObject.activeInHierarchy);
            Assert.IsNotNull(controller);

            yield return delay;

            manager.HideAll();

            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            manager.UnloadAssets();

            yield return delay;

            canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.IsEmpty(canvases);

            Debug.Log("Test: manager show and hide menu passed");

        }

        [UnityTest]
        public IEnumerator Show_And_Hide_GameOver()
        {
            yield return manager.StartCoroutine(manager.LoadAssets());

            var canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.That(canvases.Length == 3);

            manager.ShowGameOver();

            var canvas = manager.GetComponentInChildren<Canvas>();
            var controller = manager.GetComponentInChildren<GameOverController>();

            Assert.IsNotNull(canvas);
            Assert.IsTrue(canvas.gameObject.activeInHierarchy);
            Assert.IsNotNull(controller);

            yield return delay;

            manager.HideAll();

            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            manager.UnloadAssets();

            yield return delay;

            canvases = manager.GetComponentsInChildren<Canvas>(true);

            Assert.IsEmpty(canvases);

            Debug.Log("Test: manager show and hide game over passed");

        }


    }

}




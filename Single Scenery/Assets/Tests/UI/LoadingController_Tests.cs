using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;

namespace SingleScenery
{
    public class LoadingController_Tests
    {
        // Test Settings

        const string ADDRESS = "LoadingController";
        const string LOADING = "LOADING";
        const float DELAY = 3f;

        WaitForSeconds delay;
        LoadingController controller;
        bool _setup;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            new GameObject("Camera").AddComponent<Camera>();
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

            controller = handle.Result.GetComponent<LoadingController>();

            Assert.IsFalse(controller.Ready);

            _setup = true;

            Debug.Log("Setup: controller is instantiated in test scene");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (controller != null)
            {
                Addressables.ReleaseInstance(controller.gameObject);
            }

            Debug.Log("Teardown: controller reference is released");
        }

        [UnityTest, Order(1), Timeout(5000)]
        public IEnumerator Step_1_Load()
        {
            controller.Load();

            yield return new WaitUntil(() => controller.Ready);

            var canvas = controller.gameObject.GetComponentInChildren<Canvas>(true);
            var text = controller.gameObject.GetComponentInChildren<TMP_Text>(true);

            Assert.That(controller.transform.childCount > 0);
            Assert.IsNotNull(canvas);
            Assert.IsNotNull(text);
            Assert.IsFalse(canvas.gameObject.activeInHierarchy);
            Assert.That(text.text == LOADING);

            Debug.Log("Test: controller load passed");
            yield return delay;
        }

        [UnityTest, Order(2)]
        public IEnumerator Step_2_Show()
        {
            controller.Show();

            var canvas = controller.gameObject.GetComponentInChildren<Canvas>(true);
            Assert.IsTrue(canvas.gameObject.activeInHierarchy);

            Debug.Log("Test: controller show passed");
            yield return delay;
        }

        [UnityTest, Order(3)]
        public IEnumerator Step_3_Hide()
        {
            controller.Hide();

            var canvas = controller.gameObject.GetComponentInChildren<Canvas>(true);
            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            Debug.Log("Test: controller hide passed");
            yield return delay;
        }

        [UnityTest, Order(4)]
        public IEnumerator Step_4_Unload()
        {
            controller.Unload();
            yield return delay;
            Assert.That(controller.transform.childCount == 0);
            Debug.Log("Test: controller unload passed");
        }

    }

}


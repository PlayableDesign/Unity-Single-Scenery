using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace SingleScenery
{
    public class GameOverController_Tests
    {
        // Test Settings

        const string ADDRESS = "GameOverController";
        const float DELAY = 3f;

        WaitForSeconds delay;
        GameOverController controller;
        bool _setup;

        bool _playClicked;

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

            controller = handle.Result.GetComponent<GameOverController>();

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

            Assert.That(controller.transform.childCount > 0);
            Assert.IsNotNull(canvas);
            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

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
        public IEnumerator Step_3_Click_Play()
        {
            var button = controller.gameObject.GetComponentInChildren<Button>(true);
            button.onClick.Invoke(); // simulate button click

            yield return null;

            // canvas should be hidden after event is consumed
            var canvas = controller.gameObject.GetComponentInChildren<Canvas>(true);
            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            Debug.Log("Test: play clicked passed");
            yield return delay;
        }

        [UnityTest, Order(4)]
        public IEnumerator Step_4_Hide()
        {
            controller.Show();
            yield return null;
            controller.Hide();

            var canvas = controller.gameObject.GetComponentInChildren<Canvas>(true);
            Assert.IsFalse(canvas.gameObject.activeInHierarchy);

            Debug.Log("Test: controller hide passed");
            yield return delay;
        }

        [UnityTest, Order(5)]
        public IEnumerator Step_5_Unload()
        {
            controller.Unload();
            yield return delay;
            Assert.That(controller.transform.childCount == 0);
            Debug.Log("Test: controller unload passed");
        }

    }

}




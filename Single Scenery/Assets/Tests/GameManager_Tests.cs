
using NUnit.Framework;
using UnityEngine;

namespace SingleScenery
{
    public class GameManager_Tests
    {

        [Test]
        public void Can_Create_GameManager()
        {
            var go = new GameObject("Game");
            var gm = go.AddComponent<GameManager>();

            Assert.IsInstanceOf<GameManager>(gm);

        }

    }
}

using UnityEngine;
using UnityEngine.UI;

namespace SingleScenery
{
    public class UIEventButton : MonoBehaviour
    {
        [SerializeField] private GameEvent onClickEvent;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            onClickEvent.Invoke();
        }
    }
}

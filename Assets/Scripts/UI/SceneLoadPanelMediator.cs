using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SceneLoadPanelMediator : MonoBehaviour
    {
        [SerializeField]
        private Dropdown _selector;

        [SerializeField]
        private Button _loadButton;

        public void Start()
        {
            _loadButton.onClick.AddListener(HandleLoadClick);
        }

        public void OnDestroy()
        {
            _loadButton.onClick.RemoveAllListeners();
        }

        private void HandleLoadClick()
        {
            var option = _selector.options[_selector.value];
            var scene = option.text;
            SceneManager.LoadSceneAsync(scene);
        }
    }
}

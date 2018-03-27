using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class AimBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _pointer;

        private RectTransform _pointerTransform;

        private GameObject _aim;
        private RectTransform _aimTransform;

        private bool _active;

        public void Start()
        {
            _aim = gameObject;

            _aimTransform = _aim.GetComponent<RectTransform>();
            _pointerTransform = _pointer.GetComponent<RectTransform>();
        }

        public void Update()
        {
            if (_active)
            {
                _pointerTransform.position = Input.mousePosition;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _active = true;
            Debug.Log("Enter");
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            _active = false;

            Debug.Log("Exit");
        }

        
    }
}

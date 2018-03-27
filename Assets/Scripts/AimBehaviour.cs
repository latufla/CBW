using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class AimBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public UnityAction<Vector2> OnClick;

        [SerializeField]
        private GameObject _pointer;

        private RectTransform _pointerTransform;

        private RectTransform _aimTransform;

        private bool _active;

        public void Start()
        {
            _aimTransform = gameObject.GetComponent<RectTransform>();
            _pointerTransform = _pointer.GetComponent<RectTransform>();
        }

        public void Update()
        {
            if (_active)
            {
                var locPos = new Vector2();
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_aimTransform, Input.mousePosition,
                    null, out locPos);

                var radius = _aimTransform.sizeDelta.x / 2;
                var distance = new Vector2(locPos.x, locPos.y).magnitude;

                if(distance <= radius)
                    _pointerTransform.localPosition = locPos;
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
            {
                var radius = _aimTransform.sizeDelta.x / 2;
                var point = _pointerTransform.localPosition / radius;
                OnClick(point);
            }
        }        
    }
}

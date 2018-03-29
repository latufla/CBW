using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class AimBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _pointer;

        private RectTransform _pointerTransform;

        private RectTransform _aimTransform;

        private Text _positionText;

        private bool _active;

        public void Start()
        {
            _aimTransform = gameObject.GetComponent<RectTransform>();
            _pointerTransform = _pointer.GetComponent<RectTransform>();
            _positionText = gameObject.GetComponentInChildren<Text>();
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

                locPos.x = Mathf.Round(locPos.x * 10) / 10;
                locPos.y = Mathf.Round(locPos.y * 10) / 10;

                if (distance <= radius)
                {
                    _pointerTransform.localPosition = locPos;
                    _positionText.text = CalcPoint().ToString();
                }
                    
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _active = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _active = false;
        }

        public Vector2 CalcPoint()
        {
            var radius = _aimTransform.sizeDelta.x / 2;
            var pos = _pointerTransform.localPosition;
            return new Vector2(pos.x, pos.y) / radius;
        }       
    }
}

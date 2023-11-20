using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Transform _sprite;
    [SerializeField] private Shadow _shadow;

    private Vector2 _initialPosition;
    private Vector2 _shiftedPosition;

    private void Awake()
    {
        _initialPosition = _sprite.localPosition;
        _shiftedPosition = _initialPosition + _shadow.effectDistance / 2f;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        _sprite.localPosition = _shiftedPosition;
        _shadow.enabled = false;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _sprite.localPosition = _initialPosition;
        _shadow.enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundPlayer.Instance?.PlayButtonClickSound();
    }
}

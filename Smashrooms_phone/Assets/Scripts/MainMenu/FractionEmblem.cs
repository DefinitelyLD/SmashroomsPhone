using UnityEngine.EventSystems;
using UnityEngine;

public class FractionEmblem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Start() => LeanTween.moveLocalY(gameObject, 140, 0.01f);
    public void OnPointerEnter(PointerEventData eventData) => LeanTween.moveLocalY(gameObject, -100, 0.2f);
    public void OnPointerExit(PointerEventData eventData) => LeanTween.moveLocalY(gameObject, 140, 0.2f);
}

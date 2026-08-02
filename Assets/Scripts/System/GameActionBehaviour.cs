using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameDebug
{
    /// <summary>
    /// <para>UIの操作をGamePhaseに通知する</para>
    /// </summary>
    public class GameActionBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void Invoke()
        {
            GameManager.INSTANCE.Invoke(this.gameObject);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            GameManager.INSTANCE.Invoke(this.gameObject, "Pointer Enter", pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            GameManager.INSTANCE.Invoke(this.gameObject, "Pointer Exit", pointerEventData);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace ETModel
{
    public class SettingCtrPos : MonoBehaviour, IDragHandler
    {
        public RectTransform joyStick;
        private void Awake()
        {
            hhalfscreenW = Screen.width >> 2;
            hhalfscreenH = Screen.height >> 2;
        }
        private float hhalfscreenW;
        private float hhalfscreenH;
        public void ConfirmPos()
        {
            PlayerPrefs.SetFloat("JoyX", joyStick.position.x);
            PlayerPrefs.SetFloat("JoyY", joyStick.position.y);
        }
        /* private void FixedUpdate()
         {
             if (Input.GetMouseButtonDown(0)) {
                 if(Input.mousePosition.x< halfscreenW&& Input.mousePosition.y < halfscreenH)
                     joyStick.anchoredPosition = Input.mousePosition;
             }
         }*/

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(joyStick, eventData.position, eventData.pressEventCamera, out Vector3 pos))
            {
                joyStick.position = new Vector3(Mathf.Clamp(pos.x - joyStick.rect.width / 2, -hhalfscreenW, hhalfscreenW), Mathf.Clamp(pos.y - joyStick.rect.height / 2, -hhalfscreenH, hhalfscreenH), 0);
            }
        }
    }
}
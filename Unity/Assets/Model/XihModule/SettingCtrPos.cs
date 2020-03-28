using UnityEngine;
using UnityEngine.EventSystems;

namespace ETModel
{
    public class SettingCtrPos : MonoBehaviour, IDragHandler
    {
        public VariableJoystick variableJoystick;
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
        public void SetFourDir(bool value)
        {
            PlayerPrefs.SetInt("IsFourDir", value ? 1 : 0);
            variableJoystick.IsFourDir = value;
        }
        public void ModeChanged(int index)
        {
            switch (index)
            {
                case 0:
                    variableJoystick.SetMode(JoystickType.Fixed);
                    break;
                case 1:
                    variableJoystick.SetMode(JoystickType.Floating);
                    break;
                case 2:
                    variableJoystick.SetMode(JoystickType.Dynamic);
                    break;
                default:
                    break;
            }
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(joyStick, eventData.position, eventData.pressEventCamera, out Vector3 pos))
            {
                joyStick.position = new Vector3(Mathf.Clamp(pos.x - joyStick.rect.width / 2, -hhalfscreenW, hhalfscreenW), Mathf.Clamp(pos.y - joyStick.rect.height / 2, -hhalfscreenH, hhalfscreenH), 0);
            }
        }
    }
}
using Cinemachine;
using UnityEngine;

namespace ETModel {
    /// <summary>
    /// 需要放在Hotfix层，改为数据驱动或者CLR绑定
    /// </summary>
    public class PlayerControl : MonoBehaviour
    {
        public float speed = 5.0f;
        private float qurtSpeed;
        private VariableJoystick joystick;
        private Rigidbody rb;
        private Animator animator;
        public GameObject body;
        //private CinemachineVirtualCamera CMvcam;
        private CinemachineTargetGroup targetGroup;
        private void Awake()
        {
            //tag = "Untagged";
            joystick = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<VariableJoystick>();
            //CMvcam = GameObject.FindGameObjectWithTag("CMVcam").GetComponent<CinemachineVirtualCamera>();
            targetGroup = GameObject.FindGameObjectWithTag("CMVcam").GetComponent<CinemachineTargetGroup>();
            SetTargets(transform);
            //CMvcam.Follow = transform;
            rb = GetComponent<Rigidbody>();
            animator = GetComponentInChildren<Animator>();
        }
        public void SetNewSpeed(float speed)
        {
            this.speed = speed;
            this.qurtSpeed = Mathf.Sqrt(speed);
            Debug.Log("sq:" + qurtSpeed);
        }
        public void SetTargets(params Transform[] targets)
        {
            if (targets.Length <= 0)
                return;
            CinemachineTargetGroup.Target[] cmTargets = new CinemachineTargetGroup.Target[targets.Length];
            for (int i = 0; i < targets.Length; i++)
                cmTargets[i] = new CinemachineTargetGroup.Target() { target = targets[i], weight = 1f, radius = 10f };
            targetGroup.m_Targets = cmTargets;
        }
        Vector3 direction;
        bool hor;//左右移动了
        bool ver;//上下移动了
        float horVal;
        float VerVal;
        const float fixedVal = 0.25f;
        const float bfixedVal = -0.25f;
        private void FixedUpdate()
        {
            horVal = joystick.Horizontal;
            VerVal = joystick.Vertical;
            hor = (horVal > fixedVal || horVal < bfixedVal);
            ver = (VerVal > fixedVal || VerVal < bfixedVal);
            if (!ver && !hor)
            {
                animator.SetFloat("Speed", 0);
                return;
            }
            else if (ver && !hor)
            {
                body.transform.eulerAngles = VerVal > 0 ? Vector3.up * 180 : Vector3.zero;
                transform.position += (VerVal > 0 ? Vector3.forward : Vector3.back) * speed * Time.fixedDeltaTime;
            }
            else if (!ver && hor)
            {
                body.transform.eulerAngles = (horVal > 0 ? Vector3.down : Vector3.up) * 90;
                transform.position += (horVal > 0 ? Vector3.right : Vector3.left) * speed * Time.fixedDeltaTime;
            }
            else
            {
                body.transform.eulerAngles = (VerVal > 0 ? (horVal > 0 ? Vector3.down : Vector3.up) * 135 : (horVal > 0 ? Vector3.down : Vector3.up) * 45);
                transform.position -= body.transform.forward * speed * Time.fixedDeltaTime;
            }
            animator.SetFloat("Speed", speed);
            //Debug.Log("transform.forward :" + transform.forward);
        }
    }
}

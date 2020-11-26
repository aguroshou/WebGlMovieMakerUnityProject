using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using System.Globalization;

namespace KiliWare.SampleVRMApp
{
    public class TransformController : MonoBehaviour
    {
        private int openAnimationKey = Animator.StringToHash("Entry");

        static int VrmControllingObjectNumber;
        [SerializeField] int vrmObjectNumber;
        private Animation animation;

        private Animator animator;

        //[SerializeField] AnimatorStateInfo animatorStateInfo;
        [SerializeField] private float animationSpeed = 0.004f;
        [SerializeField] private float animationSpeedRate = 1.0f;
        [SerializeField] private GameObject animationSpeedRateTextGameObject;
        [SerializeField] private TMP_Text animationSpeedRateText;

        private GameObject animatorControllerDropdownGameObject;
        private TMP_Dropdown animatorControllerDropdown;
        private int previousAnimatorControllerDrowdownValue;

        protected float _rotationSpeed = 3;
        protected float _moveSpeed = 3;

        private void Start()
        {
            VrmControllingObjectNumber++;
            vrmObjectNumber = VrmControllingObjectNumber;
            animation = GetComponent<Animation>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController =
                (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    Resources.Load("AnimatorControllers/MixamoAnimatorController"));
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
            animation.playAutomatically = true;
            transform.Rotate(0f, 180f, 0f);
            animatorControllerDropdownGameObject = GameObject.Find("AnimatorControllerDropdown");
            animatorControllerDropdown = animatorControllerDropdownGameObject.GetComponent<TMP_Dropdown>();
            previousAnimatorControllerDrowdownValue = animatorControllerDropdown.value;
            ChangedAnimatorControllerDropdownValue();
            animationSpeedRateTextGameObject = GameObject.Find("AnimationSpeedRateText");
            animationSpeedRateText = animationSpeedRateTextGameObject.GetComponent<TextMeshProUGUI>();

            //ヨーロッパ圏での小数点入力の際に必要であるようです。
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("en-us");
        }

        public void SetAnimationFrame(int i_frame)
        {
            var clipInfoList = animator.GetCurrentAnimatorClipInfo(0);
            var clip = clipInfoList[0].clip;

            float time = (float) i_frame / clip.frameRate;

            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            var animationHash = stateInfo.shortNameHash;

            animator.Play(animationHash, 0, time);
        }

        private float currentAnimationTime = 0;

        void Update()
        {
            currentAnimationTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            currentAnimationTime += animationSpeed * animationSpeedRate;
            animator.Play(openAnimationKey, 0, currentAnimationTime);

            if (VrmControllingObjectNumber != vrmObjectNumber)
            {
                return;
            }

            string tmpAnimationSpeedRateString = animationSpeedRateText.text;
            
            //tmpAnimationSpeedRateStringの終端文字にゴミが入っているので取り除いています
            tmpAnimationSpeedRateString =
                tmpAnimationSpeedRateString.Substring(0, tmpAnimationSpeedRateString.Length - 1);
            if (float.TryParse(tmpAnimationSpeedRateString, out animationSpeedRate))
            {
                Mathf.Clamp(animationSpeedRate, -10f, 10f);
            }
            else
            {
                animationSpeedRate = 1.0f;
            }

            if (previousAnimatorControllerDrowdownValue != animatorControllerDropdown.value)
            {
                previousAnimatorControllerDrowdownValue = animatorControllerDropdown.value;
                ChangedAnimatorControllerDropdownValue();
            }

            if (Input.GetKey(KeyCode.U))
            {
                transform.Rotate(new Vector3(0f, 2f, 0f));
            }

            if (Input.GetKey(KeyCode.O))
            {
                transform.Rotate(new Vector3(0f, -2f, 0f));
            }

            if (Input.GetKey(KeyCode.I))
            {
                transform.Rotate(new Vector3(2f, 0f, 0f));
            }

            if (Input.GetKey(KeyCode.K))
            {
                transform.Rotate(new Vector3(-2f, 0f, 0f));
            }

            if (Input.GetKey(KeyCode.J))
            {
                transform.Rotate(new Vector3(0f, 0f, -2f));
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.Rotate(new Vector3(0f, 0f, 2f));
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-0.01f, 0f, 0f), Space.World);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(0.01f, 0f, 0f), Space.World);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0f, 0.01f, 0f), Space.World);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0f, -0.01f, 0f), Space.World);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(0f, 0, -0.01f), Space.World);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(new Vector3(0f, 0, 0.01f), Space.World);
            }
        }

        private void ChangedAnimatorControllerDropdownValue()
        {
            switch (animatorControllerDropdown.value)
            {
                case 0:
                    openAnimationKey = Animator.StringToHash("T-Pose");
                    //animator.runtimeAnimatorController =  null;
                    break;
                case 1:
                    openAnimationKey = Animator.StringToHash("Bboy Hip Hop Move");
                    //animator.runtimeAnimatorController =  (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load ("AnimatorControllers/AnimatorControllerArmStretching"));
                    break;
                case 2:
                    openAnimationKey = Animator.StringToHash("Bicycle Crunch");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerCards"));
                    break;
                case 3:
                    openAnimationKey = Animator.StringToHash("Boxing");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerDancing"));
                    break;
                case 4:
                    openAnimationKey = Animator.StringToHash("Cheering");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerHapplyIdle"));
                    break;
                case 5:
                    openAnimationKey = Animator.StringToHash("Clapping");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerOrcIdle"));
                    break;
                case 6:
                    openAnimationKey = Animator.StringToHash("Cross Jumps");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerSitting"));
                    break;
                case 7:
                    openAnimationKey = Animator.StringToHash("Female Walk");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerSittingClap"));
                    break;
                case 8:
                    openAnimationKey = Animator.StringToHash("Flair");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerSittingThumbsUp"));
                    break;
                case 9:
                    openAnimationKey = Animator.StringToHash("Floating");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerSittingYell"));
                    break;
                case 10:
                    openAnimationKey = Animator.StringToHash("Hanging Idle");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerStandingWBriefcaseIdle"));
                    break;
                case 11:
                    openAnimationKey = Animator.StringToHash("Hip Hop Dancing");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerTaunt"));
                    break;
                case 12:
                    openAnimationKey = Animator.StringToHash("Jogging");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerTextingWhileStanding"));
                    break;
                case 13:
                    openAnimationKey = Animator.StringToHash("Sword And Shield Power Up");
                    //openAnimationKey = Animator.StringToHash("");
                    // animator.runtimeAnimatorController =
                    //     (RuntimeAnimatorController) RuntimeAnimatorController.Instantiate(
                    //         Resources.Load("AnimatorControllers/AnimatorControllerThankful"));
                    break;
                case 14:
                    openAnimationKey = Animator.StringToHash("Jump");
                    break;
                case 15:
                    openAnimationKey = Animator.StringToHash("Jumping Jacks");
                    break;
                case 16:
                    openAnimationKey = Animator.StringToHash("Martelo 2");
                    break;
                case 17:
                    openAnimationKey = Animator.StringToHash("Sad Walk");
                    break;
                case 18:
                    openAnimationKey = Animator.StringToHash("Situps");
                    break;
                case 19:
                    openAnimationKey = Animator.StringToHash("Skinning Test");
                    break;
                case 20:
                    openAnimationKey = Animator.StringToHash("Spin In Place");
                    break;
                case 21:
                    openAnimationKey = Animator.StringToHash("Standing Greeting");
                    break;
                case 22:
                    openAnimationKey = Animator.StringToHash("Strong Gesture");
                    break;
                case 23:
                    openAnimationKey = Animator.StringToHash("Swimming");
                    break;
                default:
                    break;
            }
        }

        public void SetSpeed(float rotation, float move)
        {
            _rotationSpeed = rotation;
            _moveSpeed = move;
        }

        // From https://teratail.com/questions/147069#reply-221677
        protected void Rotate(Vector2 delta)
        {
            float deltaAngle = delta.magnitude * _rotationSpeed;

            if (Mathf.Approximately(deltaAngle, 0.0f))
            {
                return;
            }

            var cameraTransform = Camera.main.transform;
            var deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;
            var axisWorld = Vector3.Cross(deltaWorld, cameraTransform.forward).normalized;

            transform.Rotate(axisWorld, deltaAngle, Space.World);
        }

        protected void Move(Vector2 delta)
        {
            var cameraTransform = Camera.main.transform;
            var deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;

            transform.Translate(deltaWorld * _moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
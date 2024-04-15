using UnityEngine;
using UnityEngine.EventSystems;


public class ThirdPersonInput : MonoSingleton<ThirdPersonInput>, ICur
{
    #region Variables       

    [Header("Controller Input")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public KeyCode jumpInput = KeyCode.Space;
    public KeyCode strafeInput = KeyCode.Tab;
    public KeyCode sprintInput = KeyCode.LeftShift;

    [Header("Camera Input")]
    public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";

    [HideInInspector] public ThirdPersonController cc;
    [HideInInspector] public ThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;

    #endregion

    public bool Enable = true;

    public void SetEnable(bool t)
    {
        Enable = t;
        if (!t)
            CurControl.ReleaseCur(this);
        else
            CurControl.LockCur(this);
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InitilizeController();
        InitializeTpCamera();
        SetEnable(true);
    }
    void OnDisable()
    {
        SetEnable(false);
    }
    /*public override void OnNetworkSpawn()
    { 
        InitilizeController();
        InitializeTpCamera();
    }*/

    protected virtual void FixedUpdate()
    {
        if (!cc)
            return;
        cc.UpdateMotor();               // updates the ThirdPersonMotor methods
        cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
        cc.ControlRotationType();       // handle the controller rotation type
    }

    protected virtual void Update()
    {
        if (!cc)
            return;
        InputHandle();                  // update the input methods
        cc.UpdateAnimator();            // updates the Animator Parameters
    }

    public virtual void OnAnimatorMove()
    {
        if (!cc)
            return;
        cc.ControlAnimatorRootMotion(); // handle root motion animations 
    }

    #region Basic Locomotion Inputs

    protected virtual void InitilizeController()
    {

        cc = GetComponent<ThirdPersonController>();

        if (cc != null)
            cc.Init();
    }

    protected virtual void InitializeTpCamera()
    {

        if (tpCamera == null)
        {
            tpCamera = FindAnyObjectByType<ThirdPersonCamera>();
            if (tpCamera == null)
                return;
            if (tpCamera)
            {
                tpCamera.SetMainTarget(this.transform);
                tpCamera.Init();
            }
        }
    }

    protected virtual void InputHandle()
    {
        MoveInput();
        CameraInput();
        SprintInput();
        JumpInput();
    }


    public virtual void MoveInput()
    {
        cc.input.x = Enable ? Input.GetAxis(horizontalInput) : 0;
        cc.input.z = Enable ? Input.GetAxis(verticallInput) : 0;
    }

    protected virtual void CameraInput()
    {

        if (!cameraMain)
        {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            else
            {
                cameraMain = Camera.main;
                cc.rotateTarget = cameraMain.transform;
            }
        }

        if (cameraMain)
        {
            cc.UpdateMoveDirection(cameraMain.transform);
        }

        if (tpCamera == null)
            return;

        var Y = Enable ? Input.GetAxis(rotateCameraYInput) : 0;
        var X = Enable ? Input.GetAxis(rotateCameraXInput) : 0;

        tpCamera.RotateCamera(X, Y);
    }


    protected virtual void SprintInput()
    {

        if (Enable && Input.GetKeyDown(sprintInput))
            cc.Sprint(true);
        else if (!Enable || Input.GetKeyUp(sprintInput))
            cc.Sprint(false);
    }

    /// <summary>
    /// Conditions to trigger the Jump animation & behavior
    /// </summary>
    /// <returns></returns>
    protected virtual bool JumpConditions()
    {
        return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
    }

    /// <summary>
    /// Input to trigger the Jump 
    /// </summary>
    protected virtual void JumpInput()
    {
        if (Enable && Input.GetKeyDown(jumpInput) && JumpConditions())
            cc.Jump();
    }

    #endregion
}

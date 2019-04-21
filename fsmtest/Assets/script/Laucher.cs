using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Laucher : MonoBehaviour {

    public static Laucher instance;
    public ActorMainPlayer MainPlayer;

    public Transform joysticktran;
    private EJoystick mNGUIJoystick;

    public Transform uiboad;

    public  List<Actor> AllActors = new List<Actor>();

    public Camera MainCamera { get; set; }
    public Camera NGUICamera;

    void Awake()
    {
        instance = this;

    }

    // Use this for initialization
    void Start () {
        ZTConfig.Instance.Init();
        ZTInput.Instance.SetDontDestroyOnLoad(transform);
       

        CreateMainCamera();

        ActorMainPlayer pActor = (ActorMainPlayer)AddActor(1, EActorType.PLAYER, EBattleCamp.A, XTransform.Create(this.transform.position, this.transform.eulerAngles), true);
        MainPlayer = pActor;

        Actor actor = (ActorPlayer)AddActor(50006, EActorType.MONSTER, EBattleCamp.B, XTransform.Create(this.transform.position, this.transform.eulerAngles), false);

        mNGUIJoystick = joysticktran.GetComponent<EJoystick>();
        mNGUIJoystick.On_JoystickMove += OnJoystickMove;
        mNGUIJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;


        NGUICamera.clearFlags = CameraClearFlags.Depth;
        NGUICamera.depth = Define.DEPTH_CAM_2DUICAMERA;
        NGUICamera.nearClipPlane = -10;
        NGUICamera.farClipPlane = 1200;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void FixedUpdate()
    {
        BTTreeManager.Instance.Step();
        ZTTimer.Instance.Step();
        ZTAction.Update();
        ZTEffect.Instance.Step();

        if (Input.GetKeyDown(KeyCode.S))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_0);
        }
    }

    public void CreateMainCamera()
    {
        MainCamera = Camera.main;
        if (MainCamera == null)
        {
            GameObject c = new GameObject("MainCamera");
            MainCamera = c.AddComponent<Camera>();
            GTTools.SetTag(MainCamera.gameObject, GTTools.Tags.MainCamera);
            MainCamera.gameObject.GET<AudioListener>();
        }
        MainCamera.transform.parent = transform;
        MainCamera.gameObject.AddComponent<CameraFollow>();
    }

    public Actor AddActor(int id, EActorType type, EBattleCamp camp, XTransform param, bool isMainPlayer = false)
    {
        Actor pActor = null;
        if (isMainPlayer)
        {
             pActor = new ActorMainPlayer(id, 100, EActorType.PLAYER, camp);
            pActor.Load(param);

            object[] args = new object[] { pActor.CacheTransform.transform };
            CameraEffectBase effect = MainCamera.GetComponent<CameraFollow>();
            effect.Init(0, MainCamera, null, args);
        }
        else {
             pActor = new ActorPlayer(id, 100, type, camp);
             pActor.Load(param);
        }

        if (pActor.CacheTransform != null)
        {
            Debug.LogError("添加人物" + pActor.ActorType);
            //pActor.CacheTransform.parent = GetHolder(EMapHolder.Role).transform;
            AllActors.Add(pActor);
            //LevelData.CampActors[camp].Add(pActor);
        }
        return pActor;
    }

    public void OnClickSkill()
    {
        ESkillPos skillPos = ESkillPos.Skill_0;
        ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, skillPos);
    }

    private void OnJoystickMove(EJoystick move)
    {
        float x = move.joystickAxis.x;
        float y = move.joystickAxis.y;
        if (Math.Abs(x) > 0.1f || Math.Abs(y) > 0.1f)
        {
            ZTEvent.FireEvent(EventID.MOVING_JOYSTICK, x, y);
        }
    }

    private void OnJoystickMoveEnd(EJoystick move)
    {
        ZTEvent.FireEvent(EventID.ENDING_JOYSTICK);
    }
}

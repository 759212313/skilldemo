using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ZTInput : MonoSingleton<ZTInput>
{
    //private ETouch mTouch = new ETouch();
    public override void SetDontDestroyOnLoad(Transform parent)
    {
        base.SetDontDestroyOnLoad(parent);
        //mTouch.Init(transform);
        ZTEvent.AddHandler<float, float>(EventID.MOVING_JOYSTICK, OnMoveJoystick);
        ZTEvent.AddHandler(EventID.ENDING_JOYSTICK, OnEndJoystick);
        ZTEvent.AddHandler<ESkillPos>(EventID.REQ_CAST_SKILL, OnCastSkill);
        ZTEvent.AddHandler(EventID.REQ_PLAYER_JUMP, OnJump);
    }

    public void OnJump()
    {
        //if (!IsExistsMe())
        //{
        //    return;
        //}
        //LevelData.MainPlayer.Command(new JPCommand());
    }

    public void OnCastSkill(ESkillPos arg1)
    {
        //if (!IsExistsMe())
        //{
        //    return;
        //}
        ECommandReply reply = Laucher.instance.MainPlayer.Command(new USCommand(arg1));
        if (reply == ECommandReply.Y)
        {
            ZTEvent.FireEvent(EventID.ACK_CAST_SKILL, arg1);
        }
    }

    public void OnEndJoystick()
    {
        //if (!IsExistsMe())
        //{
        //    return;
        //}
        Laucher.instance.MainPlayer.Command(new IDCommand());
    }

    public void OnMoveJoystick(float arg1, float arg2)
    {
        //if (!IsExistsMe())
        //{
        //    return;
        //}

        Vector2 delta = new Vector2(arg1, arg2);
        Laucher.instance.MainPlayer.Command(new MVCommand(delta));
    }
     
    /*void Update()
    {
        OnEscape();
        OnMouseButton();
        OnKeyDown();
        mTouch.Update();
    }

    private void OnEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Application.isPlaying==false)
        {
            ZTUIManager.Instance.Clear();
        }
    }

    private void OnMouseButton()
    {
        if (!IsExistsMe())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnMouse0();
        }
    }

    private void OnMouse0()
    {
        RaycastHit hit;
        Camera cam = ZTCamera.Instance.MainCamera;
        Ray ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Physics.Raycast(ray, out hit, 1000))
        {
            switch(hit.transform.gameObject.layer)
            {
                case Define.LAYER_NPC:
                    if (GTTools.GetHorizontalDistance(hit.transform.root.position, LevelData.MainPlayer.Pos) > 2)
                    {
                        if (LevelData.MainPlayer.GetActorPathFinding().CanReachPosition(hit.point) == false)
                        {
                            LevelData.MainPlayer.Command(new RTCommand(hit.transform.root.position));
                        }
                    }
                    break;
                case Define.LAYER_MINE:
                    {

                    }
                    break;
            }
        }
    }

    public bool IsExistsMe()
    {
        if (LevelData.MainPlayer == null)
        {
            return false;
        }
        if (LevelData.MainPlayer.Obj == null)
        {
            return false;
        }
        return true;
    }

    private void OnKeyDown()
    {
        if (!IsExistsMe())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_0);       
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ZTEvent.FireEvent(EventID.REQ_CAST_SKILL, ESkillPos.Skill_9);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ZTEvent.FireEvent(EventID.RECV_PLAYER_START_MOUNT);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ZTEvent.FireEvent(EventID.RECV_PLAYER_END_MOUNT);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //LevelData.MainPlayer.BeDamage(null,105);
            ZTCamera.Instance.SwitchCameraEffect(ECameraType.SHAKE, ZTCamera.Instance.MainCamera,null);
        }
    }

    void OnDestroy()
    {
        ZTEvent.RemoveHandler<float, float>(EventID.MOVING_JOYSTICK, OnMoveJoystick);
        ZTEvent.RemoveHandler(EventID.ENDING_JOYSTICK, OnEndJoystick);
        ZTEvent.RemoveHandler<ESkillPos>(EventID.REQ_CAST_SKILL, OnCastSkill);
        ZTEvent.RemoveHandler(EventID.REQ_PLAYER_JUMP, OnJump);
    }*/
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EFlyWordType
{
    TYPE_AVATAR_HURT  =1,
    TYPE_AVATAR_CRIT  =2,
    TYPE_AVATAR_HEAL  =3,
    TYPE_ENEMY_HURT   =4,
    TYPE_ENEMY_CRIT   =5,
    TYPE_PARTNER_HURT =6,
    TYPE_PARTNER_CRIT =7,
}

public class ZTFlyword : MonoSingleton<ZTFlyword>
{
    private Queue<FlywordData> mQueue = new Queue<FlywordData>();
    private int timer;
    private int interval = 2;

    void Awake()
    {
        timer = interval;
    }

    void Update()
    {
        if (mQueue.Count == 0)
        {
            return;
        }
        if (timer > interval)
        {
            FlywordData item = mQueue.Dequeue();
            Play(item.value, item.pos, item.type);
            timer = 0;
        }
        else
        {
            timer++;
        }
    }

    void Play(string value, Vector3 pos, EFlyWordType type)
    {
        string path = Define.FLYWORDPATHS[type];
        //Debug.LogError(path);
        GameObject go = ZTPool.Instance.GetGo(path);
        UIFlyword flyword = go.GET<UIFlyword>();
        //BaseWindow window = ZTUIManager.Instance.GetWindow<UIFlutter>(WindowID.UI_FLUTTER);
        //if(window.IsVisable()==false)
        //{
        //    window = ZTUIManager.Instance.OpenWindow(WindowID.UI_FLUTTER);
        //}
        //if (window.CacheTransform==null)
        //    return;
        flyword.gameObject.layer = Define.LAYER_UI;
        //flyword.transform.SetParent(window.CacheTransform);
        flyword.transform.SetParent(Laucher.instance.uiboad);
        Vector3 pos_3d = pos;
        //Vector3 pos_3d = GameObject.Find("50006(Clone)").transform.position;
        Vector2 pos_screen = Laucher.instance.MainCamera.WorldToScreenPoint(pos_3d);
        Vector3 pos_ui = Laucher.instance.NGUICamera.ScreenToWorldPoint(pos_screen);
        //Debug.LogError("====11===" + pos_3d.x + " " + pos_3d.y + " " + pos_3d.z+" "+"====22===" + pos_ui.x + " " + pos_ui.y + " " + pos_ui.z);

        pos_ui.z = 0;
        flyword.flywordType = type;
        flyword.path = path;
        switch (type)
        {
            case EFlyWordType.TYPE_ENEMY_HURT:
                flyword.SetColor(Color.red);
                flyword.Show("-" + value);
                break;
            case EFlyWordType.TYPE_ENEMY_CRIT:
                flyword.SetColor(Color.red);
                flyword.Show("-" + value);
                break;
            case EFlyWordType.TYPE_AVATAR_CRIT:
                flyword.SetColor(new Color(1, 0.18f, 0), new Color(1, 0.78f, 0));
                flyword.Show("c" + value);
                break;
            case EFlyWordType.TYPE_AVATAR_HURT:
                flyword.SetColor(new Color(1, 0.18f, 0), new Color(1, 0.78f, 0));
                flyword.Show(value);
                break;
            case EFlyWordType.TYPE_AVATAR_HEAL:
                flyword.SetColor(Color.green);
                flyword.Show("+" + value);
                break;
            case EFlyWordType.TYPE_PARTNER_HURT:
            case EFlyWordType.TYPE_PARTNER_CRIT:
                flyword.SetColor(Color.white);
                flyword.Show(value);
                break;
        }
        //flyword.transform.position = pos_ui;
        //flyword.transform.localScale = new Vector3(20, 20, 20);

        flyword.Init(pos_ui, 25);
    }

    public void Show(string value,Vector3 pos,EFlyWordType type)
    {
        FlywordData data = new FlywordData(value, pos, type);
        mQueue.Enqueue(data);      
    }

    public void Clear()
    {
        mQueue.Clear();
    }

    public class FlywordData
    {
        public string value;
        public Vector3 pos;
        public EFlyWordType type;

        public FlywordData(string value, Vector3 pos, EFlyWordType type)
        {
            this.value = value;
            this.pos = pos;
            this.type = type;
        }
    }
}

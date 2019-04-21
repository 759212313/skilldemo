using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using MapDirector;

public enum EBoardType
{
    TYPE_PLAYER,
    TYPE_NPC,
    TYPE_MONSTER,
    TYPE_PORTAL
}

public class ZTBoard : MonoSingleton<ZTBoard>
{
    private Dictionary<IObj, BoardBase> mBoards = new Dictionary<IObj, BoardBase>();

    public void Create<T>(T owner, EBoardType type) where T : IObj
    {
        //if (owner == null || owner.CacheTransform == null)
        //{
        //    return;
        //}
        if (mBoards.ContainsKey(owner))
        {
            return;
        }
        BoardBase board = null;
        GameObject go = null;
        string path = null;
        Define.BOARDPATHS.TryGetValue(type, out path);
        Debug.LogError(path);
        if (string.IsNullOrEmpty(path))
        {
            return;
        }
        go = ZTPool.Instance.GetGo(path);
        switch (type)
        {
            case EBoardType.TYPE_PLAYER:
                if (owner is Actor)
                {
                    board = go.GET<BoardPlayer>();
                }
                break;
            case EBoardType.TYPE_NPC:
                if (owner is Actor)
                {
                    board = go.GET<BoardNpc>();
                }
                break;
            case EBoardType.TYPE_MONSTER:
                if (owner is Actor)
                {
                    board = go.GET<BoardMonster>();
                }
                break;
            case EBoardType.TYPE_PORTAL:
                //if (owner is LevelPortal)
                //{
                //    BoardPortal item = go.GET<BoardPortal>();
                //    board = item;
                //}
                break;
        }
        if (board != null)
        {
            //BaseWindow window = ZTUIManager.Instance.GetWindow<UIBoard>(WindowID.UI_BOARD);
            //if(window.IsVisable()==false)
            //{
            //    window = ZTUIManager.Instance.OpenWindow(WindowID.UI_BOARD);
            //}
            //if(window.CacheTransform!=null)
            //{
            //    board.transform.SetParent(window.CacheTransform);
            //    NGUITools.SetLayer(board.gameObject, window.CacheTransform.gameObject.layer);
            //}
            board.transform.SetParent(Laucher.instance.uiboad);
            board.transform.localScale = new Vector3(1, 1, 1);

            board.SetOwner(owner);
            board.UpdatePosition(owner.CacheTransform);
            board.Init();
            board.Refresh();
            board.Path = path;
            board.enabled = true;
            board.SetVisbale(false);
            board.SetVisbale(true);
            mBoards.Add(owner, board);
        }
    }

    public void Refresh<T>(T owner) where T : IObj
    {
        if (owner == null)
        {
            return;
        }
        BoardBase board = null;
        mBoards.TryGetValue(owner, out board);
        if(board!=null)
        {
            board.Refresh();
        }
    }

    public void Release<T>(T owner) where T : IObj
    {
        if (owner == null)
        {
            return;
        }
        BoardBase board = null;
        mBoards.TryGetValue(owner, out board);
        if (board != null)
        {
            board.Release();
        }
        mBoards.Remove(owner);
    }

    public void Clear()
    {
        foreach(var current in mBoards.ToArray())
        {
            Release(current.Key);
        }
    }
}

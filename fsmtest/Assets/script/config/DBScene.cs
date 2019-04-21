using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DBScene:DBModule
{
    public int Id;
    public string SceneName;
    public ESceneType SceneType;
    public string SceneMusic;
    public float  DelayTime;

    public override int GetTypeId()
    {
        return Id;
    }
}

public class ReadDBScene : IReadConfig<int, DBScene>
{
    protected override void LoadData(SQLiteQuery query, Dictionary<int, DBScene> dict)
    {
        DBScene db = new DBScene();
        db.Id = query.GetInt("Id");
        db.SceneName = query.GetString("SceneName");
        db.SceneType = (ESceneType)query.GetInt("SceneType");
        db.SceneMusic = query.GetString("SceneMusic");
        if (!dict.ContainsKey(db.Id))
        {
            dict.Add(db.Id, db);
        }
    }
}
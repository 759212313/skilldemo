using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Cfg.Act;

public class ActorAct
{
    public Actor          Owner      { get; private set; }
    public List<ActSkill> Skills     { get; private set; }
    public int            ComboIndex { get; private set; }

    public ActorAct(Actor owner)
    {
        Debug.LogError("=======1===========");
        this.Owner = owner;
        this.Skills = new List<ActSkill>();
        this.LoadXml();
        Debug.LogError("=======2===========");
    }

    public void UseSkill(ESkillPos pos)
    {
        ActSkill skill = GetSkill(pos);
        //ActManager.Instance.Run(skill);
    }

    public void UseSkillById(int id)
    {

    }

    public ActSkill GetSkill(int id)
    {
        return Skills.Find((data) => (data.Id == id));
    }

    public ActSkill GetSkill(ESkillPos pos)
    {
        return Skills.Find((data) => (data.Data.Pos == pos));
    }

    void LoadXml()
    {
        string pPath = string.Format("Text/Skill/{0}", 1);
        TextAsset asset = ZTResource.Instance.Load<TextAsset>(pPath);
        if (asset == null) return;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(asset.text);
        if (doc.FirstChild == null) return;
        XmlNode child = doc.FirstChild.FirstChild;
        while (child != null)
        {
            if(child.Name.Equals("Skill"))
            {
                ActSkillData data = new ActSkillData();
                data.Read(child as XmlElement);
                ActSkill skill = new ActSkill(data.Id, data);
                Skills.Add(skill);
            }
            child = child.NextSibling;
        }
        for (int i = 0; i < Skills.Count; i++)
        {
            Debug.LogError(Skills[i].Id);
        }
    }
}

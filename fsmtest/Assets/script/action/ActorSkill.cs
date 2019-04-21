using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.IO;

public class ActorSkillPool
{
    public List<SkillTree> Skills = new List<SkillTree>();
}

public class ActorSkill:IGame
{
    public Actor Owner = null;
    public ActorSkillPool Pool = null;
    private SkillTree mCurrSkillTree;
    private SkillTree mMinCastDistSkillTree;//最短攻击距离技能
    private int mComboIndex = 0;
    private List<SkillTree> mComboSkills = new List<SkillTree>();

    public ActorSkill(Actor owner)
    {
        this.Owner = owner;
        this.Pool = new ActorSkillPool();
        this.LoadSkill();

        float dist = 100000;
        for(int i=0;i< Pool.Skills.Count; i++)
        {
            SkillTree skill = Pool.Skills[i];
            if (skill.CastDistance<dist&&skill.CastDistance>0)
            {
                mMinCastDistSkillTree = skill;
                dist = skill.CastDistance;
            }

            if(skill.Pos==ESkillPos.Skill_0)
            {
                mComboSkills.Add(skill);
            }
        }
    }

    private void LoadSkill()
    {
        string pPath = string.Format("Text/Actor/{0}",Owner.Id);//50006
        TextAsset text = ZTResource.Instance.Load<TextAsset>(pPath);
        if (text==null)
        {
            return;
        }
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text.text);

        XmlNode root = doc.FirstChild;
        if(root.Name.Equals("Actor")== false)
        {
            return;
        }
        XmlNode child = root.FirstChild;
        while(child != null)
        {      
            if(child.Name.Equals("Skill"))
            {
                XmlElement xe = child as XmlElement;
                int id = EXml.ReadInt32(xe, "Id");
                if (id > 0)
                {
                    SkillTree skillTree = new SkillTree(id, Owner);
                    Pool.Skills.Add(skillTree);
                    skillTree.Load(xe);
                }
            }
            child = child.NextSibling;
        }
    }

    public bool UseSkill(int id)
    {
        SkillTree skillTree = GetSkill(id);
        if (skillTree == null)
        {
            return false;
        }
        mCurrSkillTree = skillTree;
        mCurrSkillTree.Start();
        return true;
    }

    public bool UseSkill(ESkillPos pos)
    {
        SkillTree skillTree = GetSkill(pos);
        if(skillTree==null)
        {
            return false;
        }
        mCurrSkillTree = skillTree;
        mCurrSkillTree.Start();
        if (pos == ESkillPos.Skill_0)
        {
            mComboIndex = mComboIndex >= mComboSkills.Count-1 ? 0 : ++mComboIndex;
        }
        return true;
    }

    public SkillTree GetSkill(ESkillPos pos)
    {
        for (int i = 0; i < Pool.Skills.Count; i++)
        {
            SkillTree skillTree = this.Pool.Skills[i];                     
            if (skillTree.Pos == pos)
            {
                if (pos == ESkillPos.Skill_0)
                {
                    return mComboSkills[mComboIndex];
                }
                return skillTree;
            }
        }
        return null;
    }

    public SkillTree GetSkill(int  id)
    {
        for (int i = 0; i < Pool.Skills.Count; i++)
        {
            SkillTree skillTree = this.Pool.Skills[i];
            if (skillTree.Id == id)
            {
                return skillTree;
            }
        }
        return null;
    }

    public float GetMinCastDistance()
    {
        return mMinCastDistSkillTree == null ? 0 : mMinCastDistSkillTree.CastDistance;
    }

    public SkillTree FindNextSkillByDist(float dist)
    {
        for (int i = 0; i < Pool.Skills.Count; i++)
        {
            SkillTree skillTree = this.Pool.Skills[i];
            if (skillTree.IsInCD())
            {
                continue;
            }
            if (skillTree.CastDistance <= 0)
            {
                return skillTree;
            }
            if (dist < skillTree.CastDistance)
            {
                return skillTree;
            }
        }
        return null;
    }

    //public SkillTree FindNextSkillByDist(Vector3 dest)
    //{
    //    float dist = GTTools.GetHorizontalDistance(dest, Owner.Pos);
    //    return FindNextSkillByDist(dist);
    //}

    public void Clear()
    {
        ClearCombo();
        if(mCurrSkillTree==null)
        {
            return;
        }
        mCurrSkillTree.Break();
        mCurrSkillTree = null;
    }

    public void ClearCombo()
    {
        mComboIndex = 0;
    }

    public void Start()
    {

    }

    public void Step()
    {

    }
}

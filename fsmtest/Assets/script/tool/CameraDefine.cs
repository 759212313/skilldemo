using UnityEngine;
using System.Collections;

public enum ECameraType
{
    NONE,
    FOLLOW,             //摄像机跟随
    SHAKE,              //摄像机震动
    SHELTER,            //场景灰暗，人物怪物高亮效果
    LIGHTING,           //闪电效果
    DRAG,               //拉效果
    DRAG_BACK,          //返回拉效果
    SPECIAL,            //BOSS镜头特写
    BULLET,             //子弹时间
}

public enum ECameraFollow
{
    DIRECT,//直接移动过去
    SMOOTH,//平滑移动过去，不旋转
    ROTATE,//平滑移动，带旋转
}

public enum ECameraState
{
    NONE,
    ENTER,
    UPDATE,
    LEAVE
}
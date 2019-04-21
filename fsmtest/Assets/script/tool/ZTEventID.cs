using UnityEngine;
using System.Collections;

public enum EventID : ushort
{
    RECV_CONNECT_FAIL,

    RECV_REGISTER,
    RECV_LOGIN,
    RECV_CREATE_NEWPLAYER,
    RECV_LOGINGAME,
    RECV_ENTERGAME,
    RECV_GET_SERVERS,
    RECV_CHECK_VERSION,


    UPDATE_CURRSELECT_SERVER,

    CHANGE_MONEY,
    CHANGE_ACTION,

    CHANGE_HEROLEVEL,//英雄升级
    CHANGE_HERONAME, //英雄改名
    CHANGE_HEROHEAD, //英雄更换图像
    CHANGE_HEROEXP,  //英雄增加经验

    CHANGE_FIGHTVALUE,
    CHANGE_ITEMPOS,
    UPDATE_ACTION_TIMER,
    RECV_SORT_BAG,
    RECV_USE_ITEM,//使用道具
    RECV_USE_BOX,
    RECV_DRESS_EQUIP,
    RECV_UNLOAD_EQUIP,
    RECV_DRESS_GEM,
    RECV_UNLOAD_GEM,
    RECV_COMPOSE_CHIP,
    RECV_STRENGTHEN_EQUIP,
    RECV_ADVANCE_EQUIP,
    RECV_UPSTAR_EQUIP,
    RECV_STRENGTHEN_GEM,

    RECV_PASS_COPY,
    RECV_RECEIVE_CHAPTERAWARD,
    RECV_BATTLE_CHECK,
    MOVING_JOYSTICK,
    ENDING_JOYSTICK,

    REQ_CAST_SKILL,    //玩家准备释放技能
    ACK_CAST_SKILL,    //玩家释放技能

    FINISH_LEVEL_LOAD,        //完成场景加载
    UPDATE_AVATAR_HEALTH,     //刷新主角生命
    UPDATE_AVATAR_POWER,      //刷新主角能量
    UPDATE_AVATAR_ATTR,       //刷新主角属性
    UPDATE_PARTNER_HEALTH,    //刷新伙伴生命

    RECV_KILL_MONSTER,        //杀死一只怪物

    RECV_PLAYER_START_MOUNT,  //骑坐骑
    RECV_PLAYER_END_MOUNT,    //卸载坐骑

    RECV_SETMOUNT,            //设置出战坐骑
    RECV_CHARGE_RELICS,       //神器充能
    RECV_UPGRADE_RELICS,      //神器升级
    RECV_BATTLE_RELICS,       //神器上阵
    RECV_UNLOAD_RELICS,       //神器卸下
    RECV_BUY_STORE,           //购买商品

    RECV_UPGRADE_PET,     //升级伙伴
    RECV_BATTLE_PET,      //上阵伙伴
    RECV_UNLOAD_PET,      //收回伙伴

    RECV_CHANGE_PARTNER,          //更换宠物
    RECV_ADVANCE_PARTNER,         //进阶宠物
    RECV_UPGRADE_PARTNER,         //升级宠物


    SHOW_3DCAMERA,            //开启3D摄像机
    HIDE_3DCAMERA,            //关闭3D摄像机
    RECV_SUBMIT_TASK,    //提交日常任务

    REQ_PLAYER_JUMP,          //请求玩家跳跃

    UPDATE_BUFF,              //更新BUFF

    GUIDE_ENTER,   //新手引导进入
    GUIDE_EXIT,    //新手引导退出
    GUIDE_FIRE,    //新手引导触发
    SECOND_TICK,   //每秒更新
    STOP_AUTOTASK, //停止自动任务
    UPDATE_THREAD_TASK_STATE, //更新主线任务状态
    UPDATE_BRANCH_TASK_STATE, //更新支线任务状态
    RECV_GATHER_MINE,         //采集回调
    RECV_CHANGE_AIMODE,       //改变AI行为
    CHANGE_SELECT_MOUNT,      //选中坐骑改变
}
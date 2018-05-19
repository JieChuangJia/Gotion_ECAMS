using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECAMSModel
{
    /// <summary>
    /// 库房
    /// </summary>
    public enum EnumStoreHouse
    { 
        A1库房,
        B1库房,
        所有
    }

    /// <summary>
    /// 货位类型
    /// </summary>
    public enum EnumGSType
    {
        货位,
        工位
    }

    /// <summary>
    /// 控制接口状态
    /// </summary>
    public enum EnumCtrlInterStatus
    { 
        未生成,
        已生成
    }

    /// <summary>
    /// 任务执行状态（控制任务、管理任务）
    /// </summary>
    public enum EnumTaskStatus
    {
        待执行,
        执行中,
        已完成,
        超时, //任务在规定时间内未完成
        错误, //任务发生错误，不可能再继续执行了，必须人工清理掉
        任务撤销
    }

    /// <summary>
    /// 任务模式
    /// </summary>
    public enum EnumTaskMode
    { 
        自动,
        手动
    }

    /// <summary>
    /// 任务类型
    /// </summary>
    public enum EnumTaskCategory
    {
        入库,
        出库,
        出入库
    }
     /// <summary>
    /// 任务类型编码
    /// 要跟数据库的任务号对应
    /// </summary>
    public enum EnumTaskName
    {
        无 = 0,
        空料框入库 = 1,
        空料框出库 = 2,
        电芯入库_A1 = 3,
        分容出库_A1 = 4,
        分容入库_A1 = 5,
        电芯出库_A1 = 6,
        电芯一次拣选 = 7,
        电芯入库_B1 = 8,
        电芯出库_B1 = 9,
        电芯二次拣选 = 10,
        电芯装箱组盘 = 11,
        空料框直接返线 = 19
       // 一次检测 = 15,
       // 二次检测 = 17
    }
    /// <summary>
    /// 货位运行状态
    /// </summary>
    public enum EnumGSRunStatus
    {
        待用,
        任务锁定,
        任务完成,
        异常
    }

    /// <summary>
    /// 满托盘标示
    /// </summary>
    public enum EnumFullTraySign
    {
        是,
        否
    }

    /// <summary>
    /// 货位存储状态
    /// </summary>
    public enum EnumGSStoreStatus
    { 
        空料框,
        有货,
        空货位
    }
 
    /// <summary>
    /// 物料状态
    /// </summary>
    public enum EnumProductStatus
    {
        无,
        初始,
        A1库老化3天,
        分容,
        A1库静置10小时,
        一次拣选,
        B1库静置10天,
        二次拣选,
        空料框 
    }
   
    /// <summary>
    /// 控制任务生成模式
    /// 主要用在手动出库选择指定位置出库
    /// </summary>
    public enum EnumCreateMode
    { 
        系统生成,
        手动生成,
        手动强制
    }

    /// <summary>
    /// 数据表枚举
    /// </summary>
    public enum EnumDataList
    { 
        控制接口表,
        控制任务表,
        管理任务表,
        库存列表
    }

    /// <summary>
    /// 流程时间是否到达
    /// </summary>
    public enum EnumWorkFolwStatus
    { 
        已达到,
        未到达,
        所有
    }
   
    /// <summary>
    /// 设备故障
    /// </summary>
    public enum EnumDevStatus
    {
        空闲,
        工作中,
        故障
    }
    public enum EnumDevType
    {
        堆垛机,
        站台,
        检测机,
        机械手
    }

    public enum EnumLogCategory
    { 
        管理层日志,
        控制层日志,
        所有
    }

    /// <summary>
    /// 日志枚举
    /// </summary>
    public enum EnumLogType
    {
		提示,
        错误,
        调试信息,
        所有
    }
    /// <summary>
    /// 报警码所属层次枚举
    /// </summary>
    public enum EnumWarnLayer
    {
        管理层,
        调度层,
        控制层
    }
    public enum EnumLogShowStatus
    {
        未显示,
        已显示
    }
    /// <summary>
    /// 枚举料框所经历的的OCV工艺过程
    /// </summary>
    public enum EnumOCVProcessStatus
    {
        空置,
        托盘装载待入A1库,
        A1库老化,
        分容出库,
        二次分容出库,
        正在一次OCV检测,
        一次OCV检测完成,
        一次分拣完成,
        B1库静置10天,
        正在二次OCV检测,
        二次OCV检测完成,
        二次分拣完成
    }

    /// <summary>
    /// OCV检测结果枚举
    /// </summary>
    public enum EnumOCVCheckResult
    {
        良品,
        不良品
    }
    public enum EnumDevCommStatus
    {
        通信正常,
        通信断开
    }
    /// <summary>
    /// 设备通信的参数类型枚举
    /// </summary>
    public enum EnumCommuDataType
    {
        DEVCOM_byte, //单字节
        DEVCOM_real, //浮点型,4字节
        DEVCOM_short,//短整型，2字节
        DEVCOM_int, //整形,4字节
        DEVCOM_string //字符串型
    }

    /// <summary>
    /// 作者:np
    /// 时间:2014年6月3日
    /// 内容:类型
    /// </summary>
    public enum EnumRequireType
    {
        成批写入,
        成批读取
    }

    /// <summary>
    /// 系统中用到的PLC
    /// </summary>
    public enum EnumDevPLC
    {
        PLC_MASTER_Q,
        PLC_STACKER_FX
    }
}

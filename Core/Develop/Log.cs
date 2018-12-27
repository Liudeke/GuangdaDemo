using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System;

public class Log
{
    //日志输出线程
    static LogOutPutThread s_LogOutPutThread = new LogOutPutThread();

    public static void Init(bool isOpenLog = true)
    {
#if UNITY_2017_1_OR_NEWER
        Debug.unityLogger.logEnabled = isOpenLog;
#else
        Debug.logger.logEnabled = isOpenLog;
#endif

        if (isOpenLog)
        {
            s_LogOutPutThread.Init();
            Application.logMessageReceivedThreaded += UnityLogCallBackThread;
            Application.logMessageReceived += UnityLogCallBack;
        }
    }

    static void UnityLogCallBackThread(string log, string track, LogType type)
    {
        LogInfo l_logInfo = new LogInfo
        {
            m_logContent = log,
            m_logTrack = track,
            m_logType = type
        };

        s_LogOutPutThread.Log(l_logInfo);
    }

    static void UnityLogCallBack(string log, string track, LogType type)
    {
        //LogInfo l_logInfo = new LogInfo
        //{
        //    m_logContent = log,
        //    m_logTrack = track,
        //    m_logType = type
        //};
    }

#if UNITY_EDITOR
    private static bool IsFullOutput = true;
#else
    private static bool IsFullOutput = true;

#endif

    public static void Info(string info)
    {
        if (IsFullOutput)
        {
            Debug.Log(ParseTool.GetCodeOutPutInfo() + "/" + info);
        }
        else
        {
            Debug.Log(info);
        }
    }
    public static void Warning(string info)
    {
        if (IsFullOutput)
        {
            Debug.LogWarning(ParseTool.GetCodeOutPutInfo() + "/" + info);
        }
        else
        {
            Debug.LogWarning(info);
        }
    }
    public static void Error(string info)
    {
        if (IsFullOutput)
        {
            Debug.LogError(ParseTool.GetCodeOutPutInfo() + "/" + info);
        }
        else
        {
            Debug.LogError(info);
        }
    }
}

public class LogInfo
{
    public string m_logContent;
    public string m_logTrack;
    public LogType m_logType;
}

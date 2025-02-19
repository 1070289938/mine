using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUtil
{





    public static long GetTimestampInMilliseconds(DateTime dateTime)
    {
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeDifference = dateTime - unixEpoch;
        return (long)timeDifference.TotalMilliseconds;
    }


    // 将以毫秒为单位的时间戳转换为DateTime对象
    public static DateTime FromMillisecondsTimestamp(long timestampInMilliseconds)
    {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return epoch.AddMilliseconds(timestampInMilliseconds);
    }
}

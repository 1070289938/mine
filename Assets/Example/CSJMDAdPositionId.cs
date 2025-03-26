
// 测试广告位id 
public static class CSJMDAdPositionId
{
#if UNITY_IOS
        public const string APP_ID = "5000546";


        /* 聚合维度广告位ID */
        // 激励视频
        public const string M_REWARD_VIDEO_V_ID = "945801623";
        public const string M_REWARD_VIDEO_H_ID = "945494739";
        // 信息流
        public const string M_NATIVE_NORMAL_ID = "945494760";
        public const string M_NATIVE_EXPRESS_ID = "945494759";
        // 开屏
        public const string M_SPLASH_EXPRESS_ID = "887418500";
        // 开屏兜底
        public const string M_SPLASH_BASELINE_APPID = "5000546";
        public const string M_SPLASH_BASELINE_ID = "800546808";
        // 横幅
        public const string M_BANNER_ID = "945494753";
        // 插全屏
        public const string M_INTERSTITAL_FULL_SCREEN_ID = "948070210";
        public const string M_INTERSTITAL_FULL_SCREEN_ID_2 = "947028072";
        public const string M_INTERSTITAL_FULL_SCREEN_ID_3 = "946961656";
        // draw
        public const string M_DRAW_ID = "948423177";
#else
    public const string APP_ID = "5674305";


    /* 聚合维度广告位ID */
    // 激励视频
    public const string M_REWARD_VIDEO_ID = "103413728";
#endif
}
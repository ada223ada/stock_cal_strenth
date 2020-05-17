using System;
using System.Collections.Generic;

namespace CalStrength.Macd
{
    class MacdAeraCalResult
    {
        internal float[] isBitch;
        internal float[] sumVal;
        internal float ret;
    }
    class MacdInfo
    {
        /// <summary>
        /// 序号，第几个(从0开始计数)
        /// </summary>
        public int index;
        public float macd;
        public float diff;
        public float low;
        public float lmacd;
        /// <summary>
        /// 所属绿柱
        /// </summary>
        public GreenMacdInfoGroup witchGroup;
        public GreenMacdInfoGroup witchLargeGroup;
    }
    class MacdInfoList : List<MacdInfo>
    {

    }
    class GreenMacdInfoGroup : List<MacdInfo>
    {
        internal MacdInfo lowMacdMInfo;
        internal float sumVal = 0;
        /// <summary>
        /// 上一个组
        /// </summary>
        internal GreenMacdInfoGroup lastGroup;
        internal MacdInfo startMInfo;
        internal MacdInfo endMInfo;

        public float SumMacd()
        {
            float result = 0;
            foreach (var item in this)
            {
                result += Math.Abs(item.macd);
            }
            return result;
        }
        public void AddByCompare(MacdInfo macdInfo)
        {
            //if (lowMInfo == null || macdInfo.low < lowMInfo.low)
            //{
            //    lowMInfo = macdInfo;
            //}
            if (lowMacdMInfo == null || macdInfo.macd < lowMacdMInfo.macd)
            {
                lowMacdMInfo = macdInfo;
            }
            this.Add(macdInfo);
        }
        public void GetFrontGroups(ref List<GreenMacdInfoGroup> groups, int index, int maxLen)
        {
            if (lastGroup != null)
            {
                if (index - lastGroup.endMInfo.index < maxLen)
                {
                    lastGroup.GetFrontGroups(ref groups, index, maxLen);
                    groups.Add(lastGroup);
                }
            }
        }
    }
}
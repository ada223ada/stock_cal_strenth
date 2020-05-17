using System;
using System.Collections.Generic;

namespace CalStrength.Macd
{
    class MacdInfoGroupList
    {
        const int compareLen = 108;
        /// <summary>
        /// 全Macd列表
        /// </summary>
        MacdInfoList totalList = new MacdInfoList();
        /// <summary>
        /// 绿柱Macd列表
        /// </summary> 
        MacdInfoList greenMacdList = new MacdInfoList();
        /// <summary>
        /// 高级别绿柱Macd列表
        /// </summary> 
        MacdInfoList largeGreenMacdList = new MacdInfoList();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="macds"></param>
        /// <param name="lmacds"></param>
        /// <param name="diffs"></param>
        public void InitByInput(float[] macds, float[] lmacds, float[] diffs)
        {
            var length = macds.Length;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    var mInfo = new MacdInfo()
                    {
                        macd = macds[i],
                        diff = diffs[i],
                        lmacd = lmacds[i],
                        index = i
                    };
                    totalList.Add(mInfo);
                    if (mInfo.macd < 0)
                    {
                        greenMacdList.Add(mInfo);
                    }
                    if (mInfo.lmacd<0)
                    {
                        largeGreenMacdList.Add(mInfo);
                    }
                }
                SepreteToGroupList(greenMacdList,
                    (x,y) => x.witchGroup = y, // 所属于的macd块儿
                    true) ;
                SepreteToGroupList(largeGreenMacdList,
                      (x, y) => x.witchLargeGroup = y// 所属于的macd块儿
                    );
            }
        }
        //遍历分割
        void SepreteToGroupList(MacdInfoList gmlist,Action<MacdInfo, GreenMacdInfoGroup> setGroupAction,bool sum=false)
        {
            int index = 0;
            GreenMacdInfoGroup current = null;
            MacdInfo tempMInfo = null;
            foreach (var item in gmlist)
            {
                if (current == null)
                {
                    current = CreteGreenInfoGroup(index, tempMInfo, null);
                }
                //当前与前一个之间有间隔
                else if (tempMInfo.index + 1 < item.index)
                {
                    //计算绿柱面积
                    if (sum)
                    {
                        current.sumVal = current.SumMacd();
                    }
                    index += 1;

                    //创建新组
                    current = CreteGreenInfoGroup(index, tempMInfo, current);
                }
                tempMInfo = item;
                if (setGroupAction!=null)
                {
                    setGroupAction(tempMInfo, current);
                }
                current.endMInfo = tempMInfo;
                current.AddByCompare(tempMInfo);
            }
            if (current != null&&sum)
            {
                current.sumVal = current.SumMacd();
            }
        }
        private GreenMacdInfoGroup CreteGreenInfoGroup(int index, MacdInfo mInfo, GreenMacdInfoGroup lastGroup)
        {
            //创建组
            var group = new GreenMacdInfoGroup()
            {
                startMInfo = mInfo
            };
            group.lastGroup = lastGroup;//指向上一个group 
            return group;
        }
        public MacdAeraCalResult IsABitch()
        {
            var result = new MacdAeraCalResult()
            {
                isBitch = new float[totalList.Count],
                sumVal=new float[totalList.Count]
            };
            for (int i = 0; i < totalList.Count; i++)
            {
                result.isBitch[i] = 0;
                result.sumVal[i] = 0;
                var curentMInfo = totalList[i];
                if (curentMInfo.macd < 0)  //&& curentMInfo.lmacd < 0
                {
                    var curGroup = curentMInfo.witchGroup;
                    if (curGroup != null)
                    {
                        result.sumVal[i] = curGroup.sumVal;
                        var frontGroups = new List<GreenMacdInfoGroup>();

                        //获取羡慕的groups
                        curGroup.GetFrontGroups(ref frontGroups, i, compareLen);
                        if (frontGroups.Count > 0)
                        {
                            foreach (var item in frontGroups)
                            {
                                if (item.endMInfo.witchLargeGroup == curentMInfo.witchLargeGroup || item.endMInfo.witchLargeGroup==null)
                                {
                                    if (item.sumVal > curGroup.sumVal)
                                    {
                                        result.isBitch[i] = curGroup.startMInfo.index - item.endMInfo.index;
                                        break;                                 
                                    }

                                }
                            }
                        }
                    }
                }
            }
            result.ret = result.isBitch[totalList.Count - 1];
            return result;
        }
    }
}

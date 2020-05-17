namespace CalStrength.ValueHub
{
    using System;
    using System.Collections.Generic;
    class ValueHubCaller
    {
        const int callLen = 10;
        Dictionary<int, Kube> totalKubeMap = new Dictionary<int, Kube>();
        Dictionary<int, ValueHubKube> valueHubs = new Dictionary<int, ValueHubKube>();
        internal void InitInput(float[] lows,float[] highs)
        {
            var length = lows.Length;
            for (int i = 0; i < length; i++)
            {
                totalKubeMap.Add(i, new Kube()
                {
                    low = lows[i],
                    high = highs[i],
                });
            }
            SeperateKubeToValueHub();
        }

        internal float[] Call()
        {
            var result = new float[totalKubeMap.Count];
            for (int i = 0; i < totalKubeMap.Count; i++)
            {
                result[i] = 0;
                if (i>1)
                {
                    for (int j = i-2; j >= Math.Max(0, j - callLen); j--)
                    {
                        if (valueHubs.ContainsKey(j))
                        {
                            if (valueHubs[j].low > totalKubeMap[i].low && valueHubs[j].low - totalKubeMap[i].low > valueHubs[j].delta)
                            {
                                result[i] = valueHubs[j].low;
                            }
                            break;
                        }
                    } 
                } 
            }
            return result;
        }

        void SeperateKubeToValueHub()
        {
            if (totalKubeMap.Count>2)
            {
                for (int i = 0; i < totalKubeMap.Count-2; i++)
                {
                    var maxLow = GetMaxLow(totalKubeMap[i], totalKubeMap[i + 1], totalKubeMap[i + 2]);
                    var minHigh = GetMinHigh(totalKubeMap[i], totalKubeMap[i + 1], totalKubeMap[i + 2]);
                    if (minHigh>maxLow)
                    {
                        valueHubs.Add(i, new ValueHubKube()
                        {
                            high = minHigh,
                            low = maxLow,
                            delta = minHigh - maxLow
                        }) ;
                    }
                }
            }
        }
        float GetMaxLow(params Kube[] kubes)
        {
            float result = 0;
            foreach (var item in kubes)
            {
                if (item.low>result)
                {
                    result = item.low;
                }
            }
            return result;
        }
        float GetMinHigh(params Kube[] kubes)
        {
            float result = 0;
            if (kubes!=null&&kubes.Length>0)
            {
                result = kubes[0].high;
                foreach (var item in kubes)
                {
                    if (item.high < result)
                    {
                        result = item.high;
                    }
                }
            }
            return result;
        }
    }
}

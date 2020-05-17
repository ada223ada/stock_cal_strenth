namespace CalStrength.RiseLimitAway
{
    using System;
    using System.Collections.Generic;
    class RiseLimitAwayCaller
    {
        List<RiseLimitKube> kubes = new List<RiseLimitKube>();
        RiseLimitKube lastRiseLimitKube;
        internal void InitInput(float[] closes, float[] highs)
        {
            for (int i = 0; i < closes.Length; i++)
            {
                var kube = new RiseLimitKube
                {
                    close = closes[i],
                    high = highs[i],
                    index = i,
                };
                kubes.Add(kube);
                if (i>0)
                {
                    if (highs[i] >= closes[i - 1] * 1.090f)
                    {
                        kube.success = true;//涨停成功
                        if (lastRiseLimitKube != null)
                        {
                            kube.lastSuccessKube = lastRiseLimitKube;
                        }
                        lastRiseLimitKube = kube;
                    }
                    else
                    {
                        if (lastRiseLimitKube != null)
                        {
                            kube.lastSuccessKube = lastRiseLimitKube;
                        }
                    }
                    kube.CalLastLimitCount();
                }
                
            }
        }
        internal RiseLimitKubeResult Call()
        {
            var length = kubes.Count;
            var result = new RiseLimitKubeResult(length); 
            for (int i = 0; i < length; i++)
            {
                result.deleys[i] = kubes[i].之前涨停持续次数;
                result.aways[i] = kubes[i].之前涨停距离;
            }
            
            return result;
        }
    }
}

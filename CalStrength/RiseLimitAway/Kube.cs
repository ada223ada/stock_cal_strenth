using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalStrength.RiseLimitAway
{
    class RiseLimitKube
    {
        public bool success;//涨停成功
        public RiseLimitKube lastSuccessKube;
        public int index;
        public int 之前涨停持续次数;
        public int 之前涨停距离;
        public float close;
        public float high;
        public void CalLastLimitCount()
        {
            if (this.success)
            {
                if (lastSuccessKube!=null)
                {
                    if (this.index - lastSuccessKube.index==1)
                    {
                        之前涨停持续次数=lastSuccessKube.之前涨停持续次数 + 1;
                    }
                }
            }
            else
            {
                if (lastSuccessKube != null)
                {
                    之前涨停持续次数 = lastSuccessKube.之前涨停持续次数+1;
                    之前涨停距离 = this.index - lastSuccessKube.index;
                }
            }
        }
    }

    class RiseLimitKubeResult
    {
        public float[] aways;
        public float[] deleys;
        public int _count;
        public RiseLimitKubeResult(int count)
        {
            _count = count;
            aways = new float[count];
            deleys = new float[count];
        }
        public float IsAbitch(int inside)
        {
            return aways[_count - 1] ;
            //return  aways[_count - 1] - deleys[_count - 1]> inside;
        }
    }
}

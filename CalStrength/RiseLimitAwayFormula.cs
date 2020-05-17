namespace CalStrength
{
    using CalStrength.RiseLimitAway;
    using fox.api;
    public class RiseLimitAwayFormula : Formula
    {
        public float Cal(string highValName, string closeValName,string awayName,string deleyName)
        {
            var highs = (float[])GetVarData(highValName);
            var closes = (float[])GetVarData(closeValName);
            var riseLimitAwayCaller = new RiseLimitAwayCaller();
            riseLimitAwayCaller.InitInput(closes, highs);
            var result = riseLimitAwayCaller.Call();

            SetVarData(deleyName, result.deleys);
            SetVarData(awayName, result.aways);
             
            return result.IsAbitch(3);
        }
    }
}
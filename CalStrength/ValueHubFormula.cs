namespace CalStrength
{
    using CalStrength.ValueHub;
    using fox.api;
    public class ValueHubFormula : Formula
    {
        public float Cal(string highValName, string lowValName,string resultName)
        {
            var highs = (float[])GetVarData(highValName);
            var lows = (float[])GetVarData(lowValName);
            var valueHubCaller = new ValueHubCaller();
            valueHubCaller.InitInput(lows, highs);
            var result = valueHubCaller.Call();
            SetVarData(resultName, result);
            return result[result.Length-1];
        }
    }
}
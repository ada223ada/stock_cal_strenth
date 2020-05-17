namespace CalStrength
{
    using Macd;
    using fox.api;
    public class MACDAreaFormula:Formula
    {
        public float Cal(string macdName, string diffValName, string largeMacdName, string sumValName, string resultName)
        {
            var macds = (float[])GetVarData(macdName);
            var lmacds = (float[])GetVarData(largeMacdName);
            var diffs = (float[])GetVarData(diffValName);
            var groupList = new MacdInfoGroupList();
            groupList.InitByInput(macds, lmacds, diffs);
            var result = groupList.IsABitch();
            SetVarData(resultName, result.isBitch);
            SetVarData(sumValName, result.sumVal);
            return result.ret; 
        }
    }
}
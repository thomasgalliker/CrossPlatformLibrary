
namespace CrossPlatformLibrary.System.Collection.Generic
{
    public class AlphaGroupKeyAlgorithm : IGroupKeyAlgorithm
    {
        public string GetGroupKey(string inputString)
        {
            char firstChar = inputString[0];
            if (char.IsNumber(firstChar))
            {
                return "#";
            }
            return firstChar.ToString()
                            .RemoveDiacritics()
                            .ToLower();
        }
    }
}

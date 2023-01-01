using System.Collections.Generic;
using System.Runtime.InteropServices;
using _core.AcountInfo;

namespace _core.Script.Utility.Extension
{
    
    
    
    
    public static class CharacterExtension
    {

        private static Dictionary<string, string> _characterSpriteMapper = new Dictionary<string, string>()
        {
            {"莉莉娅", "LLYSprite"},
            {"萝莎莉娅","LSLYSprite"}
        };
        
        public static string GetSpriteName(this CharacterInfo characterInfo)
        {
            if (_characterSpriteMapper.TryGetValue(characterInfo.Name,out string spriteName))
            {
                return spriteName;
            }
            return default;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtBinder
{
    internal class ChineseNumberConverter
    {
        // 字典對應：單位、數字
        private static readonly Dictionary<char, int> NumberMap = new Dictionary<char, int>
        {
            { '零', 0 },
            { '一', 1 },
            { '二', 2 },
            { '三', 3 },
            { '四', 4 },
            { '五', 5 },
            { '六', 6 },
            { '七', 7 },
            { '八', 8 },
            { '九', 9 }
        };

        private static readonly Dictionary<char, int> UnitMap = new Dictionary<char, int>
        {
            { '十', 10 },
            { '百', 100 },
            { '千', 1000 },
            { '萬', 10000 },
            { '億', 100000000 }
        };

        public static string ConvertToArabic(string chineseNumber)
        {
            if (string.IsNullOrEmpty(chineseNumber))
                return "0";

            int result = 0;    // 最終的數字
            int current = 0;   // 當前數字
            int unit = 1;      // 單位
            bool hasUnit = false; // 標記是否遇到單位

            // 從右到左解析字串
            for (int i = chineseNumber.Length - 1; i >= 0; i--)
            {
                char c = chineseNumber[i];

                if (NumberMap.ContainsKey(c))
                {
                    current = NumberMap[c];
                    hasUnit = false;
                }
                else if (UnitMap.ContainsKey(c))
                {
                    unit = UnitMap[c];
                    result += (current == 0 ? 1 : current) * unit;
                    current = 0;
                    hasUnit = true;
                }
                else
                {
                    throw new ArgumentException($"無法解析的字符: {c}");
                }
            }

            // 處理最後沒有單位的數字
            if (!hasUnit)
            {
                result += current;
            }

            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.ConsoleUI.GarageManager;

namespace Ex03.ConsoleUI
{
    public class InputValidator
    {
        public static int ParseEnumOption(string i_UserInput, int i_MaxValue)
        {
            bool isCorrectParsing = int.TryParse(i_UserInput, out int parsedValue);

            if (!(isCorrectParsing && parsedValue <= i_MaxValue))
            {
                throw new FormatException(string.Format($"Invalid option!"));
            }

            return parsedValue;
        }

        public static float TryParseFloat(string userInput)
        {
            if (!float.TryParse(userInput, out float parsedValue))
            {
                throw new FormatException($"Invalid input! Please enter a float value");
            }

            return parsedValue;
        }
    }
}

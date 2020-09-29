using System;
using System.Collections.Generic;
using System.Text;

namespace LenguajeProgramacionII.Helpers
{
    public class Util
    {
        private const int TableWidth = 100;

        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

        public void PrintRow(string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCenter(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        public string AlignCenter(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

        public string GetValue(string askLabel)
        {
            string value;

            do
            {
                Console.WriteLine(askLabel);
                value = Console.ReadLine();

                if (string.IsNullOrEmpty(value)) Console.WriteLine("Valor introducido incorrecto");
            } while (string.IsNullOrEmpty(value));

            return value;
        }
    }
}

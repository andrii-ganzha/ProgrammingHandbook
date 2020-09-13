using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            UnitConversion();            
        }
        /// <summary>
        /// This sample shows how to work with UnitConversion.
        /// </summary>
        public static void UnitConversion()
        {
            foreach (LengthUnit unit in Enum.GetValues(typeof(LengthUnit)))
            {
                string s = string.Format("1 point = {0} {1}",
                    LengthUnitConverter.Convert(1, LengthUnit.Point, unit),
                    unit.ToString().ToLowerInvariant());

                Console.WriteLine(s);
            }

            Console.ReadLine();
        }
    }
}

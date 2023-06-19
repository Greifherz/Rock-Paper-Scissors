using Random = System.Random;

namespace Extensions
{
    public static class StringUtilityExtensions
    {
        private static string[] ValidNames = { "James","Robert","John","Michael","William","David","Richard","Joseph","Thomas","Charles" }; 
        //Got names from https://www.ssa.gov/oact/babynames/decades/century.html the 1st 10 names there for births in 1921-2020

        public static string GenerateRandomName()
        {
            return ValidNames[new Random().Next(0, ValidNames.Length)];
        }

        public static string TransformToRandomName(this string self)
        {
            self = GenerateRandomName();
            return self;
        }
    }
}

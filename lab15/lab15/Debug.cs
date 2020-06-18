using System;


namespace Lab15
{
    public static class Debug
    {
        private const int _unitTime = 1000;

        static public int UnitTime
        {
            get { return _unitTime; }
        }


        static public void Write(string str)
        {
            if (str is null)
            {
                throw new NullReferenceException();
            }

            Console.WriteLine(DateTime.Now.ToLongTimeString() + ": " + str);
            Console.WriteLine();
        }
    }
}

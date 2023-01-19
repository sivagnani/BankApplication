namespace BankApplication.helper
{
    public class InputsAndOutputs
    {
        public T GetInput<T>()
        {
            T input = default(T);
            try
            
            {
                input = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
            }
            catch (Exception)
            {
                Console.WriteLine("Enter valid Input");
            }
            return input;
            
        }
        public void Display(string text)
        {
            Console.Write(text);
        }
        public void DisplayLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}

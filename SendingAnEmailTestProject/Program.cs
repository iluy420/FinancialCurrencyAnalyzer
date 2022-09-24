using Extensions;

namespace SendingAnEmailTestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkingWithEmail.EmailRegistration(
                "FinancialCurrencyAnalyzerTest1@gmail.com",
                "Юзверь",
                WorkingWithPasswords.GetGenerateAlphanumericKey(10));
            
        }
    }
}

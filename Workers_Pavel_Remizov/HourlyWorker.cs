
namespace Workers_Pavel_Remizov
{
    class HourlyWorker : BaseWorkerClass
    {
        private decimal _hourlyRate;
        //Для «повременщиков» формула для расчета такова: 
        //«среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»
        public HourlyWorker(string firstName, string lastName, int age, decimal hourlyRate) :
            base(firstName, lastName, age)
        {
            _hourlyRate = hourlyRate;
        }
        public override decimal AverageMonthlyWagesCalculation() => 20.8m * 8 * _hourlyRate;
    }
}

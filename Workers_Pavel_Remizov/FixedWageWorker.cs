
namespace Workers_Pavel_Remizov
{
    class FixedWageWorker : BaseWorkerClass
    {
        private decimal _salary;
        public FixedWageWorker(string firstName, string lastName, int age, decimal salary) :
            base(firstName, lastName, age)
        {
            _salary = salary;
        }
        //для  работников  с  фиксированной  оплатой: 
        //«среднемесячная заработная плата = фиксированная месячная оплата»;
        public override decimal AverageMonthlyWagesCalculation() => _salary;
    }
}

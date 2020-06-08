using System;
using System.Collections;


namespace Workers_Pavel_Remizov
{
    class WorkerEnumerable: IEnumerable
    {
        private readonly BaseWorkerClass[] _workers;
        private Random _random = new Random();
        public WorkerEnumerable(int length)
        {
            _workers = new BaseWorkerClass[length];
            CreateWorkersArray();
        }
        private void CreateWorkersArray()
        {
            for (int i = 0; i < _workers.Length / 2; i++)
                _workers[i] = new FixedWageWorker($"Muffin_{i}", $"Lover_{i}",
                                                  _random.Next(18, 30), _random.Next(30_000, 40_000));
            for (int i = _workers.Length / 2; i < _workers.Length; i++)
            {
                _workers[i] = new HourlyWorker($"Bold_{i}", $"Orange_{i}",
                                               _random.Next(18, 30), _random.Next(100, 200));
            }
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _workers.Length; i++)
                yield return _workers[i];
        }
    }
}

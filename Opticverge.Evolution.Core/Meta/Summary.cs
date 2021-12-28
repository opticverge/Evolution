using System.Diagnostics;

namespace Opticverge.Evolution.Core.Meta
{
    public class Summary
    {
        private readonly Stopwatch _stopwatch;

        public ulong Generated { get; set; }

        public int Elapsed
        {
            get => _stopwatch.Elapsed.Milliseconds;
        }

        public Summary()
        {
            _stopwatch = new Stopwatch();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void End()
        {
            _stopwatch.Stop();
        }
    }
}

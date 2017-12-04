
using System.Collections.Generic;

namespace Tests.TestDomain.Loggers
{
    public class TestLogger
    {
        private readonly List<string> log;

        public TestLogger(List<string> log)
        {
            this.log = log;
        }

        public void CompleteThird()
        {
            log.Add("third event");
        }
    }
}
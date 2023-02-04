using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    internal class ExecutionHelper
    {
        internal static async Task TryExecuteAsync(Func<Task> execute)
        {
            try
            {
                await execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal static async Task ExecuteAndLogDuration(Func<Task> execute)
        {
            DateTime start = DateTime.Now;

            await execute();

            Console.WriteLine($"Duration {DateTime.Now - start}");
        }
    }
}

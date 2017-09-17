using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracelogTest.Model
{
    public static class Command
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Public methods

        public static void AddTestMessage()
        {
            var trace = new SingleTrace();
            trace.Text = FormatStackTrace(Environment.StackTrace);
            trace.Type = SingleTrace.TraceType.Error;
            Workspace.Instance?.AddTrace(trace);
        }

        public static void ClearAllTraces()
        {
            Workspace.Instance?.Traces.Clear();
        }

        #endregion

        #region Private methods

        static string FormatStackTrace(string stackTrace)
        {
            // This methods assumes stackTrace includes 2 unnecessary lines at its top.
            // check line count
            int n = 0;
            foreach (var c in stackTrace)
            {
                if (c == '\n')
                {
                    n++;
                    if (n >= 2)
                        break;
                }
            }

            n = 0;
            int count = 0;
            foreach (var c in stackTrace)
            {
                if (c == '\n')
                    n++;
                count++;
                if (n == 2)
                    break;
            }

            return stackTrace.Remove(0, count);
        }


        #endregion
    }
}

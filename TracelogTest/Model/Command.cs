using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            trace.Text = "test";
            trace.Type = SingleTrace.TraceType.Warning;
            trace.Stacktrace = FormatStackTrace(Environment.StackTrace); 
            Workspace.Instance?.AddTrace(trace);
        }

        public static void ClearAllTraces()
        {
            Workspace.Instance?.Traces.Clear();
        }

        public static void SetTrace2Clipboard()
        {
            Workspace.Instance?.SetTrace2Clipboard();
        }

        public static void SetStacktrace2Clipboard()
        {
            Workspace.Instance?.SetStacktrace2Clipboard();
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

            // Remove first 2 lines
            n = 0;
            int count = 0;
            foreach (var c in stackTrace)
            {
                if (c == '\n')
                    n++;
                count++;
                if (n == 2)
                {
                    return stackTrace.Remove(0, count);
                }
            }

            return stackTrace;
        }

        #endregion
    }
}
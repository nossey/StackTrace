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

        public static void Info(string info)
        {
            var trace = new SingleTrace();
            trace.Type = SingleTrace.TraceType.Info;
            trace.Text = info;
            AddTrace(trace);
        }

        public static void Warning(string warning)
        {
            var trace = new SingleTrace();
            trace.Type = SingleTrace.TraceType.Warning;
            trace.Text = warning;
            AddTrace(trace);
        }

        public static void Error(string error)
        {
            var trace = new SingleTrace();
            trace.Type = SingleTrace.TraceType.Error;
            trace.Text = error;
            AddTrace(trace);
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

        static void AddTrace(SingleTrace trace)
        {
            trace.Stacktrace = FormatStackTrace(Environment.StackTrace); 
            Workspace.Instance?.AddTrace(trace);
        }

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
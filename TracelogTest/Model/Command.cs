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

        public static void AddMessage()
        {
            var trace = new SingleTrace();
            trace.Text = "Hello";
            Workspace.Instance?.AddTrace(trace);
        }

        public static void ClearAllTraces()
        {
            Workspace.Instance?.Traces.Clear();
        }

        #endregion

        #region Private methods
        #endregion
    }
}

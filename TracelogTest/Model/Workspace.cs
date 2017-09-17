using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Livet;

namespace TracelogTest.Model
{
    public class Workspace
    {
        #region Declaration
        #endregion

        #region Fields

        ObservableSynchronizedCollection<SingleTrace> _Traces = new ObservableSynchronizedCollection<SingleTrace>();

        static Workspace _Instance;

        #endregion

        #region Properties

        public static Workspace Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Workspace();
                return _Instance;
            }
        }

        public ObservableSynchronizedCollection<SingleTrace> Traces
        {
            get
            {
                return _Traces;
            }
            private set
            {
                _Traces = value;
            }
        }

        #endregion

        #region Public methods

        public void AddTrace(SingleTrace trace)
        {
            if (trace == null)
                throw new ArgumentNullException();

            Traces.Add(trace);
        }

        #endregion

        #region Private methods

        Workspace()
        {
            var trace1 = new SingleTrace();
            trace1.Text = "Hello";
            var trace2 = new SingleTrace();
            trace2.Text = "Bye";
            Traces.Add(trace1);
            Traces.Add(trace2);
        }
        

        #endregion
    }
}
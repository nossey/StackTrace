using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Livet;

namespace TracelogTest.Model
{
    public class Workspace : NotificationObject
    {
        #region Declaration
        #endregion

        #region Fields

        static Workspace _Instance = new Workspace();
        SingleTrace.TraceType _VisibleType = SingleTrace.TraceType.All;

        #endregion

        #region Properties

        public static Workspace Instance
        {
            get
            {
                return _Instance;
            }
        }

        public ObservableSynchronizedCollection<SingleTrace> Traces { get; private set; } = new ObservableSynchronizedCollection<SingleTrace>();
        public ObservableCollection<VisibleTraceType> VisibleTraceTypes { get; private set; } = new ObservableCollection<VisibleTraceType>();

        public SingleTrace.TraceType CurrentVisibleType
        {
            get
            {
                return _VisibleType;
            }
            set
            {
                if (_VisibleType == value)
                    return;
                _VisibleType = value;
                RaisePropertyChanged();

                foreach (var trace in Traces)
                {
                    trace.Visible = trace.Type == _VisibleType || _VisibleType == SingleTrace.TraceType.All;
                }
                RaisePropertyChanged(nameof(Traces));
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
            var all = new VisibleTraceType(SingleTrace.TraceType.All);
            all.IsSelected = true;
            VisibleTraceTypes.Add(all);
            VisibleTraceTypes.Add(new VisibleTraceType(SingleTrace.TraceType.Info));
            VisibleTraceTypes.Add(new VisibleTraceType(SingleTrace.TraceType.Warning));
            VisibleTraceTypes.Add(new VisibleTraceType(SingleTrace.TraceType.Error));

            var trace1 = new SingleTrace();
            trace1.Text = "Hello";
            trace1.Type = SingleTrace.TraceType.Info;
            var trace2 = new SingleTrace();
            trace2.Text = "Bye";
            trace2.Type = SingleTrace.TraceType.Info;
            Traces.Add(trace1);
            Traces.Add(trace2);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Forms;
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
        string _SearchText = string.Empty;

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
                UpdateTraceVisibility();
                RaisePropertyChanged(nameof(Traces));
            }
        }

        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                if (_SearchText == value)
                    return;
                _SearchText = value;
                RaisePropertyChanged();
                UpdateTraceVisibility();
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

        public void SetTrace2Clipboard()
        {
            var selctedOne = Traces.FirstOrDefault(t => t.IsSelected);
            if (!string.IsNullOrEmpty(selctedOne.Text))
                Clipboard.SetText(selctedOne.Text);
        }

        public void SetStacktrace2Clipboard()
        {
            var selctedOne = Traces.FirstOrDefault(t => t.IsSelected);
            if (!string.IsNullOrEmpty(selctedOne.Stacktrace))
                Clipboard.SetText(selctedOne.Stacktrace);
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

        void UpdateTraceVisibility()
        {
            foreach (var trace in Traces)
            {
                bool SearchTextHit = (!string.IsNullOrEmpty(SearchText)) ? trace.Text.Contains(SearchText) : true;
                bool TypeMatched = trace.Type == CurrentVisibleType || CurrentVisibleType == SingleTrace.TraceType.All;
                trace.Visible = SearchTextHit && TypeMatched;
            }
        }

        #endregion
    }
}
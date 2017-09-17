using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using TracelogTest.Model;
using static TracelogTest.Model.Command;

namespace TracelogTest.ViewModel
{

    public class SingleTraceViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields

        SingleTrace _Model;


        #endregion

        #region Properties

        public string Trace
        {
            get
            {
                return Model.Text;
            }
        }

        SingleTrace Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
            }
        }


        #endregion

        #region Public methods

        public SingleTraceViewModel(SingleTrace model)
        {
            Model = model;
            var listener = new PropertyChangedEventListener(model)
            {
                (sender, e)=>
                {
                    RaisePropertyChanged(e.PropertyName);
                }
            };
            CompositeDisposable.Add(listener);
        }

        #endregion

        #region Private methods
        #endregion
    }

    public class TracelogViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public ReadOnlyDispatcherCollection<SingleTraceViewModel> TraceLogList { get; private set; }

        public ViewModelCommand AddTraceTest { get; } = new ViewModelCommand(() =>
        {
            AddMessage();
        });

        #endregion

        #region Public methods

        public TracelogViewModel()
        {
            TraceLogList = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                Workspace.Instance.Traces,
                (m) => new SingleTraceViewModel(m),
                DispatcherHelper.UIDispatcher);
            CompositeDisposable.Add(TraceLogList);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
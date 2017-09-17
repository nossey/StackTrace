using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using TracelogTest.Model;
using static TracelogTest.Model.Command;
using static TracelogTest.ViewModel.ColorDefinition;

namespace TracelogTest.ViewModel
{

    public class SingleTraceViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public string Trace
        {
            get
            {
                return Model.Text;
            }
        }

        public SolidColorBrush TraceColor
        {
            get
            {
                switch (Model.Type)
                {
                    case SingleTrace.TraceType.Info:
                        return InfoColor;
                    case SingleTrace.TraceType.Warning:
                        return WarningColor;
                    case SingleTrace.TraceType.Error:
                        return ErrorColor;
                }
                return new SolidColorBrush(Colors.White);
            }
        }
        
        public bool Visible
        {
            get
            {
                return Model.Visible;
            }
        }

         SingleTrace Model { get; set; }

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


    public class VisibleTraceTypeViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public bool IsSelected
        {
            get
            {
                return Model.IsSelected;
            }
            set
            {
                if (Model.IsSelected == value)
                    return;
                Model.IsSelected = value;
                if (value)
                    Workspace.Instance.CurrentVisibleType = Model.Type;
            }
        }

        public string DisplayType
        {
            get
            {
                return Model.Type.ToString();
            }
        }

        VisibleTraceType Model;

        #endregion

        #region Public methods

        public VisibleTraceTypeViewModel(VisibleTraceType model)
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

        public ReadOnlyDispatcherCollection<VisibleTraceTypeViewModel> VisibilityList { get; private set; }

        public ViewModelCommand AddTraceTest { get; } = new ViewModelCommand(() =>
        {
            AddTestMessage();
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

            VisibilityList = ViewModelHelper.CreateReadOnlyDispatcherCollection(
                Workspace.Instance.VisibleTraceTypes,
                (m) => new VisibleTraceTypeViewModel(m),
                DispatcherHelper.UIDispatcher
                );
            CompositeDisposable.Add(VisibilityList);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
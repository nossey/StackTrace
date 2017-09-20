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

        public string Stacktrace
        {
            get
            {
                return Model.Stacktrace;
            }
        }

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
            }
        }

        SingleTrace Model { get; set; }

        public ViewModelCommand Copy { get; } = new ViewModelCommand(() =>
        {
            SetTrace2Clipboard();
        });

        public ViewModelCommand CopyStacktrace { get; } = new ViewModelCommand(() =>
        {
            SetStacktrace2Clipboard();
        });

        public ViewModelCommand Clear { get; } = new ViewModelCommand(() =>
        {
            ClearAllTraces();
        });

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

    public class SearchTextViewModel : Livet.ViewModel 
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public string SearchText
        {
            get
            {
                return Workspace.Instance?.SearchText;
            }
            set
            {
                if (Workspace.Instance.SearchText != value)
                    Workspace.Instance.SearchText = value;
            }
        }

        #endregion

        #region Public methods

        public SearchTextViewModel()
        {
            var listener = new PropertyChangedEventListener(Workspace.Instance)
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

        ViewModelCommand _AddTraceTest;

        #endregion

        #region Properties

        public ReadOnlyDispatcherCollection<SingleTraceViewModel> TraceLogList { get; private set; }

        public ReadOnlyDispatcherCollection<VisibleTraceTypeViewModel> VisibilityList { get; private set; }

        public SearchTextViewModel SearchTextVM { get; private set; } = new SearchTextViewModel();

        public DebugViewModel DebugVM { get; private set; } = new DebugViewModel(Workspace.Instance?.Debug);

        public ViewModelCommand AddTraceTest
        {
            get
            {
                if (_AddTraceTest == null)
                    _AddTraceTest = new ViewModelCommand(() =>
                    {
                        var trace = new SingleTrace();
                        trace.Text = Workspace.Instance?.Debug.DebugMessage;
                        trace.Type = Workspace.Instance.Debug.SelectedType;
                        AddTrace(trace);
                    });
                return _AddTraceTest;
            }
        }

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
            CompositeDisposable.Add(SearchTextVM);
            CompositeDisposable.Add(DebugVM);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracelogTest.Model;
using Livet;
using Livet.Commands;
using Livet.EventListeners;
using static TracelogTest.Model.Command;

namespace TracelogTest.ViewModel
{

    public class DebugMessageTypeViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields


        #endregion

        #region Properties

        DebugMessageType Model;

        public string Type
        {
            get
            {
                return Model.Type.ToString();
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


        #endregion

        #region Public methods

        public DebugMessageTypeViewModel(DebugMessageType model)
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

    public class DebugViewModel : Livet.ViewModel
    {
        #region Declaration
        #endregion

        #region Fields

        ViewModelCommand _AddTrace;

        #endregion

        #region Properties

        public ReadOnlyDispatcherCollection<DebugMessageTypeViewModel> DebugMessageTypes { get; private set; }

        public int SelectedIndex
        {
            get
            {
                var find = DebugMessageTypes.FirstOrDefault(type => type.IsSelected);
                return DebugMessageTypes.IndexOf(find);
            }
            set
            {
                var find = DebugMessageTypes.ElementAt(value);
                if (find == null)
                    return;
                foreach(var type in DebugMessageTypes)
                {
                    if (type != find)
                        type.IsSelected = false;
                }
                find.IsSelected = true;
            }
        }

        public DebugMessageTypeViewModel SelectedType
        {
            get
            {
                return DebugMessageTypes.ElementAt(SelectedIndex);
            }
        }

        public string DebugMessage
        {
            get
            {
                return Model.DebugMessage;
            }
            set
            {
                if (Model.DebugMessage == value)
                    return;
                Model.DebugMessage = value;
            }
        }

        public ViewModelCommand DebugAddTrace
        {
            get
            {
                if (_AddTrace == null)
                    _AddTrace = new ViewModelCommand(() =>{
                        switch (Model.SelectedType)
                        {
                            case SingleTrace.TraceType.Info:
                                Info(Model.DebugMessage);
                                break;
                            case SingleTrace.TraceType.Warning:
                                Warning(Model.DebugMessage);
                                break; 
                            case SingleTrace.TraceType.Error:
                                Error(Model.DebugMessage);
                                break;
                        }
                    });
                return _AddTrace;
            }
        }
        Debug Model { get; set; }

        #endregion

        #region Public methods

        public DebugViewModel(Debug model)
        {
            Model = model;
            DebugMessageTypes = Livet.ViewModelHelper.CreateReadOnlyDispatcherCollection(
                Workspace.Instance?.Debug.Types,
                (m) => { return new DebugMessageTypeViewModel(m); },
                Livet.DispatcherHelper.UIDispatcher
                );
            CompositeDisposable.Add(DebugMessageTypes);
        }

        #endregion

        #region Private methods
        #endregion
    }
}
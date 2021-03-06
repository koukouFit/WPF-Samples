using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace MVVM.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IHaveDisplayName>.Collection.OneActive, IShell, IHandle<string>
    {
        IEventAggregator _eventAggregator;
        private string _eventString;

        [ImportingConstructor]
        public ShellViewModel(
            [ImportMany]
            IEnumerable<IHaveDisplayName> items,
            IEventAggregator eventAggregator)
        {
            Items.AddRange(items);
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public string EventString
        {
            get { return _eventString; }
            set 
            { 
                _eventString = value;
                NotifyOfPropertyChange(() => EventString);
            }
        }

        public void Handle(string message)
        {
            EventString = message;
        }
    }
}

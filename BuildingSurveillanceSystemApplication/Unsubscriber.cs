using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingSurveillanceSystemApplication
{
    public class Unsubscriber<ExternalVisitor> : IDisposable
    {
        private List<IObserver<ExternalVisitor>> _observers;
        private IObserver<ExternalVisitor> _observer;

        public Unsubscriber(List<IObserver<ExternalVisitor>> observers, IObserver<ExternalVisitor> observer)
        {
            _observer = observer;
            _observers = observers;
        }

        public void Dispose()
        {
            if(_observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}

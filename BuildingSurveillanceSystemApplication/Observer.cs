using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingSurveillanceSystemApplication
{
    public abstract class Observer : IObserver<ExternalVisitor>
    {
        protected List<ExternalVisitor> _externalVisitors = new List<ExternalVisitor>();
        IDisposable _cancellation;

        public abstract void OnCompleted();
        public abstract void OnError(Exception error);
        public abstract void OnNext(ExternalVisitor value);
        
        public void Subscribe(IObservable<ExternalVisitor> provider)
        {
            _cancellation = provider.Subscribe(this);
        }

        public void UnSubscribe(IObservable<ExternalVisitor> provider)
        {
            _cancellation.Dispose();
            _externalVisitors.Clear();
        }
    }
}

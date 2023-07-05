using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildingSurveillanceSystemApplication
{
    public class SecuritySurveillanceHub : IObservable<ExternalVisitor>
    {
        private List<ExternalVisitor> _externalVisitors;
        private List<IObserver<ExternalVisitor>> _observers;

        public SecuritySurveillanceHub()
        {
            _externalVisitors = new List<ExternalVisitor>();
            _observers = new List<IObserver<ExternalVisitor>>();
        }

        // When new observer subscribes to the observable
        public IDisposable Subscribe(IObserver<ExternalVisitor> observer)
        {
            // If the observer doesn't exist in the list then add the observer
            if(!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            // Let the observer know all the external visitors who have entered
            foreach (var externalVisitor in _externalVisitors)
            {
                observer.OnNext(externalVisitor);
            }

            return new Unsubscriber<ExternalVisitor>(_observers, observer);
        }

        public void ConfirmExternalVisitorEntersBuilding(int id, string firstName, string lastName, string companyName, string jobTitle, DateTime entryDateTime, int employeeContactId)
        {
            ExternalVisitor externalVisitor = new ExternalVisitor
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                CompanyName = companyName,
                JobTitle = jobTitle,
                EntryDateTime = entryDateTime,
                InBuilding = true,
                EmployeeContactId = employeeContactId,
            };

            _externalVisitors.Add(externalVisitor);

            // Loop through each observer in the _observer list and send a notify using OnNext() with the external visitor data 
            // to let each observer know that a visitor has entered the building
            foreach(var observer in _observers)
            {
                observer.OnNext(externalVisitor);
            }
        }

        public void ConfirmExternalVisitorExitsBuilding(int externalVisitorId, DateTime exitDateTime)
        {
            var externalVisitor = _externalVisitors.FirstOrDefault(visitor => visitor.Id == externalVisitorId);
           
            if(externalVisitor != null)
            {
                externalVisitor.ExitDateTime = exitDateTime;
                externalVisitor.InBuilding = false;

                // Loop through each observer in the _observer list and send a notify using OnNext() with the external visitor data 
                // to let each observer know that a visitor has exited the building
                foreach (var observer in _observers)
                {
                    observer.OnNext(externalVisitor);
                }
            }
        }

        // We will stop sending notifications to observers when the cutoff time for visitors has been reached and all visitors have left the building
        public void BuildingEntryCutOffTimeReached()
        {
            if(_externalVisitors.Where(visitor => visitor.InBuilding == true).ToList().Count == 0)
            {
                foreach (var observer in _observers)
                {
                    observer.OnCompleted();
                }
            }
        }
    }


}

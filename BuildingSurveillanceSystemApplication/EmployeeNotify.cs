using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildingSurveillanceSystemApplication
{
    public class EmployeeNotify : Observer
    {
        IEmployee _employee = null;


        public EmployeeNotify(IEmployee employee)
        {
            _employee = employee;
        }

        public override void OnCompleted()
        {
            string heading = $"{_employee.FirstName + " " + _employee.LastName} Daily Visitor's Report";
            Console.WriteLine();
            Console.WriteLine(heading);
            Console.WriteLine(new string('-', heading.Length));
            Console.WriteLine();

            foreach(var externalVisitor in _externalVisitors)
            {
                externalVisitor.InBuilding = false;
                Console.WriteLine($"{externalVisitor.Id,-6}{externalVisitor.FirstName,-15}{externalVisitor.LastName,-15}{externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss"),-25}{externalVisitor.ExitDateTime.ToString("dd MMM yyyy hh:mm:ss tt"),-25}");
                Console.WriteLine();
            }

        }

        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public override void OnNext(ExternalVisitor value)
        {
            var externalVisitor = value;
            if(externalVisitor.EmployeeContactId == _employee.Id)
            {
                // Returns reference to visitor if the visitor already exists in the list
                var externalVisitorListItem = _externalVisitors.FirstOrDefault(visitor => visitor.Id == externalVisitor.Id);

                // Visitor does not exists, send notification
                if(externalVisitorListItem == null)
                {
                    _externalVisitors.Add(externalVisitor);
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Employee);
                    Console.WriteLine($"{_employee.FirstName + " " + _employee.LastName}, your visitor has arrived. Visitor ID({externalVisitor.Id}), FirstName({externalVisitor.FirstName}), LastName({externalVisitor.LastName}), entered the building at, DateTime({externalVisitor.EntryDateTime.ToString("dd MMM yyyy hh:mm:ss")})");
                    OutputFormatter.ChangeOutputTheme(OutputFormatter.TextOutputTheme.Normal);
                    Console.WriteLine();
                }
                // Visitor does exists
                else
                {
                    // Visitor has left so update the reference to reflect the change
                    if (externalVisitor.InBuilding == false)
                    {
                        externalVisitorListItem.InBuilding = false;
                        externalVisitorListItem.ExitDateTime = externalVisitor.ExitDateTime;
                    }
                }
            }
        }
    }
}

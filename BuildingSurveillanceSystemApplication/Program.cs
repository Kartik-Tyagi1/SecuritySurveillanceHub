using System;

namespace BuildingSurveillanceSystemApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            // Provider or Observable
            SecuritySurveillanceHub securitySurveillanceHub = new SecuritySurveillanceHub();
            
            // Subscribers or Observers
            EmployeeNotify employeeNotify = new EmployeeNotify(new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                JobTitle = "Development Manager"
            });
            EmployeeNotify employeeNotify2 = new EmployeeNotify(new Employee
            {
                Id = 2,
                FirstName = "Dave",
                LastName = "Kendal",
                JobTitle = "Chief Information Officer"
            });

            // Subscriber or Observer
            SecurityTeamNotify securityTeamNotify = new SecurityTeamNotify();

            // Subscribe to Obersevable 
            employeeNotify.Subscribe(securitySurveillanceHub);
            employeeNotify2.Subscribe(securitySurveillanceHub);
            securityTeamNotify.Subscribe(securitySurveillanceHub);

            // These next function will call the OnNext function and send notification to all observers
            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(
                1,
                "Andrew",
                "Jackson",
                "Company",
                "Contractor",
                DateTime.Parse("12 May 2020 11:00"),
                1
            );

            securitySurveillanceHub.ConfirmExternalVisitorEntersBuilding(
                2,
                "Jamie",
                "Pringle",
                "CoolerComp",
                "Business Woman",
                DateTime.Parse("12 May 2020 12:00"),
                2
            );

            // If an employee (observer) unsubscribes then they will not recieve any further notifications
            // In this case the employee will not see the visitor chart at the end
            // employeeNotify.UnSubscribe(securitySurveillanceHub)

            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(
                1,
                DateTime.Parse("12 May 2020 13:00")
            );

            securitySurveillanceHub.ConfirmExternalVisitorExitsBuilding(
                2,
                DateTime.Parse("12 May 2020 15:00")
            );

            // This will call onComplete and finish sending notification to observers and they will unsubscribe
            securitySurveillanceHub.BuildingEntryCutOffTimeReached();

            Console.ReadKey();
        }
    }
}

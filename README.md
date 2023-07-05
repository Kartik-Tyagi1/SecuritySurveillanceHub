# Security Surveillance Hub
This application was created as part of the Advanced C# Tutorial Series created by Gavin Lon on youtube as means to learn about the Observer Design Pattern in C#.

# Observer Design Pattern
The observer design patter is like a push notification system. There is a central hub that contains information (known as the provider or the observable) and there are things that subscribe to the central hub (which are known as subscribers or observers) 
to recieve notifications if the information in the hub changes. 

In this project there is a security surveillance hub that monitors visitors who want to visit and employee in a compnay builing and sends notifications to corresponding employee as well as the security team.
When a vistor exits the building then the employee and security team get notifications that the visitor has left the builiding as well. And at the end of the day, when all visitors have left, then the the 
employees who have had visitors and the security team get a overview of who visited.

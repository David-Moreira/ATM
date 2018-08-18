# ATM

My first MVC web project. 
I developed this project soon after I had learn't the Microsoft Stack from a Code Developer course I took. 
However MVC wasn't on the course's curriculum, and as a modern architectural pattern, I decided to experiment with it and Entity Framework.

# Functionalities
The application allows the user to register and be assigned a banking account. 
The user can then login and do ATM operations in a digital environment, like depositing, withdrawing, transferring funds, print statement

## As of 07/2018 - Goals

This was one of my first projects and at the moment there is no clear separation of concerns, the Data access layer and Business logic are all mixed on the interface layer,
I will make some architectural improvements here.
I will build on this and add some new functionalities, a transaction history and a logging system. 

## 17/07/2018

Made some nice architectural changes. Separation of concerns // Dependency Injection with Ninject.
Will update design, overall feel and improve the business logic next.

## 18/07/2018 

Updated design.

## 31/07/2018

Improved BL, Better validation and error handling in place instead of just throwing/catching exceptions. Improving operations and design... 

## 18/08/2018

Improvement to TransferFunds functionality.

##As of 18/08/2018 - ToDo

Review the validations and look for edge cases. 
Log errors into DB or txtFile.
EF first query is super slow! Figure out an efficient way to warm up EF.
Either make the app completly responsive to mobile or create a mobile specific interface & interaction.
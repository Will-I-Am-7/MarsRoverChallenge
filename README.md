# Platform 45 mars rover coding challenge

## Building and running

### Prerequisites

- .NET 5 runtime must be installed on the host machine in order to run the application   
<https://dotnet.microsoft.com/download/dotnet/5.0>

### Commands 

- Before attempting to build, run or test run the following command (**in the root of the project**):     
``dotnet restore ``

- To build:   
``dotnet build``

- To run tests:  
``dotnet test --verbosity normal``

- To run:  
``dotnet run --configuration Release --project ConsoleApp/ConsoleApp.csproj``

## Creative extras

> Interface to programme - Seeing that communication is made to mars I think it would be best to limit the amount  
of data that is transfered to and fro. A server running somewhere on mars or one of the spaceships with a simple  
CLI connecting to it from earth should suffice. I do realize that the issue gets vastly more complicated due to the  
great distances and satellites that will probably be in play.  

> Visualization of the conceptual model - To be able to have a view of the plateau before or after execution should  
be of great help. When all rovers are placed on the grid I display a small grid in the command line to give the user  
a better idea of where the rovers are actually located.  
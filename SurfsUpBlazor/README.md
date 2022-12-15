# Surfs Up - Blazor WebAssembly
## Setup
> Clone this repository       
> `git clone https://github.com/LovelessCodes/SurfsUp.git`    
> ### In Visual Studio    
> Open the solution in either or      
> `./SurfsUpBlazor/SurfsUpBlazor.sln`    
> `./SurfsUp-web/SurfsUp.sln`        
> ### In Terminal/Command Prompt    
> Change your working directory to the Server in the Web Assembly    
> `cd ./SurfsUpBlazor/SurfsUpBlazor/Server`   
> Run the dotnet server, either with or without watch    
> `dotnet watch` or `dotnet run`

## Configuration
> Change the connection string to whichever MySQL server you'd like to use    
> > In my experience the easiest to set up is a [SQLEXPRESS Server](https://www.microsoft.com/en-us/Download/details.aspx?id=101064)    
> Change the `OPENWEATHERAPI_KEY` inside the `Properties/launchSettings.json` file in the Server directory 
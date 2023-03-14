# The Insurwave code test

## Introduction

Let's imagine a ship operating company having vessels traveling across the globe. The operators located in one of the headquarters are responsible for communication with vessel captains during their journey.  
To make their work easier they need to understand what is the local time at the ports where vessels stay as well as local weather conditions.

With this exercise, you will build an ASP.NET Core Web Api allowing the system to provide that information and a React app as a user Interface for the created Api.

The produced Api solution should:

- build with `dotnet build` command,
- run with `dotnet run` command and start the local, console instance of the webapi,
- contain all the necessary tests (unit tests and integration tests) runnable with `dotnet test` command,
- have a documented api showing available operation and the requests/response models.

The produced UI solution should:

- build with `npm run build` command,
- run with `npm start` command,
- contain all the necessary tests runnable with `npm test` command.

During work on the solution, please use all the good practices that would be normally used during the development of the service and the UI.  
Please feel free to use any libraries, patterns or solution structure (including adding projects) that feels right for you.  

For obtaining the weather/time information, please integrate with https://www.weatherapi.com/ using a free account.

For the implementation, please follow the tickets written below.  
For every ticket, please prepare a separate commit/pull-request showing incremental work.  
Assuming every ticket is implemented and released independently, the changes should be implemented in a non-breaking manner.  

During work on UI solution please present usage of CSS (you can use a design library) and testing practices.  
While usage of React is preferred, using other frameworks is acceptable, as long as the solution is functional and presents the good engineering practices.  
Using typescript is highly appreciated.  

During the interview session, the produced solution will be demoed, reviewed and discussed.  
You will then implement an additional requirement provided by the interviewers.

## Ticket-1

As an operator,  
I want to know what is the local time, temperature as well as times of sunrise and sunset in the specified city,  
So that I can efficiently coordinate the vessel crew staying in the port.

Please design and create an endpoint returning the current weather conditions for the `city name` specified in the request, like: `Liverpool`, `Rotterdam` or `Busan, South Korea`.

The endpoint should use:

- http://api.weatherapi.com/v1/current.json for obtaining the details of city location, temperature and time,
- http://api.weatherapi.com/v1/astronomy.json for obtaining the sunrise and sunset details.

The endpoint response should include city name, region, country, local time, temperature in Celsius, sunrise and sunset times:

```
{
  "city": "Rotterdam",
  "region": "South Holland",
  "country": "Netherlands",
  "localTime": "2022-04-12 11:33",
  "temperature": 15.0,
  "sunrise": "06:52 AM",
  "sunset": "08:34 PM"
}
```

## Ticket-2

As a operator,  
I want to see the current weather details for the typed in city name,  
So that I can better handle the various vessel crews.

As a part of this ticket, please create a React page which allows to display the weather details for the typed in city.

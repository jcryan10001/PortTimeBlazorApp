Port Time- A Weather App

Refer to the Challange.txt for application context
************update********************

The controller endpoints of the Weather App are now implemented by an interface and a service, rather than directly by the controller. For example, the "GET /api/GetWeather/?cityName={cityName}" endpoint that returns the current weather information for a specified city is now implemented by the ICityController interface and the CityControllerService class.

*****use appsettings.json to set wether api key and also base url*****


Description--------------------------------------------------------------------------
Weather App is a web application that provides real-time weather data for different cities around the world. The app uses the OpenWeather API to fetch weather data and displays it in an easy-to-use interface.

Features-----------------------------------------------------------------------

Users can select a city from a dropdown menu to view its weather information.
The app displays the current temperature, sunrise and sunset times, and weather conditions.
Users can view a 7-day weather forecast for the selected city.
The app also provides a list of cities that users can select from.
Backend API
The backend API for the Weather App provides the following functionality:

"GET /api/GetWeather/?cityName={cityName}": This endpoint returns the current weather information for the specified city.

Request Parameters:
cityName: The name of the city for which to retrieve weather information.
Response Model:
CityWeather: A model containing the current weather information for the specified city. It contains the following properties:
City: a string property that represents the name of the city.
Region: a string property that represents the region where the city is located.
Country: a string property that represents the name of the country where the city is located.
LocalTime: a DateTime property that represents the local time in the city.
Temperature: a decimal property that represents the current temperature in the city.
Sunrise: a DateTime property that represents the time of sunrise in the city.
Sunset: a DateTime property that represents the time of sunset in the city.
FlatData: a nested model that contains additional weather data for the city.
"GET /api/GetweatherData/?cityName={cityName}": This endpoint returns the 7-day weather forecast for the specified city.

Request Parameters:
cityName: The name of the city for which to retrieve the weather forecast.
Response Model:
List<WeatherForecast>: A list of models containing the weather forecast for the specified city. Each model contains the following properties:
DayOfWeek: a string property that represents the day of the week for the forecast.
MinTempC: an integer property that represents the minimum temperature in Celsius for the forecast.
MaxTempC: an integer property that represents the maximum temperature in Celsius for the forecast.
ConditionText: a string property that represents the weather condition text for the forecast.
ImageUrl: a string property that represents the URL of an image that depicts the weather condition for the forecast.
"GET /api/GetCityData": This endpoint returns a list of all cities available in the database for the autocomplete feature to work.

Response Model:
List<City>: A list of models containing the name, region, country, and flag URL for each city.
The City class is a model that represents a city and contains the following properties:

Name: a string property that represents the name of the city.
ISO: a string property that represents the International Organization for Standardization (ISO) code of the city.
Country: a string property that represents the name of the country where the city is located.
FlagUrl: a string property that represents the URL of the flag of the country where the city is located.
This model can be used to store and retrieve information about cities, and can be used in


Front end side of things----------------------------------------------------------------------------

Frontend
The Weather Dashboard is a Blazor server application that provides a weather dashboard with an autocomplete input search field. The application incorporates Syncfusion components for the dashboard display, including movable and resizable panels.

Getting Started
To run the application, .NET Core 6.0 or higher must be installed on your machine. Clone the repository and run the following command in the project directory:

dotnet run

The application should start running on http://localhost:7143. Run the following command to test the application:

dotnet build: This command builds the .NET project and generates the output files in a specified directory. It compiles the code and creates a binary executable or library.

dotnet run test: This command runs all the unit tests in the .NET project and reports the results to the console.

dotnet test: This command runs all the unit tests in the .NET project and generates a test report in a specified format.

npm build: This command compiles the source code of a JavaScript project and generates the output files in a specified directory. It transpiles, bundles, and optimizes the code for production use.

npm run: This command runs a script defined in the package.json file of a JavaScript project.

npm test: This command runs all the tests defined in the package.json file of a JavaScript project and reports the results to the console. It uses a test runner such as Jest or Mocha to execute the tests.

Testing----
GetWeather_Returns_Success: Verifies that the GetWeather method of the CityController class returns a successful response when a valid city name is provided.
GetCityDataAsync_Returns_Success: Verifies that the GetCityData method of the CityController class returns a successful response and a list of cities.
ParseWeatherForecast_Returns_Success: Verifies that the GetweatherData method of the CityController class returns a successful response and a list of weather forecasts for a given city.
GetWeather_Returns_NotFound_For_Invalid_CityName: Verifies that the GetWeather method of the CityController class returns a NotFound response when an invalid city name is provided.
GetWeather_ReturnsCityWeather: Verifies that the GetWeather method of the CityController class returns a CityWeather object when a valid city name is provided.
GetCityData_ReturnsListOfCities: Verifies that the GetCityData method of the CityController class returns a list of cities.
GetweatherData_ReturnsWeatherForecast: Verifies that the GetweatherData method of the CityController class returns a list of weather forecasts for a given city, with a count of 7.

UiTesting----
The UITests class contains a single test that validates the functionality of the search feature on the UI of the application. Here's a summary of the test:

SearchForCity_ReturnsSearchResults: This test navigates to the application's home page, enters "New York" into the search input, clicks the search button, and verifies that the city header is displayed on the resulting page.
This test verifies that the search feature on the UI works as intended and helps catch any potential issues or bugs that may arise when interacting with the UI. It also provides a good basis for ensuring that the UI of the application functions as intended and is user-friendly.

API documentation

The Weather App backend API provides the following endpoints, implemented through the ICityController interface and the CityControllerService class:
----------------------------------------------------------------------------------------------------
GET /api/Parts/GetWeatherData/?cityName={cityName}

Returns the current weather information for the specified city.

Request Parameters:

cityName: string (required) - The name of the city for which to retrieve weather information.
Response Model:

CityWeather: A model containing the current weather information for the specified city. Contains the following properties:
City: string - The name of the city.
Region: string - The region where the city is located.
Country: string - The name of the country where the city is located.
LocalTime: DateTime - The local time in the city.
Temperature: decimal - The current temperature in the city.
Sunrise: DateTime - The time of sunrise in the city.
Sunset: DateTime - The time of sunset in the city.
FlatData: A nested model that contains additional weather data for the city.

----------------------------------------------------------------------------------------------------
GET /api/Parts/GetweatherData/?cityName={cityName}

Returns the 7-day weather forecast for the specified city.

Request Parameters:

cityName: string (required) - The name of the city for which to retrieve the weather forecast.
Response Model:

List<WeatherForecast>: A list of models containing the weather forecast for the specified city. Each model contains the following properties:
DayOfWeek: string - The day of the week for the forecast.
MinTempC: int - The minimum temperature in Celsius for the forecast.
MaxTempC: int - The maximum temperature in Celsius for the forecast.
ConditionText: string - The weather condition text for the forecast.
ImageUrl: string - The URL of an image that depicts the weather condition for the forecast.

----------------------------------------------------------------------------------------------------
GET /api/Parts/GetCityData

Returns a list of all cities available in the database for the autocomplete feature to work.

Response Model:

List<City>: A list of models containing the name, region, country, and flag URL for each city. Each model contains the following properties:
Name: string - The name of the city.
ISO: string - The International Organization for Standardization (ISO) code of the city.
Country: string - The name of the country where the city is located.
FlagUrl: string - The URL of the flag of the country where the city is located.
Note: All responses are returned in JSON format.Usage
When you run the application, you'll be presented with a weather dashboard. To search for a city, start typing its name in the autocomplete input search field. When you select a city from the dropdown list, the application will display the weather details for the selected city.

The dashboard is composed of movable and resizable panels that display different weather details. The resize feature is turned off byt can be turned on in grid settings: (AllowResize="true"). You can move a panel by clicking and dragging its title bar, and resize it by clicking and dragging its edges.

To display additional weather details, click the "More" button. This will display a tab containing additional weather information, including the region and country of the selected city, the local time, and the times of sunrise and sunset. To hide the additional information, click

Security------------------------------------------------------------------------------------------------
Currently, the application is using cookie authentication as the primary security mechanism. However, in the future, we plan to implement a more robust security solution that involves database authentication. This would allow users to securely log in to the application with their credentials and stay logged in for a set period of time (e.g. 2 hours) using cookie authentication. By connecting the application to a user database, we can ensure that only authorized users have access to sensitive information and actions within the application. Additionally, we can track user activity and implement more advanced security features like multi-factor authentication, password reset functionality, and role-based access control.

UE-----------------------------------------------------------------------------------------------

The user experience is simple and intuitive, and error handling is done where needed to display relevant information about the application. The README file contains a documented API showing available operations and the request/response models.

Future plans-------------------------------------------------------------------------------------
In the future or given more time, there are several ways to enhance the dashboard and make it more dynamic and responsive. One idea is to add animated backgrounds for tiles and incorporate temperature or unit conversion for various items to make the application more user-friendly. Another approach is to include an OpenWeather API live weather map, which would allow users to switch to a map view that provides precise visualizations of wind direction, speed, precipitation, global current, and other dynamic data.

There are two ways to achieve this. The first method involves using an embedded map from OpenWeather, while the second method entails creating a custom map using Syncfusion's map component and integrating weather data on top layers. Both approaches require additional development and testing, but would ultimately add value and enhance the overall user experience of the application.

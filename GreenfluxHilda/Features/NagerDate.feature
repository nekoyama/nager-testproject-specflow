Feature: Nager Date API 
	In the API
	As a Tester
	I can retrieve the public holidays for a specific Country 

@testAssigment
#---------------------------------------------------------------------------------------------------
Scenario: Public Holiday is correct for future & past years

	Given the Country Code '<CountryCode>' and the Holiday '<ExpectedHolidayName>' 
	When I send the request 
	Then the expected date is correct for '<NumberOfPreviousYears>' years in the past and for '<NumberOfFutureYears>' years in the future
	#here I avoided to add extra steps that don't make sense in the scenario if my scope is integration
	#E.g. Request is 200 OK should be a Given for my test scope 
	 
	Examples:
	| NumberOfFutureYears | NumberOfPreviousYears | CountryCode | ExpectedHolidayName |
	| 5                   | 10                    | NL          | New Year's Day      |
	| 1                   | 1                     | MX          | New Year's Day      |

	#It is possible to extend for different Country codes, the number of years and the dates
	#I am assuming that the API is sending a correct date 01-01 for New Year's
	#Basing my test in the Date Name
	#I can add an extra step verifying the 01-01 in the object but depends if it is cover in unit tests

#---------------------------------------------------------------------------------------------------
Scenario: Public Holiday is on a given WeekDay

	Given the Country Code '<CountryCode>' and the Holiday '<ExpectedHolidayName>'
	When I send the request
	Then the WeekDay should be '<Weekday>'
	Examples:
	| ExpectedHolidayName | ExpectedLocalHolidayName | Weekday  | CountryCode |
	| Ascension Day       | Hemelvaartsdag           | Thursday | NL          |
	| Good Friday         | Goede Vrijdag            | Friday   | NL          |
	| COVID19             | Goede Vrijdag            | Friday   | NL          |
 
	#It is possible to extend for different Country codes and the dates
	#I am assuming that the API is sending the correct Holiday match for Local Names
	#Here it is easier to use the Name and not yet the Local Name, however I would add it as an extra scenario with a matching table


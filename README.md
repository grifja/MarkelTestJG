# MarkelGriffin

Test API submission with the following criteria:

## Background 

We are creating a new front end to an existing SQL Server database, and need a restful DotNetCore API to be created to allow us to access the Claims and Company data. 
For the purpose of this exercise, the data can be generated in code rather than coming from SQL server. 

While this is a test exercise, the level of detail and quality should represent something that is fit for production. 

## Requirements 

- The output must be in json format 
- We need an endpoint that will give me a single company. We need a property to be returned that will tell us if the company has an active insurance policy 
- We need an endpoint that will give me a list of claims for one company 
- We need an endpoint that will give me the details of one claim. We need a property to be returned that tells us how old the claim is in days 
- We need an endpoint that will allow us to update a claim 
- We need at least one unit test to be created 

 
## Database Structure 
~~~~sql  
CREATE TABLE Claims
(
  UCR VARCHAR(20),
  CompanyId INT, 
  ClaimDate DATETIME, 
  LossDate DATETIME, 
  [Assured Name] VARCHAR(100), 
  [Incurred Loss] DECIMAL(15,2), 
  Closed BIT 
)
~~~~
~~~~sql
CREATE TABLE ClaimType 
( 
  Id INT, 
  Name VARCHAR(20) 
) 
~~~~
~~~~sql
CREATE TABLE Company 
( 
  Id INT, 
  Name VARCHAR(200), 
  Address1 VARCHAR(100), 
  Address2 VARCHAR(100), 
  Address3 VARCHAR(100), 
  Postcode VARCHAR(20), 
  Country VARCHAR(50), 
  Active BIT, 
  InsuranceEndDate DATETIME 
)
~~~~

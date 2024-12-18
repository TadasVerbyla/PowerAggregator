# PowerAggregator
 
## Requirement considerations
The task seemed highly vague, possibly intentionally. Thus, before starting implementation, I defined these interpretative clarifications for how exactly the API should work:
- As the data sources are monthly data reports, a database is required for the implementation and aggregation is necessary, I will assume that the database will receive new data with each new CSV report. 
- The data in CSV is made up of many regular reports on power usage by specific buildings in specific regions. The stated goal is to "Store data into a database grouped by Tinklas (Regionas) field;", thus I will assume that the aggregation requires me to consolidate the data of both individual apartments and regions into a singular entry for each region, desciribing its total power consumption and production for the given month. 
- The required endpoint should "retrieve aggregated data". I will attempt to implement 3 endpoints that would retrieve all data, all from certain region and all from certain month. 
- Since only the GET endpoint is needed and this is a mock-project, the CSV file URL's will be hardcoded, the database will be in-memory (no persistance) and it will be populated on startup each time application is run. 
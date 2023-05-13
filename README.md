# Rectangle

Overview
Web based API is needed to perform a search/match of each coordinate passed as input based on established sets of rectangles.

Feel free to ask as many or as few questions about the requirements as you feel are necessary.

Any code should maintain best practices, and have the ability to scale to meet load.

Please push the code up into an accessible repository of your choice.

Before moving on to Bonus Point items, please make sure the Basic Requirements are completed to your satisfaction.
Basic Requirements
Your application must:
Contain data seed methods to populate 200 rectangle data entries into database
Each data entry (and therefore row(s)) must define rectangle coordinates and/or dimensions, upon your choice of design
Define an endpoint that takes an array of coordinates upon your choice of design of input object
For each passed coordinate, output a list of rectangles defined in database that this coordinate falls into
Bonus Points
Implement automated tests using tools of your choice.
Database makes use of indexes and foreign keys.
Implement basic authentication of the caller of API.
Any further design considerations assuming scaling this system and integrations with external consumers.
Wrap the executable(s) into Docker container.

To run the application without Docker:
To use the database, you need to launch Docker with the following command:

docker run --name my-mariadb -p 3306:3306 -e MYSQL_ROOT_PASSWORD=pass -d mariadb
This command will start a MariaDB instance with the necessary configuration.

To launch the application using Docker Compose:
In the root folder of the project, there should be a docker-compose.yml file. This file contains instructions for Docker on how to run your application and all its associated services. To launch the application, simply run the following command in the terminal at the root directory of your project:

docker-compose up

This command will start all the services defined in your docker-compose.yml file.

There is no complex logic in this project, and EF Core already implements the Repository and Unit of Work pattern, so adding an additional repository layer can lead to redundant code and complicate the application architecture.

Proposals for improving the system.

NetTopologySuite: This is a .NET library that provides APIs for dealing with spatial data and performing geometric operations. NetTopologySuite is compatible with Entity Framework Core and can be used to store and retrieve spatial data from a database.

Spatial Data: This is a generic term for data describing geometric shapes and their location in space. Entity Framework Core supports spatial data types that allow working with geometry and geography in your data models.

PostGIS: As mentioned before, PostGIS is an extension for PostgreSQL that adds support for geographic objects, allowing spatial and geometric analysis to be performed.

Database Partitioning: This is a technique where you divide your database into smaller, more manageable parts (partitions). This can improve performance by allowing the database to query only a subset of the data.

Database Sharding: This involves splitting your data across multiple databases or servers. Each shard contains a subset of your total data, and queries are distributed across the shards. This can significantly improve performance and scalability for large databases or high-traffic applications.

Load Balancing: You can use a load balancer to distribute traffic across multiple database servers. This can help prevent any single server from becoming a bottleneck and can improve the overall performance and availability of your application.

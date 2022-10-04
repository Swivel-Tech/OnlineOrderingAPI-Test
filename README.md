# Simple Web API Demo Project

Made with .NET 6.0

### Instructions to run
1. Clone the project and open it using Visual Studio 2022
2. Run the project, then the swagger documentation will be displayed.
3. Obtain an authorization token by executing `/api/Token` endpoint.
4. Copy the `access_token` from the response and Authorize the Swagger UI.
5. After that you will be able to execute all the other endpoints.

### Functionalities

##### Customers
1. Users should be able to create, update and delete customers.
2. Users should be able to retrieve all customer records.
3. User should be able to retrieve a specific customer with customer id.

##### Products
1. Users should be able to create, update and delete products.
2. Users should be able to retrieve all product records.
3. User should be able to retrieve a specific product with product id.

##### Orders
1. Users should be able to create and update orders.
2. When creating an order if customer does not exist a new customer record should be added.
3. Users should be able to list all order records.
4. Users should be able to retrieve a specific order record with order id.
5. Users should be able to retrieve orders with customer id.
6. Users should be able to retrieve orders filtered from order status.
# Stach API
An E-commerce API built in Onion Architecture based on the following Design Patterns:
<blockquote>
 
- Repository Design Pattern.
- Specification Design Pattern.
- UnitOfWork Design Pattern.

</blockquote>

## Project Overview

A scalable E-commerce API. It utilizes Entity Framework Core for interacting with a Microsoft SQL Server database using the 
Code-First approach adhering to Onion Architecture based on Repository, Specification, and UnitOfWork Design Pattern ensures clean separation of 
concerns and promotes maintainability. Implements functionalities including user authentication, product browsing and filtering, basket management, order creation 
with payment processing, and delivery method selection handled with all error types and validations (400-401-404-500).


## Packages Used

This project utilizes the following packages:
 
- StackExchange.Redis.
- Microsoft.EntityFrameworkCore.Tools.
- Microsoft.EntityFrameworkCore.SqlServer.
- Microsoft.AspNetCore.Identity.EntityFrameworkCore.
- AutoMapper.Extensions.Microsoft.DependencyInjection. 
- Microsoft.AspNetCore.Authentication.JwtBearer.
- Stripe.net.


## Databases

This project migrated to Entity Framework Core, using SQL Server as the RDBMS and Redis as an in-memory database.

 - Stach
 - Stach.Identity
 - Redis


## API Documnetation

Here is a summary of the project's API endpoints:

#### Products
1. Get Product By Id.
    `GET /api/Products/{id}`
2. Get All Products Brands.
    `GET /api/Products/Brands` 
3. Get All Products Categories.
    `GET /api/Products/Categories`
4. Get All Products.
    Sort the products by price or name descending or ascending  order.
    Id of Category to fetch.
    Id of Brand to fetch.
    Determine the page size (Pagination).
    Get specific page index.
    Search for products by Name.
    `GET /api/Products?sort=${price}&CategoryId=${TypeId}&BrandId=${BrandId}&PageSize=${PageSize}&PageIndex=${PageIndex}&search=${Name}`

#### Basket
1. <p>Get Basket By Id.</p>
    `GET /api/Basket?Id=${id}`
2. Create OR Update Basket.
    `POST /api/Basket`
    ```json
    {
      "id": "basket1",
      "items": [
        {
          "id": 1,
          "productName": "Starbucks Mug",
          "price": 1,
          "quantity": 1,
          "pictureUrl": "https://localhost:5001/images/products/2.png",
          "brand": "Starbucks",
          "category": "Mug"
        }
      ]
    } 
   ```
3. Delete Basket By Id.
    `DELETE /api/Basket?Id=${id}`

#### Identity
1. Login as a User.
    `POST /api/Account/login`
    ```json
    {
        "Email": "ziad@gmail.com",
        "Password": "Ziad123"
    }
    ```
2. Register a User.
    `POST /api/Account/register`
    ```json
    {
        "DisplayName":"Ziad",
        "Email": "ziad@gmail.com",
        "Password": "Ziad123",
        "PhoneNumber": "01231231230"
    }
    ```
3. Get Current User.
    `GET /api/Account/GetCurrentUser`
4. Get Current User Address.
    `DELETE /api/Categories/{id}`
5. Update Current User Address.
    `PUT /api/Account/UpdateCurrentUserAddress`
    ```json
    {
        "FirstName": "Ziad",
        "LastName": "Saleh",
        "street": "Random St.",
        "city": "Cairo",
        "country": "Egypt"
    }
    ```

#### Orders
1. Get All Avaliable Delivery Methods.
    `GET /api/Orders/DeliveryMethods`
2. Get All Orders.
    `GET /api/Orders`
3. Get Order By ID.
    `GET /api/Orders/{id}`
4. Create an Order
    `POST /api/Orders`
    ```json
    {
        "basketId": "basket1",
        "deliveryMethodId": 1,
        "shippingaddress": {
            "FirstName": "Ziad",
            "LastName": "Saleh",
            "street": "Random St.",
            "city": "Cairo",
            "country": "Egypt"
        }
    }
    ```

#### Payments
1. Create Or Update PaymentIntent.
    `POST /api/Payments?basketId=${basketId}`
<!-- 2. Confirm Payment.
    `POST /Payments/webhook` -->


## Responses

If an invalid request is submitted, or an error occured, it returns a JSON response in the following format:

```javascript
{
  "message" : string,
  "statusCode" : int,
  "Details"    : string
}
```

The `message` attribute contains a message commonly used to indicate errors.

The `statusCode` attribute describes the code of the response.

The `Details` attribute contains error message only in developing env and in case of internal server error(500).


## Getting Started

To get started with this updated project, follow these steps:

1. **Install .NET Core 6**: If you haven't already installed .NET Core 6, you can download and install it from the official website:
   - [Download .NET](https://dotnet.microsoft.com/download)
2. Clone the repository to your local development environment.
3. Open the Package Manager Console in Visual Studio (or use the command-line equivalent) and run the following command to create the database tables based on the migrations:
   ```Shell
   Update-Database
4. Test endpoints on Postman or use the Swagger Documentation.

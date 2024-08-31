# SAOnlineMart

SAOnlineMart is an online shopping platform that promotes local South African products by providing a seamless online shopping experience. This project involves both front-end and back-end development, with functionalities for product browsing, cart management, checkout, and administrative product management.

## Features

- **User Authentication**: Registration, login, and logout.
- **Product Management**: Browse and view products.
- **Shopping Cart**: Add, update, and remove items in the cart.
- **Checkout**: Complete purchase with shipping and payment details.
- **Admin Panel**: Manage products, including creation, editing, and deletion.

## Technologies

- **Backend**: ASP.NET Core
- **Frontend**: ASP.NET MVC
- **Database**: SQL Server
- **Logging**: Microsoft.Extensions.Logging

## Installation

##Navigate to the Project Directory:

bash
Copy code
cd SAOnlineMart
Set Up the Database:

Open SQL Server Management Studio (SSMS).
Execute the SQL scripts in Database/Setup.sql to create and populate the database.
Configure the Application:

Update the appsettings.json file with your database connection string.
Run the Application:

##Use the following command to start the application:

bash
Copy code
dotnet run
Open your web browser and navigate to https://localhost:7077.

##Usage
Register a new account or log in to an existing one.
Browse Products and add them to your shopping cart.
View Cart to modify quantities or remove items.
Checkout to complete your purchase.
Admin Panel: Manage products through the admin interface.
Contributing
Contributions are welcome! Please follow these steps:

##Fork the repository.
Create a new branch (git checkout -b feature/your-feature).
Commit your changes (git commit -am 'Add new feature').
Push to the branch (git push origin feature/your-feature).
Create a new Pull Request.
License
This project is licensed under the MIT License - see the LICENSE file for details.

##Acknowledgements
ASP.NET Core for building the backend.
SQL Server for database management.
Microsoft.Extensions.Logging for logging.

# WebShop made for Bolta by K. Wittenberg

Hi and welcome to my webshop app!

1. Open or download the Code in VisualStudio

2. Go to appsettings.json - change Server name, optional change Database name.

3. Go to Package Manager Console and update-database.

4. Run the app.

5. You have have two already entered users:

- Admin:   u: admin@gmail.com   p:admin@gmail.com
- User:   u: user@gmail.com   p: user@gmail.com




### For API use Swagger ###
https://localhost:44367/swagger/index.html



### ShopAPI ###
- GET All Products: https://localhost:44367/api/ShopAPI/products

### UserAPI ###
- POST Register: https://localhost:44367/api/userapi/register
{
  "email": "string",
  "password": "string"
}

- POST Token/Login: https://localhost:44367/api/userapi/token
{
  "userName": "string",
  "password": "string"
}

### AdminAPI ###
- Postman script:
var Username = 'admin@admin@gmail.com';
var Password = 'admin@admin@gmail.com';

pm.sendRequest({
    url: "https://localhost:44367/api/userapi/token",
    method: 'POST',
    header: {
        'Content-Type': 'application/json',
    },
    body: {
        mode: 'raw',
        raw: JSON.stringify({
            UserName: Username,
            Password: Password
        }),
        options: {
            raw: {
                language: 'json'
            }
        }
    }
}, function (err, res) {

    console.log(res.json().token);
    pm.globals.set("token", res.json().token);
})

- GET Product Category: https://localhost:44367/api/AdminApi/product-categorys
{
    "title": "string",
    "description": "string",
    "id": 0
 }
 
 - GET Product Id: https://localhost:44367/api/AdminApi/product/1
 {
  "title": "string",
  "description": "string",
  "shortDescription": "string",
  "author": "string",
  "image": "string",
  "available": true,
  "quantity": 0,
  "price": 0,
  "discount": true,
  "discountPrice": 0,
  "startDate": "2022-08-03T10:21:28.545Z",
  "endDate": "2022-08-03T10:21:28.545Z",
  "yearOfPublication": 0,
  "publisher": "string",
  "isbn": "string",
  "bookCategory": 1,
  "bookBinding": 1,
  "numberOfPages": 0,
  "width": 0,
  "height": 0,
  "thickness": 0,
  "weight": 0,
  "id": 0,
  "created": "2022-08-03T10:21:28.545Z",
  "productCategory": {
    "title": "string",
    "description": "string",
    "id": 0
  },
  "productCategoryId": 0
}

- POST Product: https://localhost:44367/api/AdminApi/product
- PUT Product: https://localhost:44367/api/AdminApi/product
- EDIT Product: https://localhost:44367/api/AdminApi/product
- DELETE Product: https://localhost:44367/api/AdminApi/product

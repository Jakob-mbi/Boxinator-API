# Boxinator-API

A REST API for Boxinator with full CRUD functionality built with ASP.NET and Entity Framework. The API uses Swagger for it's documentation, so please refer to that for a in depth explanation of every endpoint. Otherwise you can use the list of endpoints below as a quick reference.

## Install

   To use the API locally on your machine.  
   
   Then go ahead and clone the repository using the following command:
   git clone https://github.com/Jakob-mbi/Boxinator-API.git
   
   
   The application is contenerised with docker
   
   docker build -f "Boxinator-API\Dockerfile" -t [imageName]:[tag} .

## Run the app

    docker run [OPTIONS] IMAGE[:TAG] [COMMAND] [ARGS]
    

# REST API

Ther is four main controllers Countries, ShipmentAdmin, ShipmentUser and Users
The REST API to the example app is described below.

## Countries

### Get list of Countries

#### Request

`GET /api/v1/countries`

    curl -i -H 'Accept: application/json' https://localhost:32770//api/v1/countries


### Get a specific countrie

#### Request

`Get /api/v1/countries/{id}`

    curl -i -H 'Accept: application/json' https://localhost:32770//api/v1/countries/{id}


### Change a countries's state

#### Request

`PUT /api/v1/countries/{id}´

    curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/countries/{id}
   

#### Request body

 {
  "id": 0,
  "name": "string",
  "multiplier": 0
}



## ShipmentAdmin

### Get list of current shipments

#### Request

`Get /api/v1/admin/shipment/current`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/current


### Get list of cancelled shipments

#### Request

`Get /api/v1/admin/shipment/cancelled`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/cancelled


### Get list of completed shipments

#### Request

`Get /api/v1/admin/shipment/completed`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/completed



### Get a specific shipment

#### Request

`Get /api/v1/admin/shipment/{id}`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/{id}



### Get list of shipments customer

#### Request

`Get /api/v1/admin/shipment/customer/{customerid}`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/customer/{customerid}



### Delete shipment

#### Request

`Delete /api/v1/admin/shipment/{id}/delete`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/{id}/delete
     
     
### Update shipment

#### Request

`PUT /api/v1/admin/shipment/{id}/update`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/{id}/update
     
 #### Request body

{
  "id": 0,
  "reciverName": "string",
  "weight": 100,
  "boxColor": "string",
  "destinationID": 0,
  "email": "string",
  "price": 1000
}

### Add status to shipment 

#### Request

`PUT /api/v1/admin/shipment/{shipmentid}/addstatus`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/{shipmentid}/addstatus
     
 #### Request body

{
  "id": 0,
  "statusName": "string"
}

### Remove status to shipment 

#### Request

`PUT /api/v1/admin/shipment/{shipmentid}/removestatus`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/admin/shipment/{shipmentid}/removestatus
     
 #### Request body

{
  "id": 0,
  "statusName": "string"
}




## ShipmentUser

### Get list of current shipments

#### Request

`Get /api/v1/shipment/current`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/current


### Get list of cancelled shipments

#### Request

`Get /api/v1/shipment/cancelled`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/cancelled


### Get list of completed shipments

#### Request

`Get /api/v1/shipment/completed`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/completed
     
     
### Get list of completed previous

#### Request

`Get /api/v1/shipment/previous`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/previous




### Get a specific shipment

#### Request

`Get /api/v1/shipment/{id}`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/{id}
     
     
### Post new shipment 

#### Request

`POST /api/v1/shipment/new`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/new
     
#### Request body

{
  "reciverName": "string",
  "weight": 0,
  "boxColor": "string",
  "destinationID": 0,
  "price": 0
}

### Post new shipment as guest

#### Request

`POST /api/v1/shipment/guest/new`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/guest/new
     
#### Request body

{
  "reciverName": "string",
  "weight": 0,
  "boxColor": "string",
  "destinationID": 0,
  "email": "string",
  "price": 0
}

### Cancel shipment as a user

#### Request

`PUT /api/v1/shipment/{shipmentid}/cancel`

     curl -i -H 'Accept: application/json' https://localhost:32770/api/v1/shipment/{shipmentid}/cancel
     
     
## Users

### Get List of Users

#### Request

`GET /api/users`

 curl -i -H 'Accept: application/json' https://localhost:32770/api/users
 

### Update user info

#### Request

`PUT /api/users`

 curl -i -H 'Accept: application/json' https://localhost:32770/api/users
 
 #### Request body
 {
  "dateOfBirth": "string",
  "zipCode": "string",
  "country": "string",
  "contactNumber": "string"
}

### Get a specific Users

#### Request

`GET /api/users/(sub}`

 curl -i -H 'Accept: application/json' https://localhost:32770/api/users/{sub}
 
 ### Delete a specific Users

#### Request

`Delete /api/users/(sub}`

 curl -i -H 'Accept: application/json' https://localhost:32770/api/users/{sub}
 
 
### Add new  Users

#### Request

`POST /api/users/newuser`

 curl -i -H 'Accept: application/json' https://localhost:32770/api/users/newuser
 
 #### Request body
 
 {
  "sub": "string",
  "dateOfBirth": "string",
  "zipCode": "string",
  "country": "string",
  "contactNumber": "string",
  "shipments": [
    "string"
  ]
}
 

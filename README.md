# SwaggerAds
# API Handlers for SwaggerAds API

## Introduction

This API provides endpoints to perform CRUD operations on ads.

## Base URL

The base URL for all endpoints is https://deletemee20230616111844.azurewebsites.net/swagger/index.html) .

## Retrieve All Ads

- Method: GET
- URL: `/`
- Permissions: Admin, User
- Description: Retrieves all ads from the database.
- Response Code: 200
- Response Body: List of all ads.

## Retrieve One Ad

- Method: GET
- URL: `/{id}`
- Permissions: Admin, User
- Description: Retrieves a specific ad by its ID.
- Query Parameters:
  - `id`: The ID of the ad to retrieve.
- Response Code: 200
- Response Body: The requested ad.

## Create Ad

- Method: POST
- URL: `/`
- Permissions: Admin
- Description: Creates a new ad.
- Request Body: The ad object to create.
- Response Code: 200
- Response Body: List of all ads.

## Update Ad

- Method: PUT
- URL: `/`
- Permissions: Admin
- Description: Updates an entire ad.
- Request Body: The updated ad object.
- Response Code: 200
- Response Body: List of all ads.

## Delete Ad

- Method: DELETE
- URL: `/{id}`
- Permissions: Admin
- Description: Deletes a specific ad by its ID.
- Query Parameters:
  - `id`: The ID of the ad to delete.
- Response Code: 200
- Response Body: List of all ads.

## Update Ad (Partial)

- Method: PATCH
- URL: `/{id}`
- Permissions: Admin
- Description: Updates specific properties of an ad.
- Query Parameters:
  - `id`: The ID of the ad to update.
- Request Body: The JSON patch document with the properties to update.
- Response Code: 200
- Response Body: List of all ads.

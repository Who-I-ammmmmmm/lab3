Feature: APITesting

  Background:
    Given the base API URL is "https://reqres.in/api"

  Scenario: Create a new user
    When a POST request is sent to "/users"
    Then the server returns a status code 201
    And the response body contains information about the new user
    And the user identifier is not empty

  Scenario: Read user information
    Given the base API URL GET is "https://reqres.in/api"
    When a GET request is sent to "/users/2"
    Then the server returns a status code for get 200

  Scenario: Update user information
    Given the base API URL PUT is "https://reqres.in/api"
    When a PUT request is sent to "/users/2"
    Then the server returns a status code for put 200

  Scenario: Delete a user
    Given the base API URL DEL is "https://reqres.in/api"
    When a DELETE request is sent to "/users/666"
    Then the server returns a status code for del 204
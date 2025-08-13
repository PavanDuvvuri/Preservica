Feature: NASA UI Sign Up
  In order to test the NASA UI
  As a user
  I want to be able to sign up

  Background:
	Given I am on the NASA UI sign up page

	Scenario: Sign up with valid credentials
	When I fill in the sign up form with valid First Name "Test", Last Name "User", Email "testuser@example.com"
	And I submit the sign up form
	Then I should see a captcha challenge or success message

	Scenario: Sign up with invalid credentials
	When I fill in the sign up form with invalid credentials	
	And I submit the sign up form
	Then I should see an error message indicating the issues with my submission
		| Field Name | Error Message           |
		| Email      | Enter an email address. |
		| First Name | Fill out this field.    |
		| Last Name  | Fill out this field.    |
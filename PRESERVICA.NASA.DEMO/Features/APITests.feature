Feature: NASA API Tests
  In order to test the NASA API
  As a developer
  I want to be able to perform API tests

  Scenario Outline: Validate Coronal Mass Ejection Get Request
    Given I have created a Coronal Mass Ejection Get Request for <TestScenario>
    When the request is sent to the NASA API
    Then no exceptions should be thrown
    And the response status code should be <StatusCode>
    And the response should contain <ResponseData> with <Message>

    Examples:
      | TestScenario      | StatusCode            | ResponseData | Message         |
      | ValidRequest      | OK                    | Success      | activityID      |
      | InvalidAPIKey     | Forbidden             | Failed       | API_KEY_INVALID |
      | InvalidDateFormat | InternalServerError   | NA           |                 |
      | IncorrectDate     | OK                    |              |                 |

  Scenario Outline: Validate Solar Flare Get Request
    Given I have created a Solar Flare Ejection Get Request for <TestScenario>
    When the request is sent to the NASA API
    Then no exceptions should be thrown
    And the response status code should be <StatusCode>
    And the response should contain <ResponseData> with <Message>

    Examples:
      | TestScenario      | StatusCode            | ResponseData | Message         |
      | ValidRequest      | OK                    | Success      | flrID           |
      | InvalidAPIKey     | Forbidden             | Failed       | API_KEY_INVALID |
      | InvalidDateFormat | InternalServerError   | NA           |                 |
      | IncorrectDate     | OK                    |              |                 |

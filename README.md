# Playwright .NET Solution

This solution uses [Microsoft Playwright](https://playwright.dev/dotnet/) for browser automation and end-to-end testing in .NET 8.  
It also includes API testing for NASA endpoints using Reqnroll (SpecFlow-compatible) and NUnit.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (required for Playwright installation)
- Supported browsers (installed via Playwright CLI)

## Getting Started

1. **Restore dependencies**
2. **Install Playwright browsers**

## Useful Commands

- Clone the repository: `git clone`
- Restore dependencies: `dotnet restore`
- Install Playwright: `dotnet tool install --global Microsoft.Playwright.CLI`
- Update Playwright: `dotnet tool update --global Microsoft.Playwright.CLI`
- Install browsers: `dotnet playwright install`
- Run Playwright codegen: `dotnet playwright codegen`
- Run all tests: `dotnet test`

## Project Structure

- `PRESERVICA.NASA.DEMO/Features/` - Contains Reqnroll feature files.
- `PRESERVICA.NASA.DEMO/StepDefinitions/` - Step definitions for API feature tests.
- `README.md` - This file.

## API Testing

- API tests are defined in `.feature` files under `PRESERVICA.NASA.DEMO/Features/APITests.feature`.

## UI Testing

- API tests are defined in `.feature` files under `PRESERVICA.NASA.DEMO/Features/SignUp.feature`.

## Resources

- [Playwright .NET Documentation](https://playwright.dev/dotnet/docs/intro)
- [Playwright API Reference](https://playwright.dev/dotnet/docs/api/class-playwright)
- [Reqnroll Documentation](https://www.reqnroll.net/docs/)
- [NUnit Documentation](https://docs.nunit.org/)

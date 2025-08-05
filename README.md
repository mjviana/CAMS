# 🚗 Car Auction Management System (CAMS)

A clean and extensible **Car Auction Management System** built with C# and .NET 8. This system manages various types of vehicles and their auctions, allowing operations such as creating vehicles, starting and closing auctions, and placing bids.

---

## 📚 Table of Contents

-   [🚗 Car Auction Management System (CAMS)](#-car-auction-management-system-cams)
    -   [📚 Table of Contents](#-table-of-contents)
    -   [📖 Context](#-context)
    -   [✅ Business Rules](#-business-rules)
        -   [✔️ Vehicle Management](#️-vehicle-management)
        -   [✔️ Auction Lifecycle](#️-auction-lifecycle)
        -   [✔️ Bidding Rules](#️-bidding-rules)
    -   [🧠 Architecture \& Design](#-architecture--design)
        -   [Design Patterns \& Principles Used:](#design-patterns--principles-used)
    -   [📦 Project Structure](#-project-structure)
    -   [🚀 Getting Started](#-getting-started)
        -   [✅ Requirements](#-requirements)
        -   [🔧 Run the Demo Console App](#-run-the-demo-console-app)
    -   [🧪 Tests](#-tests)
    -   [🔮 Future Improvements](#-future-improvements)
    -   [💭 Final Thoughts](#-final-thoughts)

---

## 📖 Context

This project is part of a technical challenge with the following requirements:

> **Problem Statement: Car Auction Management System**
>
> You are tasked with implementing a simple Car Auction Management System. The system should handle different types of vehicles: Sedans, SUVs, Hatchbacks and Trucks.
>
> Each of these types has different attributes:
>
> -   **Hatchback**: Number of doors, manufacturer, model, year, and starting bid.
> -   **Sedan**: Number of doors, manufacturer, model, year, and starting bid.
> -   **SUV**: Number of seats, manufacturer, model, year, and starting bid.
> -   **Truck**: Load capacity, manufacturer, model, year, and starting bid.
>
> The system should allow users to:
>
> -   Add vehicles to the auction inventory. Each vehicle has a type (Sedan, SUV, or Truck), a unique identifier, and respective attributes based on its type.
> -   Search for vehicles by type, manufacturer, model, or year. The search should return all available vehicles that match the search criteria.
> -   Start and close auctions for vehicles. Only one auction can be active for a vehicle at a time. Users should be able to place bids on the vehicle during an active auction.
>
> **Error Handling Requirements:**
>
> -   When adding a vehicle, ensure the unique identifier is not already in use by another vehicle in the inventory. Raise an appropriate error or exception if there's a duplicate identifier.
> -   When starting an auction, verify the vehicle exists in the inventory and is not already in an active auction. Raise an error if the vehicle does not exist or if it's already in an auction.
> -   When placing a bid, validate that the auction for the given vehicle is active and that the bid amount is greater than the current highest bid. Raise an error if the auction is not active or the bid amount is invalid.
> -   Handle other potential edge cases like invalid inputs, out-of-range values, or unexpected behavior.
>
> **Implementation Requirements:**
>
> -   Design a C# solution that uses object-oriented principles.
> -   Focus on clean structure and quality tests.
> -   No UI or database is required.
> -   Deliverables:
>     -   All necessary classes, interfaces, etc.
>     -   Unit tests for auction management operations.
>     -   A brief write-up explaining design decisions and assumptions.

---

## ✅ Business Rules

### ✔️ Vehicle Management

-   Vehicles can be created with type-specific properties.
-   Vehicles may have a **unique identifier**, which can either be system-generated (GUID) or externally supplied.
-   Vehicles can be searched by:
    -   Type
    -   Manufacturer
    -   Model
    -   Year
    -   Or any **combination of filters**

### ✔️ Auction Lifecycle

-   Auctions can be in the following states:
    -   **Created**
    -   **Started**
    -   **Closed**
    -   **Canceled**
-   A vehicle can only be part of an auction **if it has never been sold before**, meaning:
    -   It must **not already have a closed auction**, and
    -   It must **not currently be part of an active auction**.
-   Auctions must be associated with an existing vehicle.
-   **Closed or canceled** auctions cannot be reopened.

### ✔️ Bidding Rules

-   Bids can only be placed on **active** auctions.
-   The bid value must be **greater than the current highest bid**.
-   The system throws domain-specific exceptions for invalid operations (e.g. `DuplicateIdentifierException`, `AuctionDoesNotExistException`, etc).

---

## 🧠 Architecture & Design

This project follows a **Clean Architecture** approach with a clear separation of concerns between:

| Layer              | Responsibility                                                                                |
| ------------------ | --------------------------------------------------------------------------------------------- |
| **Domain**         | Contains core business logic and entities. No dependencies on other layers.                   |
| **Application**    | Implements use cases and business services. Depends on `Domain`. Interfaces are defined here. |
| **Infrastructure** | In-memory repository implementations to simulate data persistence.                            |
| **Presentation**   | Simple demo application to interact with the system via CLI for testing/validation.           |

![Clean Architecture Diagram](https://www.milanjovanovic.tech/blogs/mnw_004/clean_architecture.png?imwidth=3840)

### Design Patterns & Principles Used:

-   **Domain-Driven Design (DDD)**
-   **Dependency Injection (DI)**
-   **Factory Pattern** (for vehicle creation logic)
-   **Separation of Concerns**

---

## 📦 Project Structure

```
CAMS.Domain/           # Entities (e.g., Vehicle, Auction, Bid)
CAMS.Application/      # Services, DTOs, Validators, Interfaces
CAMS.Infrastructure/   # In-memory repository implementations
CAMS.Presentation/          # Demo CLI App (console-based interaction)
CAMS.Tests/            # Unit Tests using xUnit and Moq
```

---

## 🚀 Getting Started

### ✅ Requirements

-   [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### 🔧 Run the Demo Console App

```bash
cd CAMS.Presentation
dotnet run
```

You’ll see a basic CLI that uses dependency injection to create vehicles, start auctions, and place bids.

---

## 🧪 Tests

This solution includes **unit tests** written using `xUnit` and `Moq`, organized into two projects:

-   **CAMS.Application.UnitTests** — Tests for application layer services and logic.
-   **CAMS.Domain.UnitTests** — Tests for domain entities and business rules.

To run all tests:

```bash
dotnet test CAMS.Application.UnitTests
dotnet test CAMS.Domain.UnitTests
```

Test coverage includes:

-   Vehicle creation (valid and invalid)
-   Auction lifecycle (start, cancel, close)
-   Bid placement (valid and invalid)
-   Exception handling and edge cases

---

## 🔮 Future Improvements

Here are some enhancements that could be made:

-   [ ] Persistence using a real database
-   [ ] Domain events for auction updates - Command Query Responsibility Segregation (CQRS) - need to learn about this :D
-   [ ] UI layer with React or Blazor
-   [ ] Logging & metrics
-   [ ] REST API

---

## 💭 Final Thoughts

Working on this challenge has been a rewarding learning experience. Here are a few reflections from the journey:

-   🏁 **Understanding Auctions**  
    I’ve never personally participated in an auction, so I had to do some research to understand how they typically function. While I made efforts to implement the business logic correctly, I acknowledge that I may have misunderstood or missed certain real-world behaviors. Thankfully, many issues were caught and corrected through unit testing — though some subtle ones might have slipped through. 😊

-   🚗 **Vehicle Types Exploration**  
    Similarly, the specific vehicle types (like Hatchback, SUV, etc.) weren’t very familiar to me initially. I took the time to study their characteristics and used that understanding to define custom rules for their creation (e.g., expected number of doors or seats, reasonable year ranges, etc.). It was an interesting dive into automotive terminology!

-   🏗️ **Architecture Journey**  
    This project gave me the opportunity to work with a **Clean Architecture** structure — something I hadn't used before in this depth. I must admit I really enjoyed building the system and seeing it evolve.  
    That said, at times I felt unsure about whether I was over-engineering or following best practices, especially since Clean Architecture is a flexible concept with many possible interpretations. I tried to balance pragmatism with good separation of concerns.

-   📚 **Resources & Learning**  
    Throughout the development, I leaned on several helpful resources, including:

    -   [Microsoft Architecture Guide](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
    -   [Next-Level Clean Architecture Boilerplate (ISE Blog)](https://devblogs.microsoft.com/ise/next-level-clean-architecture-boilerplate/#summary)
    -   [Jason Taylor's Clean Architecture GitHub Repo](https://github.com/jasontaylordev/CleanArchitecture)

-   ✅ **Room for Improvement**  
    I’m aware there’s still work to be done — especially when it comes to improving test coverage and exploring more advanced scenarios. I look forward to iterating further and leveling up my approach.

Thanks for reading! 👋

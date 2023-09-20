# ICS Project - Time Tracking and Activity Management Application

![Project Logo](.\src\Project.App\Resources\Images\logo.png)

This repository contains the code and documentation for our ICS project, a comprehensive time tracking and activity management application. This project was successfully completed and received a full score.

## Project Overview

The ICS project aimed to create a user-friendly and extensible application for managing activities and tracking time spent on them, inspired by tools like Toggl Track and Kimai. The application allows users to:

- Manage user profiles with names and profile pictures.
- Create, edit, and view activities with start and end times, activity types, and descriptions.
- Create, edit, and view projects.
- Perform CRUD operations on all data entities.
- Filter activities by start and end dates.
- Filter activities by user-friendly date ranges (e.g., last week, last month, previous month, and year).
- Ensure that users can only perform one activity at a time to prevent overlaps.

## Team

- Matej Keznikl
- Marián Tarageľ
- Simona Jánošíková
- Klára Smoleňová
- Ela Fedorová

## Project Architecture

The project adheres to a layered architecture, separating classes into different projects, including:
- App: The presentation layer containing the MAUI frontend.
- BL (Business Logic): Contains the ViewModel logic.
- DAL (Data Access Layer): Manages data storage and interacts with the database.

## Data Storage

Data is persistently stored using SQL Server Express LocalDB, which is included as part of Visual Studio's Data storage and processing workload. Entity Framework Core is used as the ORM framework.

## Project Collaboration and Version Control

- Azure DevOps and Git were used for project collaboration and version control.
- Access to the project was provided to the instructors for evaluation.
- Pull requests and code reviews were utilized to maintain code quality.
- Automated builds and testing were set up using Azure Pipelines.

## Project Phases

### Phase 1 - Object-Oriented Design and Database
- Logical design of classes and relationships.
- Creation of Entity Framework Core DbContext and DbSet.
- Generation of an ER diagram from the code.
- Creation of wireframes for all views.

### Phase 2 - Repositories and Mapping
- Implementation of repositories and facades.
- Integration of Entity Framework Core with data models.
- Implementation of unit tests and integration tests.
- Refinement of code based on code reviews and Clean Code principles.

### Phase 3 - MAUI Frontend and Data Binding
- Development of ViewModels and Views.
- Data binding in XAML.
- Implementation of intuitive user interfaces.
- Code quality and validation.

## Project Completion and Defense

The project was successfully completed and defended, earning a full score. The team collaborated effectively using Azure DevOps and Git, ensuring code quality and adherence to best practices.

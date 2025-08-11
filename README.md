# Simama Hospital Management System API
A .NET 8 Web API backend for managing patients, doctors, prescriptions, drugs, and pharmacists.
Built with Clean Architecture principles, secure JWT-based authentication, and support for role-based access control.
This project is designed as a learning and production-ready template for hospital management systems.

## 📌 Features
- Authentication & Authorization

    - Register & login with JWT tokens

    - Role-based access control (Admin, Doctor, Pharmacist, Patient)

    - /api/account/me endpoint for retrieving logged-in user details (ID, Email, First Name, Last Name, Role)

- Prescription Management

    - Create, update, and fetch prescriptions

    - Nested details for Doctor, Patient, Drug, and optionally Pharmacist

- Drug Management

    - Add, update, list, and delete drugs

- Secure API

    - JWT authentication

    - Role-based endpoint restrictions

## 🛠 Tech Stack
- Language: C# (.NET 8)

- Framework: ASP.NET Core Web API

- Database: PostgreSQL

- ORM: Entity Framework Core

- Authentication: JWT Bearer

- Testing: xUnit (planned)

- Dependency Injection: Built-in .NET DI

- Contanerization: Docker/Docker-compose

## 📌 API Overview
| **Category**       | **Method** | **Endpoint**             | **Description**                                                                |
| ------------------ | ---------- | ------------------------ | ------------------------------------------------------------------------------ |
| **Authentication** | POST       | `/api/auth/register`     | Register a new user                                                            |
|                    | POST       | `/api/auth/login`        | Authenticate and get JWT token                                                 |
|                    | GET        | `/api/account/me`        | Get logged-in user details (ID, Email, First Name, Last Name, Role)            |
| **Prescriptions**  | GET        | `/api/prescription`      | Get all prescriptions with nested Doctor, Patient, Drug, Pharmacist (nullable) |
|                    | POST       | `/api/prescription`      | Create new prescription                                                        |
|                    | PUT        | `/api/prescription/{id}` | Update prescription                                                            |
|                    | DELETE     | `/api/prescription/{id}` | Delete prescription                                                            |
| **Drugs**          | GET        | `/api/drug`              | List all drugs                                                                 |
|                    | POST       | `/api/drug`              | Add new drug                                                                   |
|                    | PUT        | `/api/drug/{id}`         | Update drug                                                                    |
|                    | DELETE     | `/api/drug/{id}`         | Delete drug                                                                    |

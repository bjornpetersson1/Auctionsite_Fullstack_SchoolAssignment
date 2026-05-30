# Auction Site — Fullstack Application

  A fullstack auction platform built with React and ASP.NET Core Web API, backed by a SQL Server database. Users can register, log
  in, create auctions, and place bids. Admins can manage users and auctions.

  ## Tech Stack

  **Frontend**
  - React 19 with TypeScript
  - React Router v7
  - Context API for state management
  - Vite (build tool)

  **Backend**
  - ASP.NET Core Web API (.NET 10)
  - Entity Framework Core with SQL Server
  - JWT authentication
  - BCrypt password hashing
  - Swagger / OpenAPI

  ## Features

  - Register and log in with JWT-based authentication
  - Create, view, update, and search auctions
  - Place bids — must exceed the current highest bid
  - Auction owners cannot bid on their own auctions
  - Retract your latest bid on an open auction
  - View closed auctions with their winning bid (no bid history shown)
  - Update auction details — price cannot be changed once bids exist
  - Change your own password
  - Responsive design for both desktop and mobile
  - Admin panel: deactivate auctions or user accounts

  ## Getting Started

  ### Prerequisites

  - [.NET 10 SDK](https://dotnet.microsoft.com/download)
  - [Node.js 20+](https://nodejs.org/)
  - SQL Server (local or remote)

  ### Backend

  1. Navigate to the backend project:
     cd Auctionsite_Backend/Auctionsite_Backend/Auctionsite_Backend

  2. Configure your database connection string in `appsettings.json`

  4. Apply migrations and run:
  dotnet ef database update
  dotnet run

  The API will be available at https://localhost:7xxx and Swagger UI at /swagger.

  Frontend

  1. Navigate to the frontend project:
  cd Auctionsite_Frontend
  2. Install dependencies:
  npm install
  3. Start the dev server:
  npm run dev

  The app will be available at https://localhost:5173.

  Project Structure

  Auctionsite_Fullstack/
  ├── Auctionsite_Backend/   # ASP.NET Core Web API
  └── Auctionsite_Frontend/  # React + TypeScript client

# Auction Site — Fullstack Application

  A fullstack auction platform built with React and ASP.NET Core Web API, backed by a SQL Server database. Users can
  register, log in, create auctions, and place bids. Admins can manage users and auctions.

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

  ## Screenshots

  **Auctions**

  <img width="532" height="419" alt="Auktionslista"
  src="https://github.com/user-attachments/assets/2a2c26b3-2ada-408b-95dc-bc9a20bdd9ef" />
  <img width="525" height="428" alt="Auktionsdetaljer"
  src="https://github.com/user-attachments/assets/a757414c-8e5c-4382-8eea-1d91c1879a13" />
  <img width="530" height="197" alt="Sökfunktion"
  src="https://github.com/user-attachments/assets/216165a2-2240-413a-8c73-8ba098845ce3" />

  **Account**

  <img width="539" height="427" alt="Registrera"
  src="https://github.com/user-attachments/assets/9b4dc74a-410d-40b7-afdb-564bfa73d077" />
  <img width="538" height="409" alt="Logga in"
  src="https://github.com/user-attachments/assets/963ac77b-b2c1-4c01-9d79-53b9b629e56d" />
  <img width="537" height="428" alt="Skapa ny auktion"
  src="https://github.com/user-attachments/assets/c1c78808-7cd7-4e73-ae2e-80483f400d60" />
  <img width="538" height="416" alt="Profil"
  src="https://github.com/user-attachments/assets/575c4c02-6724-4775-9dee-3cd3b2f5dca0" />

  **Admin**

  <img width="583" height="422" alt="Adminauktioner"
  src="https://github.com/user-attachments/assets/48d958ed-4b4e-4c53-bb72-b0770d2d0f7a" />
  <img width="580" height="422" alt="Adminanvändare"
  src="https://github.com/user-attachments/assets/e1e9404d-a784-41bd-8241-9350270f5db5" />

  ## Getting Started

  ### Prerequisites

  - [.NET 10 SDK](https://dotnet.microsoft.com/download)
  - [Node.js 20+](https://nodejs.org/)
  - SQL Server (local or remote)

  ### Backend

  1. Navigate to the backend project:
     cd Auctionsite_Backend/Auctionsite_Backend/Auctionsite_Backend

  2. Configure your database connection string in `appsettings.json`

  3. Apply migrations and run:
     dotnet ef database update
     dotnet run

  The API will be available at `https://localhost:7xxx` and Swagger UI at `/swagger`.

  ### Frontend

  1. Navigate to the frontend project:
     cd Auctionsite_Frontend

  2. Install dependencies:
     npm install

  3. Start the dev server:
     npm run dev

  The app will be available at `https://localhost:5173`.

  ## Project Structure

  Auctionsite_Fullstack/
  ├── Auctionsite_Backend/    # ASP.NET Core Web API
  └── Auctionsite_Frontend/   # React + TypeScript client

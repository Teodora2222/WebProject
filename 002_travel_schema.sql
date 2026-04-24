USE TravelPlannerDB;

-- Tabela planova putovanja
CREATE TABLE TravelPlans (
    id INT IDENTITY(1,1) PRIMARY KEY,
    userId INT NOT NULL,
    title NVARCHAR(200) NOT NULL,
    description NVARCHAR(1000) NULL,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    budget DECIMAL(10,2) NOT NULL DEFAULT 0,
    notes NVARCHAR(2000) NULL,
    createdAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_TravelPlans_Users FOREIGN KEY (userId) REFERENCES Users(id) ON DELETE CASCADE,
    CONSTRAINT CHK_Dates CHECK (endDate >= startDate),
    CONSTRAINT CHK_Budget CHECK (budget >= 0)
);

-- Tabela destinacija
CREATE TABLE Destinations (
    id INT IDENTITY(1,1) PRIMARY KEY,
    travelPlanId INT NOT NULL,
    name NVARCHAR(200) NOT NULL,
    location NVARCHAR(300) NULL,
    arrivalDate DATE NULL,
    departureDate DATE NULL,
    description NVARCHAR(1000) NULL,
    CONSTRAINT FK_Destinations_TravelPlans FOREIGN KEY (travelPlanId) 
        REFERENCES TravelPlans(id) ON DELETE CASCADE
);

-- Tabela aktivnosti
CREATE TABLE Activities (
    id INT IDENTITY(1,1) PRIMARY KEY,
    travelPlanId INT NOT NULL,
    name NVARCHAR(200) NOT NULL,
    activityDate DATE NOT NULL,
    activityTime TIME NULL,
    location NVARCHAR(300) NULL,
    description NVARCHAR(1000) NULL,
    estimatedCost DECIMAL(10,2) DEFAULT 0,
    status NVARCHAR(20) DEFAULT 'planned',
    CONSTRAINT FK_Activities_TravelPlans FOREIGN KEY (travelPlanId) 
        REFERENCES TravelPlans(id) ON DELETE CASCADE,
    CONSTRAINT CHK_Status CHECK (status IN ('planned','reserved','completed','cancelled'))
);

-- Tabela checkliste
CREATE TABLE ChecklistItems (
    id INT IDENTITY(1,1) PRIMARY KEY,
    travelPlanId INT NOT NULL,
    name NVARCHAR(200) NOT NULL,
    isCompleted BIT DEFAULT 0,
    CONSTRAINT FK_Checklist_TravelPlans FOREIGN KEY (travelPlanId) 
        REFERENCES TravelPlans(id) ON DELETE CASCADE
);

-- Tabela dijeljenja plana
CREATE TABLE SharedPlans (
    id INT IDENTITY(1,1) PRIMARY KEY,
    travelPlanId INT NOT NULL,
    token NVARCHAR(500) NOT NULL UNIQUE,
    accessType NVARCHAR(10) NOT NULL DEFAULT 'view',
    createdAt DATETIME DEFAULT GETDATE(),
    expiresAt DATETIME NULL,
    CONSTRAINT FK_SharedPlans_TravelPlans FOREIGN KEY (travelPlanId) 
        REFERENCES TravelPlans(id) ON DELETE CASCADE,
    CONSTRAINT CHK_AccessType CHECK (accessType IN ('view','edit'))
);

-- Tabela troskova (ExpenseService)
CREATE TABLE Expenses (
    id INT IDENTITY(1,1) PRIMARY KEY,
    travelPlanId INT NOT NULL,
    name NVARCHAR(200) NOT NULL,
    category NVARCHAR(50) NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    expenseDate DATE NOT NULL,
    description NVARCHAR(500) NULL,
    CONSTRAINT FK_Expenses_TravelPlans FOREIGN KEY (travelPlanId) 
        REFERENCES TravelPlans(id) ON DELETE CASCADE,
    CONSTRAINT CHK_Amount CHECK (amount >= 0)
);
-- Drop the database if it exists and create a new one
DROP DATABASE IF EXISTS demo_bypass;
CREATE DATABASE demo_bypass;

-- Use the new database
USE demo_bypass;

-- Create the table
CREATE TABLE account (
    ID INT AUTO_INCREMENT NOT NULL,
    Name CHAR(50) NOT NULL,
    Password CHAR(10),
    Role CHAR(10),
    Email CHAR(50),
    IsConfirmEmail BOOLEAN,
    PRIMARY KEY (ID)
);

-- Insert data into the table
INSERT INTO account (Name, Password, Role, Email, IsConfirmEmail)
VALUES 
('AdminUser', 'admin123', 'Admin', 'admin@example.com', TRUE);

-- Insert a regular user
INSERT INTO account (Name, Password, Role, Email, IsConfirmEmail)
VALUES 
('RegularUser', 'user12345', 'User', 'user@example.com', FALSE);

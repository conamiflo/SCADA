# SCADA System Project

## Overview

This project implements a SCADA (Supervisory Control and Data Acquisition) system that supports the management and monitoring of analog and digital tags. The system includes user registration, tag management, real-time data acquisition, alarm handling, and reporting.

## Features

### Tag Management
- **Analog Input (AI) and Output (AO)**
  - Tag ID, Description, Driver, I/O Address, Scan Time, Alarms, On/Off Scan
- **Digital Input (DI) and Output (DO)**
  - Tag ID, Description, I/O Address, Initial Value

### User Management
- User registration and login for DatabaseManager access.

### DatabaseManager Application
- Add/Remove tags
- Enable/Disable scanning of input tags
- Write and display output tag values
- Register, log in, and log out users

### Trending Application
- Real-time display of input tag values.

### SCADA Core
- Simulation Driver: Generates predefined signals.
- Tag Processing: Reads tag values and generates events.
- RTU Integration: Connects to RTUs for real-time data.

### Alarms
- Add/Remove alarms for analog inputs
- Log alarms to `alarmsLog.txt` and a database
- Display alarms via Alarm Display client

### Configuration Management
- Read/Write system and alarm configurations from/to `scadaConfig.xml` on startup/shutdown.

### Reporting
- Generate reports on alarms and tag values via Report Manager.

## Installation and Setup

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/conamiflo/SCADA.git
   cd SCADA-Project

2. **Run the Applications**:
   - Start the SCADA Core and launch other applications as needed.

## Usage

### User Registration and Login

- **Register a New User**: Register a new user via the DatabaseManager application.
- **Log In**: Log in to access tag management and monitoring functionalities.

### Tag Management

- **Add or Remove Tags**: Add or remove tags and configure their properties using the DatabaseManager.

### Monitoring and Control

- **Enable/Disable Scanning**: Enable or disable scanning of input tags.
- **Write and View Output Tag Values**: Write values to output tags and view their current values.

### Alarm Handling

- **Set Up and Monitor Alarms**: Set up alarms and monitor them through the Alarm Display client.

### Reporting

- **Generate Reports**: Generate reports on tag values and alarms using the Report Manager.

  


   
